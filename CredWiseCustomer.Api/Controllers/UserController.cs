using CredWiseCustomer.Application.DTOs;
using CredWiseCustomer.Application.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System;
using Microsoft.Extensions.Options;
using Authentication.Service.Interfaces;
using Loggers.service.Services;

namespace CredWiseCustomer.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IValidator<CreateUserDto> _createUserValidator;
        //private readonly IValidator<UpdateUserDto> _updateUserValidator;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IAuthenticationService _authService;
        private readonly AuditLogger _logger;

        public UserController(
            IUserService userService,
            IValidator<CreateUserDto> createUserValidator,
            IPasswordHasher passwordHasher,
            IAuthenticationService authService,
            AuditLogger logger)
        {
            _userService = userService;
            _createUserValidator = createUserValidator;
            _passwordHasher = passwordHasher;
            _authService = authService;
            _logger = logger;
        }

        //[AllowAnonymous]
        //[HttpPost("decode")]
        //public IActionResult DecodeToken([FromBody] string token)
        //{
        //    try
        //    {
        //        _logger.LogApiRequest("POST", "/api/User/decode", "Token decode request received");
                
        //        if (string.IsNullOrEmpty(token))
        //        {
        //            _logger.LogWarning("Empty token provided", "/api/User/decode", "POST");
        //            return BadRequest(ApiResponse<object>.CreateError("Token cannot be empty"));
        //        }

        //        if (token.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
        //        {
        //            token = token.Substring("Bearer ".Length).Trim();
        //        }

        //        var handler = new JwtSecurityTokenHandler();

        //        if (!handler.CanReadToken(token))
        //        {
        //            _logger.LogWarning("Invalid JWT format", "/api/User/decode", "POST");
        //            return BadRequest(ApiResponse<object>.CreateError("Token is not in a valid JWT format"));
        //        }

        //        var jwtToken = handler.ReadJwtToken(token);
        //        var response = new
        //        {
        //            Header = jwtToken.Header,
        //            Payload = jwtToken.Payload,
        //            Claims = jwtToken.Claims.Select(c => new { c.Type, c.Value }),
        //            RoleClaim = jwtToken.Claims.FirstOrDefault(c =>
        //                c.Type == "role" || c.Type == ClaimTypes.Role)?.Value
        //        };

        //        _logger.LogInfo("Token decoded successfully", "/api/User/decode", "POST");
        //        return Ok(ApiResponse<object>.CreateSuccess(response, "Token decoded successfully"));
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError($"Token decoding failed: {ex.Message}", "/api/User/decode", "POST");
        //        return BadRequest(ApiResponse<object>.CreateError($"Token decoding failed: {ex.Message}"));
        //    }
        //}
       
        //[Authorize(Policy = "Admin")]
        //[HttpPost]
        //public async Task<ActionResult<ApiResponse<UserDto>>> CreateUser([FromBody] CreateUserDto createUserDto)
        //{
        //    _logger.LogApiRequest("POST", "/api/User", "Create user request received");
            
        //    var validationResult = await _createUserValidator.ValidateAsync(createUserDto);
        //    if (!validationResult.IsValid)
        //    {
        //        _logger.LogWarning("User creation validation failed", "/api/User", "POST");
        //        return BadRequest(ApiResponse<object>.CreateError("Validation failed", validationResult.ToDictionary()));
        //    }

        //    createUserDto.CreatedBy = "Admin";
        //    var user = await _userService.CreateUserAsync(createUserDto);
        //    _logger.LogUserAction("Admin", user.UserId.ToString(), "Created new user", "/api/User", "POST");
        //    return CreatedAtAction(
        //        nameof(GetUserById), 
        //        new { id = user.UserId }, 
        //        ApiResponse<UserDto>.CreateSuccess(user, "User created successfully")
        //    );
        //}

        [HttpGet("{id:int}")]
        [Authorize(Policy = "Admin")]
        public async Task<ActionResult<ApiResponse<UserDto>>> GetUserById(int id)
        {
            var user = await _userService.GetByIdAsync(id);
            if (user == null)
            {
                return NotFound(ApiResponse<object>.CreateError($"User with ID {id} not found"));
            }
            return Ok(ApiResponse<UserDto>.CreateSuccess(user, "User retrieved successfully"));
        }

        //[HttpGet("email/{email}")]
        //[Authorize(Policy = "Admin")]
        //public async Task<ActionResult<ApiResponse<UserDto>>> GetUserByEmail(string email)
        //{
        //    var user = await _userService.GetByEmailAsync(email);
        //    if (user == null)
        //    {
        //        return NotFound(ApiResponse<object>.CreateError($"User with email {email} not found"));
        //    }
        //    return Ok(ApiResponse<UserDto>.CreateSuccess(user, "User retrieved successfully"));
        //}

        [HttpGet]
        [Authorize(Policy = "Admin")]
        public async Task<ActionResult<ApiResponse<IEnumerable<UserDto>>>> GetAllUsers()
        {
            var users = await _userService.GetAllAsync();
            return Ok(ApiResponse<IEnumerable<UserDto>>.CreateSuccess(users, "Users retrieved successfully"));
        }

        //[HttpPut("{id:int}")]
        //[Authorize(Policy = "AdminOrCustomer")]
        //public async Task<ActionResult<ApiResponse<UserDto>>> UpdateUser(int id, [FromBody] UpdateUserDto updateUserDto)
        //{
        //    var validationResult = await _updateUserValidator.ValidateAsync(updateUserDto);
        //    if (!validationResult.IsValid)
        //    {
        //        _logger.LogWarning($"User update validation failed for UserId {id}", "/api/User/{id}", "PUT");
        //        return BadRequest(ApiResponse<object>.CreateError("Validation failed", validationResult.ToDictionary()));
        //    }

        //    var updatedUser = await _userService.UpdateUserAsync(id, updateUserDto);
        //    if (updatedUser == null)
        //    {
        //        _logger.LogWarning($"User with ID {id} not found for update", "/api/User/{id}", "PUT");
        //        return NotFound(ApiResponse<object>.CreateError($"User with ID {id} not found"));
        //    }
        //    _logger.LogUserAction("Unknown", id.ToString(), "Updated user", "/api/User/{id}", "PUT");
        //    return Ok(ApiResponse<UserDto>.CreateSuccess(updatedUser, "User updated successfully"));
        //}

        //[HttpDelete("{id:int}")]
        //[Authorize(Policy = "Admin")]
        //public async Task<ActionResult<ApiResponse<object>>> SoftDeleteUser(int id)
        //{
        //    var result = await _userService.SoftDeleteUserAsync(id);
        //    if (!result)
        //    {
        //        _logger.LogWarning($"User with ID {id} not found for delete", "/api/User/{id}", "DELETE");
        //        return NotFound(ApiResponse<object>.CreateError($"User with ID {id} not found"));
        //    }
        //    _logger.LogUserAction("Admin", id.ToString(), "Soft deleted user", "/api/User/{id}", "DELETE");
        //    return Ok(ApiResponse<object>.CreateSuccess(null, "User deleted successfully"));
        //}

        //[HttpPost("{id:int}/restore")]
        //[Authorize(Policy = "Admin")]
        //public async Task<ActionResult<ApiResponse<object>>> RestoreUser(int id)
        //{
        //    var result = await _userService.RestoreUserAsync(id);
        //    if (!result)
        //    {
        //        _logger.LogWarning($"User with ID {id} not found for restore", "/api/User/{id}/restore", "POST");
        //        return NotFound(ApiResponse<object>.CreateError($"User with ID {id} not found"));
        //    }
        //    _logger.LogUserAction("Admin", id.ToString(), "Restored user", "/api/User/{id}/restore", "POST");
        //    return Ok(ApiResponse<object>.CreateSuccess(null, "User restored successfully"));
        //}

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<ActionResult<ApiResponse<UserDto>>> Register([FromBody] CreateUserDto dto)
        {
            dto.CreatedBy = "Customer";
            var validationResult = await _createUserValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
            {
                _logger.LogWarning("User registration validation failed", "/api/User/register", "POST");
                return BadRequest(ApiResponse<object>.CreateError("Validation failed", validationResult.ToDictionary()));
            }
            var user = await _userService.CreateUserAsync(dto);
            _logger.LogUserAction("Customer", user.UserId.ToString(), "Registered new user", "/api/User/register", "POST");
            return CreatedAtAction(
                nameof(GetUserById), 
                new { id = user.UserId }, 
                ApiResponse<UserDto>.CreateSuccess(user, "User registered successfully")
            );
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<ApiResponse<object>>> Login([FromBody] LoginUserDto dto)
        {
            try
            {
                var user = await _userService.GetByEmailAsync(dto.Email);
                var userEntity = await _userService.GetUserEntityByEmailAsync(dto.Email);
                
                if (userEntity == null || !_passwordHasher.VerifyPassword(dto.Password, userEntity.Password))
                {
                    _logger.LogUserAction("Unknown", "Unknown", $"Failed login attempt for {dto.Email}", "/api/User/login", "POST");
                    return Unauthorized(ApiResponse<object>.CreateError("Invalid credentials"));
                }

            var claims = new Dictionary<string, object>
            {
                { "unique_name", user.Email },
                { "nameid", user.UserId.ToString() },
                { "role", user.Role ?? "Customer" }
            };

            var token = await _authService.GenerateTokenAsync(claims);
            var validationResult = await _authService.ValidateTokenAsync(token);
                
            if (!validationResult.IsValid)
            {
                    _logger.LogError($"Failed to generate valid token for user {user.Email}", "/api/User/login", "POST");
                    return StatusCode(500, ApiResponse<object>.CreateError("Failed to generate valid token"));
            }
            
                _logger.LogUserLogin(user.Role ?? "Customer", user.UserId.ToString(), "/api/User/login");
                return Ok(ApiResponse<object>.CreateSuccess(
                    new { token, user }, 
                    "Login successful"
                ));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Login failed: {ex.Message}", "/api/User/login", "POST");
                return Unauthorized(ApiResponse<object>.CreateError("Invalid credentials"));
            }
        }

        //[Authorize(Policy = "Admin")]
        //[HttpGet("admin-only")]
        //public ActionResult<ApiResponse<string>> AdminOnly()
        //{
        //    return Ok(ApiResponse<string>.CreateSuccess("This endpoint is for Admins only"));
        //}

        //[Authorize(Policy = "AdminOrCustomer")]
        //[HttpGet("admin-or-customer")]
        //public ActionResult<ApiResponse<string>> AdminOrCustomer()
        //{
        //    return Ok(ApiResponse<string>.CreateSuccess("This endpoint is for Admin or Customer"));
        //}

        //[HttpGet("logtest")]
        //[AllowAnonymous]
        //public IActionResult LogTest()
        //{
        //    _logger.LogInfo("Manual log test from /api/User/logtest", "/api/User/logtest", "GET");
        //    return Ok("Log entry created!");
        //}
    }
}