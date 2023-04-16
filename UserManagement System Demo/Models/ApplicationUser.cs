using Microsoft.AspNetCore.Identity;

namespace UserManagement_System_Demo.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? FullName { get; set; }

        public string? Address { get; set; }
        

        public string? State { get; set; }
    }
}
