using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.Core.Common
{
    public class AttendanceTableEntity 
    {
        // Property to hold the employee's ID
        [Required(ErrorMessage = "Employee ID is required.")]
        public string EmpID { get; set; }

        // Property to hold the employee's first name
        [Required(ErrorMessage = "First name is required.")]
        [StringLength(100, ErrorMessage = "First name cannot be longer than 100 characters.")]
        public string FirstName { get; set; }

        // Property to hold the employee's last name
        [Required(ErrorMessage = "Last name is required.")]
        [StringLength(100, ErrorMessage = "Last name cannot be longer than 100 characters.")]
        public string LastName { get; set; }

        // Property to hold the employee's designation
        [Required(ErrorMessage = "Designation is required.")]
        public string Designation { get; set; }

        // Property to hold the employee's working hours
        [Required(ErrorMessage = "Working hours are required.")]
        //[RegularExpression(@"^\d+(\.\d{1,2})?$", ErrorMessage = "Working hours must be a valid number with up to two decimal places.")]
        public string WorkingHours { get; set; }
    }

}

