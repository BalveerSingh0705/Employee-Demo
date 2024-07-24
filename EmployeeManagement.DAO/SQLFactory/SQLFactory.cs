

namespace EmployeeManagement.DAO
{
    public class SQLFactory : DAOFactories
    {

        public override IConfiguration Configuration => new EmployeeConfigurationDAO();

    }
}
