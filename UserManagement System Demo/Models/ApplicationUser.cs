using Microsoft.AspNetCore.Identity;

namespace UserManagement_System_Demo.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? PhoneNumber { get; set; }


        public string? Address { get; set; }
    }
}
