using System.Collections.Generic;

namespace Authentication.Service.Models
{
    public class AuthResult
    {
        public bool IsValid { get; set; }
        public Dictionary<string, object> Claims { get; set; }
        public string Error { get; set; }
    }
} 