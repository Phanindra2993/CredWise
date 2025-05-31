using System.Collections.Generic;
using System.Threading.Tasks;
using Authentication.Service.Models;

namespace Authentication.Service.Interfaces
{
    public interface IAuthenticationService
    {
        Task<string> GenerateTokenAsync(Dictionary<string, object> claims);
        Task<AuthResult> ValidateTokenAsync(string token);
        Task<bool> RevokeTokenAsync(string token);
    }
} 