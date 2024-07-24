
using EmployeeManagement.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Business.Base
{
    public class BaseBO<T>
    {
        public T DAO => DAOManager.GetDAO<T>();
    }
}
