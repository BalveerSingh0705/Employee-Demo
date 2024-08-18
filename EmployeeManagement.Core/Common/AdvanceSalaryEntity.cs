using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.Core.Common
{
    public class AdvanceSalaryEntity
    {
        [Required(ErrorMessage = "Employee ID is required.")]
        public string EmpID { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Advance amount is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Advance amount must be greater than 0.")]
        public decimal AdvanceAmount { get; set; }

        [Required(ErrorMessage = "Advance date is required.")]

        public DateTime AdvanceDate { get; set; }

        [Required(ErrorMessage = "Payment time is required.")]

        public String PaymentTime { get; set; }

        [Required(ErrorMessage = "Payment mode is required.")]
        public string PaymentMode { get; set; }

        [Required(ErrorMessage = "Payment by is required.")]
        public string PaymentBy { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Other credit must be a positive value.")]
        public decimal? OtherCredit { get; set; } 

        public string OtherComment { get; set; }
    }
}

