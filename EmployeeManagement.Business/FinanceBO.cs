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
            try
            {
                // Fetch employee salary details from the database
                List<EmployeeSalaryEntity> employeeSalaries = DAO.FinalSalary(salaryRequestEntity);

                foreach (var employeeSalaryEntity in employeeSalaries)
                {
                    // Check if the employee salary and working hours are not null
                    if (employeeSalaryEntity.Salary.HasValue && !string.IsNullOrEmpty(employeeSalaryEntity.WorkingHours))
                    {
                        decimal salaryPerMinute = (decimal)employeeSalaryEntity.Salary / (decimal)TimeSpan.Parse(employeeSalaryEntity.WorkingHours).TotalMinutes;
                        decimal totalMinutesWorked = (decimal)employeeSalaryEntity.TotalAttendances * (decimal)TimeSpan.Parse(employeeSalaryEntity.WorkingHours).TotalMinutes;

                        // Calculate basic salary
                        decimal basicSalary = salaryPerMinute * totalMinutesWorked;

                        // Calculate PF and ESI amounts (12% each)
                        employeeSalaryEntity.PfAmount = (basicSalary * 12) / 100;
                        employeeSalaryEntity.EsiAmount = (basicSalary * 12) / 100;

                        // Calculate overtime salary
                        decimal otSalary = (employeeSalaryEntity.TotalHours*60 ?? 0) * salaryPerMinute;
                        employeeSalaryEntity.TotalSalaryForMonth = employeeSalaryEntity.TotalSalaryForMonth - (employeeSalaryEntity.PfAmount + employeeSalaryEntity.EsiAmount);

                        // CalculemployeeSalaryEntityate total salary for the month
                        employeeSalaryEntity.TotalSalaryForMonth = (basicSalary + otSalary + employeeSalaryEntity.OtherCredit.GetValueOrDefault()) - employeeSalaryEntity.AdvanceAmount.GetValueOrDefault();
                    }
                }

                return employeeSalaries;
            }
            catch (Exception ex)
            {
                // Log exception if necessary and return an empty list or null
                // Console.WriteLine(ex.Message); // Example for logging
                return new List<EmployeeSalaryEntity>();
            }
        }
    }
}
