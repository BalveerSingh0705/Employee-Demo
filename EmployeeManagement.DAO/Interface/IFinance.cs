using EmployeeManagement.Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.DAO.Interface
{
    public interface IFinance
    {
        bool AdvanceSalary(AdvanceSalaryEntity advanceSalaryEntity);
        List<EmployeeSalaryEntity> FinalSalary(SalaryRequestEntity salaryRequestEntity);
    }
}
