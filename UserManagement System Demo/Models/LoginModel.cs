using System.ComponentModel.DataAnnotations;

namespace UserManagement_System_Demo.Models
{
    public class LoginModel
    {
        //[StringLength(maximumLength: 21, MinimumLength = 5, ErrorMessage = "The maximum character allowed is 21 while the minimum is 5")]
        //public string? Username { get; set; }
        //[DataType(DataType.EmailAddress)]
        public string? Username { get; set; } 

        [DataType(DataType.Password)]
        public string? Password { get; set; }

        
    }
}
