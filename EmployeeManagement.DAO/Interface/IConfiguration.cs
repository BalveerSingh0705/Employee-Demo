using EmployeeManagement.Core.Common;

namespace EmployeeManagement.DAO
{
    public interface IConfiguration
    {
        /// <summary>
        /// SaveOrUpdateTemplateFieldData
        /// </summary>
        /// <param name="template"></param>
        /// <returns></returns>
        bool SaveOrUpdateEmployeeDetails(EmployeeEntity employeeEntity);
    }
}
