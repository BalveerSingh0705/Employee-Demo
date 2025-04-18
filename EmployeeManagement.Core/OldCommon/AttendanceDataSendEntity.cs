using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Core.Common
{
    public class AttendanceDataSendEntity
    {
        public string EmpID { get; set; }
        public string Attendance { get; set; }
        public DateTime AttendanceDate { get; set; }
        public OTDetails OTDetails { get; set; }


    }
}
