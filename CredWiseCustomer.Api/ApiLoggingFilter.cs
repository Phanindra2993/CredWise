using Microsoft.AspNetCore.Mvc.Filters;
using Loggers.service.Services;
using System.Security.Claims;
using System.Linq;

namespace CredWiseCustomer.Api
{
    public class ApiLoggingFilter : IActionFilter
    {
        private readonly AuditLogger _logger;

        public ApiLoggingFilter(AuditLogger logger)
        {
            _logger = logger;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var httpContext = context.HttpContext;
            var endpoint = httpContext.Request.Path;
            var method = httpContext.Request.Method;

            var user = httpContext.User;
            int userId = 0; // 0 for anonymous
            string userType = "Anonymous";

            if (user.Identity != null && user.Identity.IsAuthenticated)
            {
                var userIdClaim = user.Claims.FirstOrDefault(c => c.Type == "nameid")?.Value;
                if (int.TryParse(userIdClaim, out int parsedId))
                    userId = parsedId;
                userType = user.Claims.FirstOrDefault(c => c.Type == "role")?.Value ?? "Anonymous";
            }

            _logger.LogApiRequest(method, endpoint, $"API {method} request by {userType} (ID: {userId})");
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            // Optionally log after the action executes
        }
    }
} 