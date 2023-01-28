namespace UserManagement_System_Demo.Models
{
    public class Base
    {
        public int Id { get; set; }

        public DateTime? DateCreated { get; set; }

        public bool? IsDeleted { get; set; }

        public DateTime? DateUpdated { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public int? Deletedby { get; set; }
    }
}
