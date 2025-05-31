using System.Collections.Generic;

namespace CredWiseCustomer.Application.Interfaces
{
    public interface IAuditLogger
    {
        void LogInfo(string message, string apiEndpoint = null, string apiMethod = null);
        void LogWarning(string message, string apiEndpoint = null, string apiMethod = null);
        void LogError(string message, string apiEndpoint = null, string apiMethod = null);
        void LogUserLogin(string userType, string userId, string apiEndpoint = null);
        void LogUserLogout(string userType, string userId, string apiEndpoint = null);
        void LogUserAction(string userType, string userId, string action, string apiEndpoint = null, string apiMethod = null);
        void LogApiRequest(string httpMethod, string endpoint, string message = null);
        IEnumerable<dynamic> GetRecentLogs(int count = 100);
    }
} 