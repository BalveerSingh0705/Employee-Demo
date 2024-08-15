using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.Core.Common
{
    public class AdvanceSalaryEntity
    {
        [Required]
        public string EmpID { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Please enter a valid amount")]
        public decimal AdvanceAmount { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime AdvanceDate { get; set; }

        [Required]
        [DataType(DataType.Time)]
        public TimeSpan PaymentTime { get; set; }

        [Required]
        public string PaymentMode { get; set; }

        [Required]
        public string PaymentBy { get; set; }

        public string OtherCredit { get; set; }

        public string OtherComment { get; set; }
    }
}
