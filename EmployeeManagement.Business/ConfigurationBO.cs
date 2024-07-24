using EmployeeManagement.Business.Base;
using EmployeeManagement.Core.Common;
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
    }
}
