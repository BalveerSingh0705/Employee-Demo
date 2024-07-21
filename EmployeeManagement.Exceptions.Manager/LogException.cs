
using log4net;
using log4net.Appender;
using log4net.Layout;
using log4net.Repository.Hierarchy;
using Microsoft.ApplicationInsights;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace EmployeeManagement.Exceptions.Manager
{
    /// <summary>
    /// The Log Exception is used to log exceptions and message level information.
    /// </summary>
    public class LogException : ILogging, IDisposable
    {
        private readonly ILog _logger;
        bool disposed = false;
        private TelemetryClient telemetry = new TelemetryClient();
        public LogException(Type objLog, string sqlConnectionString = "")
        {
            InitializeHierarchyLogManager();
            log4net.Config.BasicConfigurator.Configure(GetAdoNetAppender(sqlConnectionString));
            _logger = LogManager.GetLogger(objLog);
        }
        /// <summary>
        /// The Log Exception is used to log exceptions.
        /// </summary>
        public LogException(string objLog, string sqlConnectionString = "")
        {
            InitializeHierarchyLogManager();
            log4net.Config.BasicConfigurator.Configure(GetAdoNetAppender(sqlConnectionString));
            _logger = LogManager.GetLogger(objLog);
        }

        private static void InitializeHierarchyLogManager() => ((Hierarchy)LogManager.GetRepository()).Root.RemoveAllAppenders();

        private AdoNetAppender GetAdoNetAppender(string sqlConnectionString = "")
        {
            dynamic adoNetAppender;
            ConnectionState originalState = ConnectionState.Fetching;
            IDbConnection connection = null;

            try
            {
                if (string.IsNullOrEmpty(sqlConnectionString))
                {
                    sqlConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                }

                connection = new SqlConnection(sqlConnectionString);

                originalState = connection.State;
                if (originalState != ConnectionState.Open)
                    connection.Open();

                adoNetAppender = new AdoNetAppender
                {
                    ConnectionString = sqlConnectionString,
                    CommandText = Constants.DB_CONFIG_STOREPROCEDURE,
                    CommandType = CommandType.StoredProcedure,
                    ConnectionType = connection.ToString(),
                    BufferSize = 1,
                    UseTransactions = false,
                };

                adoNetAppender = GetAdoNetAppenderWithParameters(adoNetAppender);
                adoNetAppender.ActivateOptions();
            }
            finally
            {
                if (originalState != ConnectionState.Closed)
                    if (connection != null) connection.Close();
            }
            return adoNetAppender;
        }

        private PatternLayout GetConversionPatternLayout(string pattern)
        {
            var patternLayout = new PatternLayout
            {
                ConversionPattern = string.Format("%{0}", pattern),
            };
            patternLayout.ActivateOptions();
            return patternLayout;
        }

        private AdoNetAppender GetAdoNetAppenderWithParameters(AdoNetAppender adoNetAppender)
        {
            var logDateParameter = new AdoNetAppenderParameter
            {
                DbType = DbType.DateTime,
                ParameterName = Constants.longDate,
                Layout = new RawTimeStampLayout()
            };
            adoNetAppender.AddParameter(logDateParameter);

            var threadParameter = new AdoNetAppenderParameter
            {
                DbType = DbType.String,
                Size = 255,
                ParameterName = Constants.thread,
                Layout = new Layout2RawLayoutAdapter(GetConversionPatternLayout("thread"))
            };
            adoNetAppender.AddParameter(threadParameter);

            var logLevelParameter = new AdoNetAppenderParameter
            {
                DbType = DbType.String,
                Size = 50,
                ParameterName = Constants.logLevel,
                Layout = new Layout2RawLayoutAdapter(GetConversionPatternLayout("level"))
            };
            adoNetAppender.AddParameter(logLevelParameter);

            var loggerParameter = new AdoNetAppenderParameter
            {
                DbType = DbType.String,
                Size = 255,
                ParameterName = Constants.logger,
                Layout = new Layout2RawLayoutAdapter(GetConversionPatternLayout("logger"))
            };
            adoNetAppender.AddParameter(loggerParameter);

            var messageParameter = new AdoNetAppenderParameter
            {
                DbType = DbType.String,
                Size = 4000,
                ParameterName = Constants.message,
                Layout = new Layout2RawLayoutAdapter(GetConversionPatternLayout("message"))
            };
            adoNetAppender.AddParameter(messageParameter);

            var exceptionParameter = new AdoNetAppenderParameter
            {
                DbType = DbType.String,
                Size = 2000,
                ParameterName = Constants.exception,
                Layout = new Layout2RawLayoutAdapter(GetConversionPatternLayout("exception"))
            };
            adoNetAppender.AddParameter(exceptionParameter);

            var hostNameParameter = new AdoNetAppenderParameter
            {
                DbType = DbType.String,
                Size = 1000,
                ParameterName = Constants.machineName,
                Layout = new Layout2RawLayoutAdapter(GetConversionPatternLayout("property{log4net:HostName}"))
            };
            adoNetAppender.AddParameter(hostNameParameter);

            var domainParameter = new AdoNetAppenderParameter
            {
                DbType = DbType.String,
                Size = 1000,
                ParameterName = Constants.domain,
                Layout = new Layout2RawLayoutAdapter(GetConversionPatternLayout("appdomain"))
            };
            adoNetAppender.AddParameter(domainParameter);

            var identityParameter = new AdoNetAppenderParameter
            {
                DbType = DbType.String,
                Size = 1000,
                ParameterName = Constants.identity,
                Layout = new Layout2RawLayoutAdapter(GetConversionPatternLayout("identity"))
            };
            adoNetAppender.AddParameter(identityParameter);

            var locationParameter = new AdoNetAppenderParameter
            {
                DbType = DbType.String,
                Size = 1000,
                ParameterName = Constants.location,
                Layout = new Layout2RawLayoutAdapter(GetConversionPatternLayout("location"))
            };
            adoNetAppender.AddParameter(locationParameter);

            var methodParameter = new AdoNetAppenderParameter
            {
                DbType = DbType.String,
                Size = 500,
                ParameterName = Constants.method,
                Layout = new Layout2RawLayoutAdapter(GetConversionPatternLayout("method"))
            };
            adoNetAppender.AddParameter(methodParameter);

            var stacktraceParameter = new AdoNetAppenderParameter
            {
                DbType = DbType.String,
                Size = -1,
                ParameterName = Constants.stacktrace,
                Layout = new Layout2RawLayoutAdapter(GetConversionPatternLayout("stacktracedetail{2}"))
            };
            adoNetAppender.AddParameter(stacktraceParameter);

            //var usernameParameter = new AdoNetAppenderParameter
            //{
            //    DbType = DbType.String,
            //    Size = 300,
            //    ParameterName = Constants.username,
            //    Layout = new Layout2RawLayoutAdapter(GetConversionPatternLayout("username"))
            //};
            //adoNetAppender.AddParameter(usernameParameter);

            return adoNetAppender;

        }

        #region Methods

        public void Exception(string message, Exception exception, string tenantName, string userInfo = "", object data = null)
        {
            message = message?.Replace('\n', '_')?.Replace('\r', '_');

            _logger.Error(message, exception);

            string parameterData = JsonConvert.SerializeObject(data == null ? "" : data);

            JObject jsonObject = null;
            if (!String.IsNullOrEmpty(parameterData))
            {
                jsonObject = JObject.Parse(parameterData);
                RemoveNullProperties(jsonObject);
            }

            telemetry.TrackException(exception, new Dictionary<string, string>
            {
                { "message", message },
                { "data", parameterData },
                { "SubDomain", string.IsNullOrEmpty(tenantName) ? "" : tenantName },
                { "UserInfo", string.IsNullOrEmpty(userInfo) ? "" : userInfo },
            });
            telemetry.Flush();

            //---Send email notification to team
            NotifyException.SendExceptionOccured(message, exception, tenantName, JsonConvert.SerializeObject(jsonObject, Formatting.Indented));
        }
        public void Info(Guid id, string screenName, string action, string data)
        {
            screenName = screenName?.Replace('\n', '_')?.Replace('\r', '_');
            action = action?.Replace('\n', '_')?.Replace('\r', '_');
            data = data?.Replace('\n', '_')?.Replace('\r', '_');

            _logger.Info("ID=  " + id + ", ScreenName=  " + screenName + ", Action=  " + action + ", Data=  " + data);
        }


        public void Message(string message)
        {
            message = message?.Replace('\n', '_')?.Replace('\r', '_');
            _logger.Error(message);
        }

        public void Info(string message)
        {
            message = message?.Replace('\n', '_')?.Replace('\r', '_');
            _logger.Info(message);
        }

        public void Progress(string message)
        {
            message = message?.Replace('\n', '_')?.Replace('\r', '_');
            _logger.Info(message);
        }

        public void Debug(string message)
        {
            message = message?.Replace('\n', '_')?.Replace('\r', '_');
            _logger.Debug(message);
        }

        public void Warning(string message)
        {
            message = message?.Replace('\n', '_')?.Replace('\r', '_');
            _logger.Warn(message);
        }

        public void Error(string message)
        {
            message = message?.Replace('\n', '_')?.Replace('\r', '_');
            _logger.Error(message);
        }

        #endregion

        /// <summary>
        /// Method to remove null tuples from json data
        /// </summary>
        /// <param name="jsonObject"></param>
        private static void RemoveNullProperties(JObject jsonObject)
        {
            foreach (JProperty property in jsonObject.Properties().ToList())
            {
                if (property.Value.Type == JTokenType.Null)
                {
                    property.Remove();
                }
                else if (property.Value.Type == JTokenType.Object)
                {
                    RemoveNullProperties((JObject)property.Value);
                    if (!((JObject)property.Value).HasValues)
                    {
                        property.Remove();
                    }
                }
                else if (property.Value.Type == JTokenType.Array)
                {
                    foreach (JToken item in property.Value.Children().ToList())
                    {
                        if (item.Type == JTokenType.Object)
                        {
                            RemoveNullProperties((JObject)item);
                            if (!((JObject)item).HasValues)
                            {
                                item.Remove();
                            }
                        }
                    }

                    if (!property.Value.HasValues)
                    {
                        property.Remove();
                    }
                }
            }
        }

        #region Dispose

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // Protected implementation of Dispose pattern.
        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                _logger.Logger.Repository.Shutdown();
            }
            disposed = true;
        }

        #endregion
    }
}
