using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CredWiseCustomer.Application.DTOs
{
    public class CreateUserDto
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string PhoneNumber { get; set; }
        public string CreatedBy { get; set; } // Will be set to 'Admin' or 'Customer' internally
    }
}
