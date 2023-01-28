using System.ComponentModel.DataAnnotations;

namespace UserManagement_System_Demo.Models
{
    public class Order : Base
    {

        public int? UserId { get; set; }

        [Required]
        public int? Quantity { get; set; }

        [Required]
        public double Amount { get; set; }

        public bool? IsCompleted { get; set; }

        public bool? IsCancelled { get; set; }
    }
}
