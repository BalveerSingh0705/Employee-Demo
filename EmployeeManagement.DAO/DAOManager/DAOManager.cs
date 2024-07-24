


namespace EmployeeManagement.DAO
{
    public static class DAOManager
    {
        private static readonly DAOFactories factory = DAOFactories.GetFactory();
        private static readonly IDictionary<Type, object> DaoList = new Dictionary<Type, object>()
        {
            //{typeof(IUserActivityLog), factory.UserActivityLog},
            //{typeof(IUserInfo), factory.UserInfo},
            //{typeof(INavigationTree), factory.NavigationTree},
            //{typeof(IFileManager), factory.FileManager},
            //{typeof(IReports), factory.Reports},
            //{typeof(IFilterLists), factory.FilterLists},
            //{typeof(IUserWidget), factory.UserWidgets },
            //{typeof(IRecentAsset), factory.RecentAsset },
            //{typeof(IOnlineMonitoring), factory.OnlineMonitoring },
            //{typeof(ITenantManagement), factory.TenantManagement },
            //{typeof(IUser), factory.User},
            //{typeof(IRole), factory.Role},
            //{typeof(IData), factory.Data},
              {typeof(IConfiguration), factory.Configuration}
            // {typeof(IUserPreferences), factory.UserPreferences},
            //{ typeof(ITruCompliancePublicPortal), factory.TruCompliancePublicPortal},
            //{typeof(IRecordCount), factory.RecordCount},
            //{typeof(ILog), factory.Log},
            //{typeof(IGeospatial), factory.Geospatial},
            //{typeof(IMonitoringConnector), factory.MonitoringConnector},
            //{typeof(IAuditLogs), factory.AuditLogs}


        };

        /// <summary>
        /// Function which return the DAO Proxy object based on the input parameter "T".
        /// </summary>
        /// <typeparam name="T">Input type parameter (i.e. interface)</typeparam>
        /// <returns>Return the DAO Proxy object based on the input parameter "T"</returns>
        public static T GetDAO<T>() => (T)DaoList[typeof(T)];
    }
}
