using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Authentication.Service.Interfaces;

namespace Authentication.Service.Services
{
    public class PolicyBasedAuthorizationService : IAuthorizationService
    {
        private readonly Dictionary<string, Func<Dictionary<string, object>, bool>> _policies;

        public PolicyBasedAuthorizationService()
        {
            _policies = new Dictionary<string, Func<Dictionary<string, object>, bool>>();
        }

        public void AddPolicy(string policyName, Func<Dictionary<string, object>, bool> policy)
        {
            _policies[policyName] = policy;
        }

        public async Task<bool> AuthorizeAsync(Dictionary<string, object> claims, string resource, string action)
        {
            var policyKey = $"{resource}:{action}";
            return _policies.TryGetValue(policyKey, out var policy) && policy(claims);
        }
    }
} 