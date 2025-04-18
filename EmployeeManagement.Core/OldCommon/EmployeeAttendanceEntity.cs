using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Core.Common
{
    public class EmployeeAttendanceEntity
    {
        public string EmpID { get; set; }
        public string Name { get; set; }
        public string Designation { get; set; }
        public string SignIn { get; set; }
        public string SignOut { get; set; }
        public string Attendance { get; set; }
        public OTDetails OTDetails { get; set; }
    }
}
