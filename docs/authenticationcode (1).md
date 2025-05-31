# CredWise Authentication Service - Technical Documentation

## Table of Contents
1. [Architecture Overview](#architecture-overview)
2. [Data Models](#data-models)
3. [API Endpoints](#api-endpoints)
4. [Authentication Flow](#authentication-flow)
5. [Integration Guide](#integration-guide)
6. [Security Features](#security-features)
7. [Code Examples](#code-examples)

## Architecture Overview

The authentication service is built using .NET 8.0 and follows a clean architecture pattern:

```
Authentication.Service/
├── Controllers/       # API endpoints
├── Models/           # Data models and DTOs
├── Services/         # Business logic
├── Data/            # Database context and repositories
└── Middleware/      # Custom middleware components
```

### Key Components
- **JWT Token Service**: Handles token generation and validation
- **User Management Service**: Manages user operations
- **Role-based Authorization**: Handles user permissions
- **Password Service**: Manages password hashing and validation

## Data Models

### User Model
```csharp
public class User
{
    public Guid Id { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public List<string> Roles { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? LastLogin { get; set; }
}
```

### Authentication DTOs
```csharp
public class LoginRequest
{
    public string Username { get; set; }
    public string Password { get; set; }
}

public class LoginResponse
{
    public string Token { get; set; }
    public DateTime ExpiresAt { get; set; }
    public UserDto User { get; set; }
}

public class RegisterRequest
{
    public string Username { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }
}
```

## API Endpoints

### Authentication Endpoints

#### 1. Login
```
POST /api/auth/login
Content-Type: application/json

Request:
{
    "username": "string",
    "password": "string"
}

Response:
{
    "token": "jwt_token_string",
    "expiresAt": "2024-03-21T10:00:00Z",
    "user": {
        "id": "guid",
        "username": "string",
        "email": "string",
        "roles": ["string"]
    }
}
```

#### 2. Register
```
POST /api/auth/register
Content-Type: application/json

Request:
{
    "username": "string",
    "email": "string",
    "password": "string",
    "confirmPassword": "string"
}

Response:
{
    "id": "guid",
    "username": "string",
    "email": "string"
}
```

#### 3. Refresh Token
```
POST /api/auth/refresh
Authorization: Bearer {token}

Response:
{
    "token": "new_jwt_token_string",
    "expiresAt": "2024-03-21T10:00:00Z"
}
```

## Authentication Flow

1. **Registration Process**:
   ```mermaid
   sequenceDiagram
   Client->>Auth Service: POST /register
   Auth Service->>Database: Check if user exists
   Auth Service->>Auth Service: Hash password
   Auth Service->>Database: Save user
   Auth Service->>Client: Return user details
   ```

2. **Login Process**:
   ```mermaid
   sequenceDiagram
   Client->>Auth Service: POST /login
   Auth Service->>Database: Validate credentials
   Auth Service->>Auth Service: Generate JWT
   Auth Service->>Client: Return token + user
   ```

3. **Token Validation**:
   ```mermaid
   sequenceDiagram
   Client->>Protected API: Request with JWT
   Protected API->>Auth Service: Validate token
   Auth Service->>Protected API: Token valid/invalid
   Protected API->>Client: Response
   ```

## Integration Guide

### Frontend Integration (React/TypeScript Example)

1. **Authentication Context**:
```typescript
interface AuthContext {
  user: User | null;
  login: (username: string, password: string) => Promise<void>;
  logout: () => void;
  isAuthenticated: boolean;
}

const AuthProvider: React.FC = ({ children }) => {
  // Implementation
};
```

2. **API Client Setup**:
```typescript
import axios from 'axios';

const api = axios.create({
  baseURL: 'https://api.credwise.com'
});

api.interceptors.request.use((config) => {
  const token = localStorage.getItem('token');
  if (token) {
    config.headers.Authorization = `Bearer ${token}`;
  }
  return config;
});
```

3. **Login Component Example**:
```typescript
const Login: React.FC = () => {
  const { login } = useAuth();
  
  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    try {
      await login(username, password);
      // Redirect on success
    } catch (error) {
      // Handle error
    }
  };
};
```

### Backend Integration (.NET Example)

1. **Controller Protection**:
```csharp
[Authorize]
[ApiController]
[Route("api/[controller]")]
public class ProtectedController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        // Implementation
    }
}
```

2. **Service Authentication**:
```csharp
services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            // Add your configuration
        };
    });
```

## Security Features

1. **Password Security**:
   - Passwords are hashed using BCrypt
   - Minimum password requirements enforced
   - Password history maintained

2. **Token Security**:
   - Short-lived JWT tokens (15 minutes)
   - Refresh token rotation
   - Token blacklisting for logout

3. **Request Security**:
   - Rate limiting
   - CORS configuration
   - XSS protection headers

## Code Examples

### Token Generation
```csharp
public class JwtService
{
    public string GenerateToken(User user)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.Email, user.Email)
        }.Concat(user.Roles.Select(r => new Claim(ClaimTypes.Role, r)));

        // Token generation logic
    }
}
```

### User Authentication
```csharp
public class AuthService
{
    public async Task<LoginResponse> AuthenticateAsync(LoginRequest request)
    {
        var user = await _userRepository.GetByUsernameAsync(request.Username);
        if (user == null || !VerifyPassword(request.Password, user.PasswordHash))
        {
            throw new UnauthorizedException("Invalid credentials");
        }

        var token = _jwtService.GenerateToken(user);
        // Return login response
    }
}
```

## Error Handling

Common authentication errors and their handling:

```csharp
public class AuthenticationException : Exception
{
    public int StatusCode { get; }

    public AuthenticationException(string message, int statusCode = 401)
        : base(message)
    {
        StatusCode = statusCode;
    }
}
```

Error response format:
```json
{
    "error": {
        "code": "AUTH_ERROR",
        "message": "Invalid credentials",
        "details": "Username or password is incorrect"
    }
}
```

## Testing

Example test cases:

```csharp
public class AuthenticationTests
{
    [Fact]
    public async Task Login_WithValidCredentials_ReturnsToken()
    {
        // Test implementation
    }

    [Fact]
    public async Task Login_WithInvalidCredentials_ThrowsException()
    {
        // Test implementation
    }
}
```

---

## Best Practices

1. **Token Management**:
   - Store tokens securely (HttpOnly cookies for web apps)
   - Implement token refresh strategy
   - Clear tokens on logout

2. **Error Handling**:
   - Use consistent error responses
   - Don't expose sensitive information in errors
   - Log authentication failures

3. **Security**:
   - Implement rate limiting
   - Use HTTPS only
   - Implement proper CORS policies

## Support

For technical support or questions:
- Create an issue in the repository
- Contact the authentication team
- Check the troubleshooting guide

---
*This documentation is maintained by the CredWise Authentication Team* 