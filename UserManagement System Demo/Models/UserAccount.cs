using System.ComponentModel.DataAnnotations;

namespace UserManagement_System_Demo.Models
{
    public class UserAccount : Base
    {

        [DataType(DataType.Text)]
        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string? PhoneNumber { get; set; }

        public int Gender { get; set; }

        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }


        public string? MaritalStatus { get; set; }


        public string? Address { get; set; }

        public int StateId { get; set; }


        public int LGAId { get; set; }

        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }

        [StringLength(maximumLength: 21, MinimumLength =5, ErrorMessage ="The maximum character allowed is 21 while the minimum is 5")]
        public string? Username { get; set; }

        [DataType(DataType.Password)]
        public string? Password { get; set; }

        public int StatusId { get; set; }

    }
}
