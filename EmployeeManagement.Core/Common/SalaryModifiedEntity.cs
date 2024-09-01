using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Core.Common
{
    public class SalaryModifiedEntity
    {

            public decimal Salary { get; set; } // Make sure this property exists
            public string WorkingHours { get; set; }
            public int? TotalAttendances { get; set; }
            public int? TotalHours { get; set; }
            public decimal? OtherCredit { get; set; }
            public decimal? AdvanceAmount { get; set; }
            // Other properties as needed...
  

    }
}
