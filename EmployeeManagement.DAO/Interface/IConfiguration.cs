using EmployeeManagement.Core.Common;
using EmployeeManagement.Core.Entities;

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
        List<TableFormEntity> GetEmployeeDetailsInTableForm();
      
      //  List<EmployeeEntity> DeleteSingleEmployeeDetails(EmployeeDataInIDEntity employeeDataInIDEntity);
        List<EmployeeEntity> GetEmployeeDetailsClickOnEditButton(EmployeeDataInIDEntity employeeDataInIDEntity);
        bool DeleteSingleEmployeeDetails(EmployeeDataInIDEntity employeeDataInIDEntity);
        bool SaveEmployeeChangesInfo(EmployeeEntity employeeEntity);
    }
}
