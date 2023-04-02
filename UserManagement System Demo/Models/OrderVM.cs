namespace UserManagement_System_Demo.Models
{
    public class OrderVM
    {
        
        public int? Quantity { get; set; }

        
        public double Amount { get; set; }

        public string? Status { get; set; }

        public string? OrderStatus { get; set; }

        public int Id { get; set; }

        public DateTime? DateCreated { get; set; }
    }
}
