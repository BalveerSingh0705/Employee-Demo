using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Core.Common
{
    public class OverTimeEntity
    {
        public string EmpID { get; set; }   // Employee ID
        public string Hours { get; set; }      // Overtime hours
        public string Comment { get; set; } // Overtime comments
    }
}
