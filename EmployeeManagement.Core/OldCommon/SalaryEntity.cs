using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Core.Common
{
    public class SalaryEntity
    {
            public string EmpID { get; set; }
            public string Name { get; set; }
            public string Designation { get; set; }
            public decimal Salary { get; set; }
            public decimal Advance { get; set; }
            public decimal PF { get; set; }
            public decimal ESIF { get; set; }
            public decimal OtherCredit { get; set; }
            public decimal TotalSalary { get;set; }
            //public decimal TotalSalary => Salary - Advance - PF - ESIF - OtherCredit;

    }
}
