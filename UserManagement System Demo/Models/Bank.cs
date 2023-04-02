using System.ComponentModel.DataAnnotations;

namespace UserManagement_System_Demo.Models
{
	public class Bank : Base
	{
		[Required]
		[Display(Name = "Bank Name")]
		public string? BankName { get; set; }

		[Required]
		[Display(Name = "Bank Location")]
		public string? BankLocation { get; set; }
	}
}
