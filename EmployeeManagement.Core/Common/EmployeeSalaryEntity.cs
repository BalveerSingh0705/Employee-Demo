namespace EmployeeManagement.Core.Common
{
    public class EmployeeSalaryEntity
    {
        //public string EmpID { get; set; }
        //public string Name { get; set; }
        //public string Designation { get; set; }
        //public decimal Salary { get; set; }
        //public decimal Advance { get; set; }
        //public decimal PF { get; set; }
        //public decimal ESIF { get; set; }
        //public decimal OtherCredit { get; set; }
        //public decimal TotalSalary { get; set; }
        public string EmpID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Designation { get; set; }
        public string Phone { get; set; }
        public decimal? Salary { get; set; }
        public decimal? AdvanceAmount { get; set; }
        public decimal? OtherCredit { get; set; } // Nullable if there might be no other credits
        public int? TotalAttendances { get; set; }
        public int? TotalHours { get; set; }
        public string WorkingHours { get; set; }
        public decimal? TotalSalaryForMonth { get; set; }
    }
}


