using System.ComponentModel.DataAnnotations;

namespace UserManagement_System_Demo.Models
{
    public class RegisterModel
    {
        [DataType(DataType.Text)]
        public string? Fullname { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string? Phonenumber { get; set; }


        [StringLength(maximumLength: 21, MinimumLength = 5, ErrorMessage = "The maximum character allowed is 21 while the minimum is 5")]
        public string? Username { get; set; }

        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [DataType(DataType.Password)]
        public string? ConfirmPassword { get; set; }

        public string? Email { get; set; }


        public string? ConfirmEmail { get; set; }
    }
}
