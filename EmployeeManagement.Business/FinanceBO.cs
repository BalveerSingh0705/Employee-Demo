using EmployeeManagement.Business.Base;
using EmployeeManagement.Core.Common;
using EmployeeManagement.DAO.Interface;

namespace EmployeeManagement.Business
{
    public class FinanceBO : BaseBO<IFinance>
    {
        public object AdvanceSalary(AdvanceSalaryEntity advanceSalaryEntity)
        {
            return DAO.AdvanceSalary(advanceSalaryEntity);
        }

        public List<EmployeeSalaryEntity> FinalSalary(SalaryRequestEntity salaryRequestEntity)
        {
            return DAO.FinalSalary(salaryRequestEntity);

        }
    }
}
