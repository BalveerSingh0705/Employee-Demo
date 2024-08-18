using EmployeeManagement.Business.Base;
using EmployeeManagement.Core.Common;
using EmployeeManagement.Core.Entities;
using EmployeeManagement.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Business
{
    public class ConfigurationBO: BaseBO<IConfiguration>
    {
        public bool SaveOrUpdateEmployeeDetails(EmployeeEntity employeeEntity)
        {
            return DAO.SaveOrUpdateEmployeeDetails(employeeEntity);
        }
        public List<TableFormEntity> GetEmployeeDetailsInTableForm()
        {
            return DAO.GetEmployeeDetailsInTableForm();
        }

        public List<EmployeeEntity> GetEmployeeDetailsClickOnEditButton(EmployeeDataInIDEntity employeeDataInIDEntity)
        {
            return DAO.GetEmployeeDetailsClickOnEditButton(employeeDataInIDEntity);
        }

        public object DeleteSingleEmployeeDetails(EmployeeDataInIDEntity employeeDataInIDEntity)
        {
            return DAO.DeleteSingleEmployeeDetails(employeeDataInIDEntity);
        }

        public object SaveEmployeeChangesInfo(EmployeeEntity employeeEntity)
        {
            return DAO.SaveEmployeeChangesInfo(employeeEntity);
        }

        public List<AttendanceTableEntity> GetEmployeeDetailsInAttendanceTable()
        {
            return DAO.GetEmployeeDetailsInAttendanceTable();
        }

        public bool SendEmployeeAttendanceDetails(List<AttendanceDataSendEntity> attendanceDataSendEntity)
        {
            return DAO.SendEmployeeAttendanceDetails(attendanceDataSendEntity);
        }

        public IdEntity GetNextEmpID()
        {
            return DAO.GetNextEmpID();
        }
    }
  
}
