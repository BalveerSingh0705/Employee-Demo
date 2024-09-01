namespace EmployeeManagement.Core.Common
{
    public class EmployeeSalaryEntity
    {

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
        public decimal? EsiAmount {  get; set; }
        public decimal? PfAmount { get; set; }
    }
}


