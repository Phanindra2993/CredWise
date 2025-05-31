

namespace Authentication.Service.Interfaces
{
    public interface IAuthorizationService
    {
        Task<bool> AuthorizeAsync(Dictionary<string, object> claims, string resource, string action);   
        void AddPolicy(string policyName, System.Func<Dictionary<string, object>, bool> policy);
        
    }
} 