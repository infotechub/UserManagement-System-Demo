﻿using System.ComponentModel.DataAnnotations;

namespace UserManagement_System_Demo.Models
{
    public class Product : Base
    {

        [Required]
        public string? ProductName { get; set; }
        [Required]
        [StringLength(maximumLength: 50, MinimumLength = 30, 
        ErrorMessage ="The {0} cannot be more than 50 and the {1} can not be less than 30")]
        public string? ProductDescription { get; set; }
        [Required]
        [DataType(DataType.Currency)]
        public double ProductPrice { get; set; }

        public string? Manufacturer { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime? ExpirationDate { get; set; }
    }
}
