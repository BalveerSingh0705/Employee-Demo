
using EmployeeManagement.DAO.Interface;

namespace EmployeeManagement.DAO
{
    public static class DAOManager
    {
        private static readonly DAOFactories factory = DAOFactories.GetFactory();
        private static readonly IDictionary<Type, object> DaoList = new Dictionary<Type, object>()
        {

              {typeof(IConfiguration), factory.Configuration},
              {typeof(IFinance), factory.Finance},
            {typeof(IAuth), factory.Auth}



        };

        /// <summary>
        /// Function which return the DAO Proxy object based on the input parameter "T".
        /// </summary>
        /// <typeparam name="T">Input type parameter (i.e. interface)</typeparam>
        /// <returns>Return the DAO Proxy object based on the input parameter "T"</returns>
        public static T GetDAO<T>() => (T)DaoList[typeof(T)];
    }
}
