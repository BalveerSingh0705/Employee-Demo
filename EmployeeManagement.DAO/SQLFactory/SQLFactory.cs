

using EmployeeManagement.DAO.Class;
using EmployeeManagement.DAO.Interface;

namespace EmployeeManagement.DAO
{
    public class SQLFactory : DAOFactories
    {

        public override IConfiguration Configuration => new EmployeeConfigurationDAO();
        public override IFinance Finance => new FinanceConfigrationDAO();
        public override IAuth Auth => new AuthConfigurationDAO();

    }
}
