using System.ComponentModel.DataAnnotations;

namespace UserManagement_System_Demo.Models
{
    public class Wallet : Base
    {
        [DataType(DataType.Currency)]
        public double? AmountDeposit { get; set; }


        [DataType(DataType.Currency)]
        public double? AmountWithdraw { get; set; }


       
        public int? UserId { get; set; }


        [DataType(DataType.Currency)]
        public double? AvailableBalance { get; set; }


        [DataType(DataType.Currency)]
        public double? BookBalance { get; set; }
    }
}
