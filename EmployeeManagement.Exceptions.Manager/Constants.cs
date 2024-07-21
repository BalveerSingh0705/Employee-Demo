namespace EmployeeManagement.Exceptions.Manager
{
    /// <summary>
    /// The Constants class provides configuration and parameter details.
    /// </summary>
    public static class Constants
    {
        #region Configurations

        public const string DB_CONFIG_CONNECTION_TYPE = "System.Data.SqlClient.SqlConnection, System.Data, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";
        public const string DB_CONFIG_STOREPROCEDURE = "usp_LogExceptionDetails";

        #endregion

        #region Parameters

        public const string longDate = "@Date";
        public const string thread = "@Thread";
        public const string logLevel = "@Level";
        public const string logger = "@Logger";
        public const string message = "@Message";
        public const string exception = "@Exception";
        public const string machineName = "@MachineName";
        public const string domain = "@Domain";
        public const string identity = "@Identity";
        public const string location = "@Location";
        public const string method = "@Method";
        public const string stacktrace = "@StackTraceDetails";

        #endregion
    }
}
