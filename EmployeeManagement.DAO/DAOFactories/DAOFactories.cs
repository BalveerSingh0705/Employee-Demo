

using EmployeeManagement.DAO.Interface;

namespace EmployeeManagement.DAO
{
    public abstract class DAOFactories
    {
        #region Interfaces

       
        public abstract IConfiguration Configuration { get; }
        public abstract IFinance Finance { get; }


        #endregion

        #region Method

        /// <summary>
        /// Returns the factory objects.
        /// </summary>
        /// <returns></returns>
        public static DAOFactories GetFactory() => new SQLFactory();


        #endregion
    }
}
