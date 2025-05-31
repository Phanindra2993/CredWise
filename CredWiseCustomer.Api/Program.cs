using CredWiseCustomer.Application.DTOs;
using CredWiseCustomer.Application.Interfaces;
using CredWiseCustomer.Application.Mappings;
using CredWiseCustomer.Application.Services;
using CredWiseCustomer.Application.Validators;
using CredWiseCustomer.Infrastructure.Data;
using CredWiseCustomer.Infrastructure.Repositories;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using CredWiseCustomer.Api;
using Loggers.service.Services;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();

// Register AuditLogger
builder.Services.AddSingleton<AuditLogger>(provider => 
{
    try
    {
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
        if (string.IsNullOrEmpty(connectionString))
        {
            throw new InvalidOperationException("DefaultConnection string is missing in appsettings.json");
        }
        return new AuditLogger(connectionString);
    }
    catch (Exception ex)
    {
        // Log the error but don't throw - this allows the application to start even if logging fails
        Console.WriteLine($"Failed to initialize AuditLogger: {ex.Message}");
        return new AuditLogger(); // Use default connection string
    }
});

// Register ApiLoggingFilter
builder.Services.AddScoped<ApiLoggingFilter>();
builder.Services.AddControllers(options =>
{
    options.Filters.Add<ApiLoggingFilter>();
});

// Database Configuration
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Application Services
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ILoanRepository, LoanRepository>();
builder.Services.AddScoped<IRepaymentRepository, RepaymentRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();
builder.Services.AddScoped<IFdRepository, FdRepository>();
builder.Services.AddScoped<IFdService, FdService>();
builder.Services.AddScoped<ILoanService, LoanService>();
builder.Services.AddScoped<IRepaymentService, RepaymentService>();
builder.Services.AddScoped<IReturnsCalculatorService, ReturnsCalculatorService>();
builder.Services.AddScoped<ILoanApplicationService, LoanApplicationService>();
builder.Services.AddScoped<ILoanApplicationRepository, LoanApplicationRepository>();
builder.Services.AddScoped<ILoanEnquiryRepository, LoanEnquiryRepository>();
builder.Services.AddScoped<ILoanEnquiryService, LoanEnquiryService>();


// Register validators
builder.Services.AddScoped<IValidator<CreateUserDto>, CreateUserDtoValidator>();
builder.Services.AddScoped<IValidator<UpdateUserDto>, UpdateUserDtoValidator>();

// OR register all validators in the assembly (recommended)
builder.Services.AddValidatorsFromAssemblyContaining<CreateUserDtoValidator>();
// FluentValidation
builder.Services.AddValidatorsFromAssemblyContaining<Program>(ServiceLifetime.Scoped);
// AutoMapper
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddAutoMapper(typeof(UserProfile).Assembly);
// Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "Please enter JWT with Bearer into field. Example: 'Bearer {token}'",
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

// Add Authentication
var jwtSection = builder.Configuration.GetSection("Jwt");
var jwtKey = jwtSection["SecretKey"];
var jwtIssuer = jwtSection["Issuer"];
var jwtAudience = jwtSection["Audience"];

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtIssuer,
        ValidAudience = jwtAudience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtKey)),
        ClockSkew = TimeSpan.Zero,
        RoleClaimType = "role",
        NameClaimType = "unique_name"
    };

    options.Events = new JwtBearerEvents
    {
        OnTokenValidated = context =>
        {
            Console.WriteLine("Token validated successfully");
            Console.WriteLine($"Claims after validation: {string.Join(", ", context.Principal.Claims.Select(c => $"{c.Type}: {c.Value}"))}");
            return Task.CompletedTask;
        },
        OnAuthenticationFailed = context =>
        {
            Console.WriteLine($"Authentication failed: {context.Exception.Message}");
            return Task.CompletedTask;
        },
        OnChallenge = context =>
        {
            Console.WriteLine($"Challenge issued: {context.Error}, {context.ErrorDescription}");
            return Task.CompletedTask;
        }
    };
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Admin", policy => 
    {
        policy.RequireClaim("http://schemas.microsoft.com/ws/2008/06/identity/claims/role", "Admin");
        policy.RequireAssertion(context =>
        {
            var claims = context.User.Claims.ToList();
            Console.WriteLine("Authorization check for Admin policy:");
            Console.WriteLine($"All claims: {string.Join(", ", claims.Select(c => $"{c.Type}: {c.Value}"))}");
            Console.WriteLine($"Has role claim: {claims.Any(c => c.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role")}");
            Console.WriteLine($"Role value: {claims.FirstOrDefault(c => c.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role")?.Value}");
            return true; // Let RequireClaim do the actual check
        });
    });
    options.AddPolicy("Customer", policy => policy.RequireClaim("http://schemas.microsoft.com/ws/2008/06/identity/claims/role", "Customer"));
    options.AddPolicy("AdminOrCustomer", policy => 
        policy.RequireAssertion(context => 
            context.User.HasClaim("http://schemas.microsoft.com/ws/2008/06/identity/claims/role", "Admin") || 
            context.User.HasClaim("http://schemas.microsoft.com/ws/2008/06/identity/claims/role", "Customer")));
});

// Bind AuthConfiguration from appsettings.json
var authConfig = builder.Configuration.GetSection("Jwt").Get<Authentication.Service.Models.AuthConfiguration>();
if (authConfig == null)
{
    throw new InvalidOperationException("JWT configuration is missing in appsettings.json");
}
builder.Services.AddSingleton(authConfig);
builder.Services.AddScoped<Authentication.Service.Interfaces.IAuthenticationService, Authentication.Service.Services.JwtAuthenticationService>();


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularDev",
        policy =>
        {
            policy.WithOrigins("http://localhost:4200")
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

var app = builder.Build();

app.UseCors("AllowAngularDev");

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Global Exception Handling Middleware
app.Use(async (context, next) =>
{
    try
    {
        await next();
    }
    catch (Exception ex)
    {
        var logger = context.RequestServices.GetService<AuditLogger>();
        logger?.LogError($"Unhandled exception: {ex.Message}", context.Request.Path, context.Request.Method);
        context.Response.StatusCode = 500;
        context.Response.ContentType = "application/json";
        var response = ApiResponse<object>.CreateError("An unexpected error occurred.", ex.Message);
        await context.Response.WriteAsJsonAsync(response);
    }
});

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();