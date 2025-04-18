using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Core.Common
{
    public class SalaryRequestEntity
    {
        public string EmpID { get; set; } = string.Empty;
        public int Month { get; set; }
        public int Year { get; set; }
    }
}
