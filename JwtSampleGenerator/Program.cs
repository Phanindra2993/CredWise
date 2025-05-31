using Authentication.Service.Models;
using Authentication.Service.Services;

var authConfig = new AuthConfiguration
{
    SecretKey = "A7d!kL9zQ2x#Vb6pT4w@eR8uY1s$Jm5n",
    Issuer = "your-issuer",
    Audience = "your-audience",
    TokenExpirationInHours = 1
};

var authService = new JwtAuthenticationService(authConfig);

var claims = new Dictionary<string, object>
            {
                { "unique_name", "admin@credwise.com" },
                { "nameid", "1" },
                { "role", "Admin" }
            };

var token = await authService.GenerateTokenAsync(claims);
Console.WriteLine("Generated JWT token:");
Console.WriteLine(token);
Console.WriteLine("\nUse this token in your API requests as follows:");
Console.WriteLine($"Authorization: Bearer {token}");