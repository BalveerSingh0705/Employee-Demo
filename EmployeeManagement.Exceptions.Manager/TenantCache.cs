//using System;
//using System.Collections.Generic;
//using System.Configuration;
//using System.Data.SqlClient;
//using System.Data;
//using System.Linq;
//using System.Runtime;
//using System.Text;
//using System.Threading.Tasks;
//using EmployeeManagement.Exceptions.Manager;
//using AIMS.Common.Configuration.Properties;
//using AIMS.Common.DataObjects;
//using AIMS.Common.Helper;
//using AIMS.Common.Security;
//using AIMS.Edmx.Database.Master;
//using AIMS.Edmx.Database.Tenant;
//using AIMS.Exceptions.Manager;
//using System;
//using System.Collections.Generic;
//using System.Configuration;
//using System.Data;
//using System.Data.SqlClient;
//using System.Linq;
//namespace AIMS.Exceptions.Manager
//{
//    public static class TenantCache
//    {
//        #region Private Variables

//        private static readonly string _edmxPrefix = "_EDMX";
//        private static readonly string _sqlPrefix = "_SQL";
//        private static readonly string _tenantMappingPrefix = "_TMAP";
//        private static readonly string _subDomainMappingPrefix = "_SMAP";
//        private static readonly string _isMultiTenant = Convert.ToString(ConfigurationManager.AppSettings["IsMultiTenant"]);

//        #endregion

//        #region POST Methods

//        /// <summary>
//        /// To save TenantId and its EDMX dbconnection in cache.
//        /// </summary>
//        /// <param name="tenantId">Tenant Id from Master database</param>
//        /// <param name="dbConnection">EDMX Connection string</param>
//        /// <returns>True if saved successfully.</returns>


//        /// <summary>
//        /// To save TenantId and its SQL dbconnection in cache.
//        /// </summary>
//        /// <param name="tenantId">Tenant Id from Master database</param>
//        /// <param name="dbConnection">SQL Db Connection string</param>
//        /// <returns>True if saved successfully.</returns>
//        private static bool SaveSqlDbConnectionMappingToTenantIdInCache(Guid tenantId, string dbConnection)
//        {
//            try
//            {
//                // Check application is cloud enabled.
//                if (Settings.Default.IsCloudEnabled)
//                {
//                    //--Encrypting database connection and saving it.
//                    RedisCacheManager.SetString(RedisCacheManager.Database, tenantId + _sqlPrefix, EncryptionHelper.Encrypt(dbConnection));
//                }

//                return true;
//            }
//            catch (Exception ex)
//            {
//                using (LogException _error = new LogException(typeof(TenantCache)))
//                {
//                    var subdomain = TenantCache.GetSubDomainByTenantId(tenantId);
//                    _error.Exception("SaveSqlDbConnectionMappingToTenantIdInCache", ex, subdomain, "", dbConnection);
//                }

//                return false;
//            }
//        }

//        /// <summary>
//        /// To save Tenant and subdomain mapping in cache.
//        /// </summary>
//        /// <param name="tenantId">Tenant Id from Master database</param>
//        /// <param name="subDomainName">Domain Name</param>
//        /// <returns>True if saved successfully.</returns>
//        private static bool SaveTenantIdMappingToSubDomainInCache(Guid tenantId, string subDomainName)
//        {
//            try
//            {
//                // Check application is cloud enabled.
//                if (Settings.Default.IsCloudEnabled)
//                {
//                    RedisCacheManager.SetString(RedisCacheManager.Database, subDomainName + _tenantMappingPrefix, tenantId.ToString());
//                }
//                return true;
//            }
//            catch (Exception ex)
//            {
//                using (LogException _error = new LogException(typeof(TenantCache)))
//                {
//                    var subdomain = TenantCache.GetSubDomainByTenantId(tenantId);
//                    _error.Exception("SaveTenantIdMappingToSubDomainInCache", ex, subdomain, "", subDomainName);
//                }

//                return false;
//            }
//        }

//        /// <summary>
//        /// To Save SabDomain and Tenant mapping ,To get subdomain by Tenant Id
//        /// </summary>
//        /// <param name="tenantId"></param>
//        /// <param name="subDomainName"></param>
//        /// <returns>True if saved successfully</returns>
//        private static bool SaveSubDomainMappingToTenantIdInCache(Guid tenantId, string subDomainName)
//        {
//            try
//            {
//                // Check application is cloud enabled.
//                if (Settings.Default.IsCloudEnabled)
//                {
//                    RedisCacheManager.SetString(RedisCacheManager.Database, tenantId + _subDomainMappingPrefix, subDomainName);
//                }

//                return true;
//            }
//            catch (Exception ex)
//            {
//                using (LogException _error = new LogException(typeof(TenantCache)))
//                {
//                    var subdomain = TenantCache.GetSubDomainByTenantId(tenantId);
//                    _error.Exception("SaveSubDomainMappingToTenantIdInCache", ex, subdomain, "", subDomainName);
//                }

//                return false;
//            }
//        }

//        #endregion

//        //#region GET Methods

//        ///// <summary>
//        ///// To Get EDMX connection string from cache.
//        ///// </summary>
//        ///// <param name="tenantId">Tenant Id from Master database</param>
//        ///// <returns>Connection string.</returns>
//        //public static string GetEdmxDbConnectionFromCacheByTenantId(Guid? tenantId)
//        //{
//        //    if (tenantId == null || Convert.ToString(tenantId).Equals(string.Empty))
//        //    {
//        //        tenantId = Guid.Empty;
//        //    }
//        //    try
//        //    {
//        //        // Check application is cloud enabled.
//        //        if (Settings.Default.IsCloudEnabled)
//        //        {
//        //            string dbConnection = RedisCacheManager.GetString(RedisCacheManager.Database, tenantId + _edmxPrefix);

//        //            if (!string.IsNullOrEmpty(dbConnection))
//        //            {
//        //                //--Decrypting database connection
//        //                return EncryptionHelper.Decrypt(dbConnection);
//        //            }
//        //        }
//        //    }
//        //    catch (Exception ex)
//        //    {
//        //        using (LogException _error = new LogException(typeof(TenantCache)))
//        //        {
//        //            var subdomain = TenantCache.GetSubDomainByTenantId((Guid)tenantId);
//        //            _error.Exception("GetEdmxDbConnectionFromCacheByTenantId", ex, subdomain, "", tenantId);
//        //        }
//        //    }

//        //    return GetEdmxConnectionFromDatabaseByTenantId(tenantId);
//        //}

//        ///// <summary>
//        ///// To Get SQL Connection string of Tenant from cache.
//        ///// </summary>
//        ///// <param name="tenantId">Tenant Id from Master database</param>
//        ///// <returns>Connection string.</returns>
//        //public static string GetSqlDbConnectionFromCacheByTenantId(Guid? tenantId)
//        //{
//        //    if (tenantId == null || Convert.ToString(tenantId).Equals(string.Empty))
//        //    {
//        //        tenantId = Guid.Empty;
//        //    }
//        //    try
//        //    {
//        //        // Check application is cloud enabled.
//        //        if (Settings.Default.IsCloudEnabled)
//        //        {
//        //            string dbConnection = RedisCacheManager.GetString(RedisCacheManager.Database, tenantId + _sqlPrefix);

//        //            if (!string.IsNullOrEmpty(dbConnection))
//        //            {
//        //                //--Decrypting database connection
//        //                return EncryptionHelper.Decrypt(dbConnection);
//        //            }
//        //        }

//        //    }
//        //    catch (Exception ex)
//        //    {
//        //        using (LogException _error = new LogException(typeof(TenantCache)))
//        //        {
//        //            var subdomain = TenantCache.GetSubDomainByTenantId((Guid)tenantId);
//        //            _error.Exception("GetSqlDbConnectionFromCacheByTenantId", ex, subdomain, "", tenantId);
//        //        }
//        //    }

//        //    return GetSqlDbConnectionFromDatabaseByTenantId(tenantId);
//        //}

//        ///// <summary>
//        ///// To Get Tenant and its dbconnection from cache.
//        ///// </summary>
//        ///// <param name="subDomain">Sub Domain from Master database</param>
//        ///// <returns>Connection string.</returns>
//        //public static string GetSqlDbConnectionOfTenantFromCacheBySubDomain(string subDomain)
//        //{
//        //    try
//        //    {
//        //        // Check application is cloud enabled.
//        //        if (Settings.Default.IsCloudEnabled)
//        //        {
//        //            string dbConnection = RedisCacheManager.GetString(RedisCacheManager.Database, subDomain);
//        //            if (!string.IsNullOrEmpty(dbConnection))
//        //            {
//        //                //--Decrypting database connection
//        //                return EncryptionHelper.Decrypt(dbConnection);
//        //            }
//        //        }
//        //    }
//        //    catch (Exception ex)
//        //    {
//        //        using (LogException _error = new LogException(typeof(TenantCache)))
//        //        {
//        //            //var subdomain = TenantCache.GetSubDomainByTenantId((Guid)tenantId);
//        //            _error.Exception("GetSqlDbConnectionOfTenantFromCacheBySubDomain", ex, subDomain, "", subDomain);
//        //        }
//        //    }

//        //    return GetSqlDbConnectionFromDatabaseBySubDomain(subDomain);

//        //}

//        ///// <summary>
//        ///// To get Tenant id from subdomain
//        ///// </summary>
//        ///// <param name="subDomain">SubDomain</param>
//        ///// <returns>Tenant Id</returns>
//        //public static Guid? GetTenantIdBySubDomain(string subDomain)
//        //{
//        //    Guid tenantId = Guid.Empty;

//        //    try
//        //    {
//        //        // Check application is cloud enabled.
//        //        if (Settings.Default.IsCloudEnabled)
//        //        {
//        //            string tenantIdFromRedish = RedisCacheManager.GetString(RedisCacheManager.Database, subDomain + _tenantMappingPrefix);

//        //            // Make sure it has data before we parse to guid if not it will be default empty.
//        //            if (!string.IsNullOrEmpty(tenantIdFromRedish))
//        //            {
//        //                tenantId = Guid.Parse(tenantIdFromRedish);
//        //            }
//        //        }

//        //        // Check of application is Multitenant enabled.

//        //        if (!string.IsNullOrEmpty(_isMultiTenant) && _isMultiTenant.ToLower().Equals("true"))
//        //        {
//        //            // Check if its empty or null then only get the data from master database for specific domain.
//        //            if (tenantId == Guid.Empty || tenantId == null)
//        //            {
//        //                using (MasterDbContext _db = new MasterDbContext())
//        //                {
//        //                    var configurationDetails = _db.usp_GetTenantConfigurationDetailsBySubDomain(subDomain).FirstOrDefault();
//        //                    if (configurationDetails != null)
//        //                    {
//        //                        tenantId = Guid.Parse(configurationDetails.TenantId.ToString());
//        //                        SaveTenantIdMappingToSubDomainInCache(tenantId, subDomain);
//        //                    }
//        //                    return tenantId;
//        //                }
//        //            }
//        //        }
//        //    }
//        //    catch (Exception ex)
//        //    {
//        //        using (LogException _error = new LogException(typeof(TenantCache)))
//        //        {
//        //            _error.Exception("GetTenantIdBySubDomain", ex, subDomain, "", tenantId);
//        //        }
//        //    }
//        //    return tenantId;
//        //}

//        ///// <summary>
//        ///// To get subdomain from Tenant Id
//        ///// </summary>
//        ///// <param name="subDomain">SubDomain</param>
//        ///// <returns>Tenant Id</returns>
//        //public static string GetSubDomainByTenantId(Guid tenantId)
//        //{
//        //    string subDomain = string.Empty;
//        //    try
//        //    {
//        //        // Check application is cloud enabled.
//        //        if (Settings.Default.IsCloudEnabled)
//        //        {
//        //            subDomain = RedisCacheManager.GetString(RedisCacheManager.Database, tenantId + _subDomainMappingPrefix);
//        //        }

//        //        // If subdomain is not available
//        //        if (string.IsNullOrEmpty(subDomain))
//        //        {
//        //            if (!string.IsNullOrEmpty(_isMultiTenant) && _isMultiTenant.ToLower().Equals("true"))
//        //            {
//        //                using (MasterDbContext _db = new MasterDbContext())
//        //                {
//        //                    var configurationDetails = _db.usp_GetConfigurationDetailsById(tenantId.ToString()).ToList();

//        //                    if (configurationDetails.Count() > 0)
//        //                    {
//        //                        Dictionary<string, string> dict = configurationDetails.ToDictionary(x => x.ConfigKey, x => x.Value);

//        //                        // Save SubDomain and TenantId mapping in cache.
//        //                        SaveSubDomainMappingToTenantIdInCache(tenantId, dict["SubDomain"]);

//        //                        // Return subdomain.
//        //                        subDomain = dict["SubDomain"];
//        //                    }
//        //                }
//        //            }
//        //            else
//        //            {
//        //                //Get SubDomain config from Configuration
//        //                subDomain = Convert.ToString(ConfigurationManager.AppSettings["SubDomain"]);

//        //                tenantId = Guid.Empty;
//        //                // Save SubDomain and TenantId mapping in cache.
//        //                SaveSubDomainMappingToTenantIdInCache(tenantId, subDomain);

//        //            }
//        //        }

//        //        return subDomain;
//        //    }
//        //    catch (Exception ex)
//        //    {
//        //        using (LogException _error = new LogException(typeof(TenantCache)))
//        //        {
//        //            _error.Exception("GetSubDomainByTenantId", ex, Convert.ToString(tenantId), "", subDomain);
//        //        }
//        //        return string.Empty;
//        //    }
//        //}

//        ///// <summary>
//        ///// To delete all keys from cache
//        ///// </summary>
//        ///// <returns></returns>
//        //public static bool DeleteAllKeyValuesFromCache()
//        //{
//        //    try
//        //    {
//        //        RedisCacheManager.DeleteAllKeys(RedisCacheManager.Database);

//        //        return true;
//        //    }
//        //    catch (Exception ex)
//        //    {
//        //        using (LogException _error = new LogException(typeof(TenantCache)))
//        //        {
//        //            //var subdomain = TenantCache.GetSubDomainByTenantId(tenantId);
//        //            _error.Exception("Exception In DeleteAllKeyValuesFromCache", ex, "");
//        //        }

//        //        return false;
//        //    }
//        //}

//        //public static string GetConfigValueByTenantId(Guid? tenantId, string key)
//        //{
//        //    string keyVal = string.Empty;
//        //    Dictionary<string, string> dict = new Dictionary<string, string>();
//        //    if (tenantId == null || Convert.ToString(tenantId).Equals(string.Empty))
//        //    {
//        //        tenantId = Guid.Empty;
//        //    }
//        //    try
//        //    {
//        //        if (!string.IsNullOrEmpty(_isMultiTenant) && _isMultiTenant.ToLower().Equals("true"))
//        //        {
//        //            dict = GetConfigurationByTenantId(tenantId, out DataTable dataTable);

//        //            if (dict.Count > 0)
//        //            {
//        //                var connectionString = EdmxConnectionHelper.BuildEdmxConnectionStringUsingProperties(edmxConnectionModel: new ConnectionPropertiesData()
//        //                {
//        //                    DataSource = dict["DataSource"],
//        //                    InitialCatalog = dict["InitialCatalog"],
//        //                    UserId = dict["UserId"],
//        //                    Password = dict["Password"]
//        //                });

//        //                //Get the Tenant Configuration From tenant Database
//        //                using (TenantDbContext _tenantdb = new TenantDbContext(connectionString))
//        //                {
//        //                    var tenantConfigurationDetails = _tenantdb.usp_GetConfigurationDetails().ToList();

//        //                    if (tenantConfigurationDetails.Count() > 0)
//        //                    {
//        //                        var dicTenantSpecificConfig = new Dictionary<string, string>();
//        //                        dicTenantSpecificConfig = tenantConfigurationDetails.ToDictionary(x => x.ConfigKey, x => x.Value);

//        //                        if (dicTenantSpecificConfig.Count > 0)
//        //                        {
//        //                            dicTenantSpecificConfig.ToList().ForEach(k =>
//        //                            {
//        //                                dict.Add(k.Key, k.Value);
//        //                            });
//        //                        }
//        //                    }
//        //                }
//        //            }
//        //        }
//        //        else
//        //        {
//        //            var singleTenantConfigurationDetails = new List<usp_GetConfigurationDetails_Result>();
//        //            using (TenantDbContext _db = new TenantDbContext())
//        //            {
//        //                singleTenantConfigurationDetails = _db.usp_GetConfigurationDetails().ToList();

//        //                if (singleTenantConfigurationDetails.Count() > 0)
//        //                {
//        //                    dict = singleTenantConfigurationDetails.ToDictionary(x => x.ConfigKey, x => x.Value);
//        //                }
//        //            }
//        //        }

//        //        keyVal = dict[key];
//        //    }
//        //    catch (Exception)
//        //    {

//        //    }

//        //    return keyVal;
//        //}

//        //public static string GetConfigValueBySubDomainName(string subDomainName, string key)
//        //{
//        //    string keyVal = string.Empty;
//        //    Dictionary<string, string> dict = new Dictionary<string, string>();

//        //    try
//        //    {
//        //        if (!string.IsNullOrEmpty(_isMultiTenant) && _isMultiTenant.ToLower().Equals("true"))
//        //        {
//        //            dict = GetConfigurationBySubDomain(subDomainName, out DataTable dataTable);

//        //            if (dict.Count > 0)
//        //            {
//        //                var connectionString = EdmxConnectionHelper.BuildEdmxConnectionStringUsingProperties(edmxConnectionModel: new ConnectionPropertiesData()
//        //                {
//        //                    DataSource = dict["DataSource"],
//        //                    InitialCatalog = dict["InitialCatalog"],
//        //                    UserId = dict["UserId"],
//        //                    Password = dict["Password"]
//        //                });

//        //                //Get the Tenant Configuration From tenant Database
//        //                using (TenantDbContext _tenantdb = new TenantDbContext(connectionString))
//        //                {
//        //                    var tenantConfigurationDetails = _tenantdb.usp_GetConfigurationDetails().ToList();

//        //                    if (tenantConfigurationDetails.Count() > 0)
//        //                    {
//        //                        var dicTenantSpecificConfig = new Dictionary<string, string>();
//        //                        dicTenantSpecificConfig = tenantConfigurationDetails.ToDictionary(x => x.ConfigKey, x => x.Value);

//        //                        if (dicTenantSpecificConfig.Count > 0)
//        //                        {
//        //                            dicTenantSpecificConfig.ToList().ForEach(k =>
//        //                            {
//        //                                dict.Add(k.Key, k.Value);
//        //                            });
//        //                        }
//        //                    }
//        //                }
//        //            }
//        //        }
//        //        else
//        //        {
//        //            var singleTenantConfigurationDetails = new List<usp_GetConfigurationDetails_Result>();
//        //            using (TenantDbContext _db = new TenantDbContext())
//        //            {
//        //                singleTenantConfigurationDetails = _db.usp_GetConfigurationDetails().ToList();

//        //                if (singleTenantConfigurationDetails.Count() > 0)
//        //                {
//        //                    dict = singleTenantConfigurationDetails.ToDictionary(x => x.ConfigKey, x => x.Value);
//        //                }
//        //            }
//        //        }

//        //        keyVal = dict[key];
//        //    }
//        //    catch (Exception)
//        //    {

//        //    }

//        //    return keyVal;
//        //}

//        //#endregion

//        //#region Private Methods

//        //private static string GetEdmxConnectionFromDatabaseByTenantId(Guid? tenantId)
//        //{
//        //    string connectionString = string.Empty;
//        //    if (tenantId == null || Convert.ToString(tenantId).Equals(string.Empty))
//        //    {
//        //        tenantId = Guid.Empty;
//        //    }

//        //    try
//        //    {
//        //        // Check if application is set to multitenant mode.
//        //        if (!string.IsNullOrEmpty(_isMultiTenant) && _isMultiTenant.ToLower().Equals("true"))
//        //        {
//        //            Dictionary<string, string> dict = GetConfigurationByTenantId(tenantId, out DataTable dataTable);

//        //            if (dict.Count > 0)
//        //            {
//        //                //--Create connection string
//        //                connectionString = EdmxConnectionHelper.BuildEdmxConnectionStringUsingProperties(edmxConnectionModel: new ConnectionPropertiesData()
//        //                {
//        //                    DataSource = dict["DataSource"],
//        //                    InitialCatalog = dict["InitialCatalog"],
//        //                    UserId = dict["UserId"],
//        //                    Password = dict["Password"]
//        //                });

//        //                Guid TenantId = Guid.Parse(dataTable.Rows[0]["TenantId"].ToString());
//        //                //--Save in cache
//        //                SaveEdmxDbConnectionMappingToTenantIdInCache(TenantId, connectionString);
//        //            }

//        //        }
//        //        else
//        //        {
//        //            // If application is not multitenant and not cloud enabled i.e. on-premises single tenant
//        //            // Then use connections from web.config file

//        //            connectionString = ConfigurationManager.ConnectionStrings["TenantDbContext"].ConnectionString;

//        //            //--Save in cache
//        //            SaveEdmxDbConnectionMappingToTenantIdInCache(Guid.Empty, connectionString);
//        //        }

//        //    }
//        //    catch (Exception ex)
//        //    {
//        //        using (LogException _error = new LogException(typeof(TenantCache)))
//        //        {
//        //            var subdomain = TenantCache.GetSubDomainByTenantId((Guid)tenantId);
//        //            _error.Exception("GetEdmxConnectionFromDatabaseByTenantId", ex, subdomain, "", connectionString);
//        //        }
//        //    }

//        //    return connectionString;
//        //}

//        //public static string GetSqlDbConnectionFromDatabaseByTenantId(Guid? tenantId)
//        //{
//        //    string connectionString = string.Empty;
//        //    if (tenantId == null || Convert.ToString(tenantId).Equals(string.Empty))
//        //    {
//        //        tenantId = Guid.Empty;
//        //    }

//        //    try
//        //    {
//        //        // Check if application is set to multitenant mode.
//        //        if (!string.IsNullOrEmpty(_isMultiTenant) && _isMultiTenant.ToLower().Equals("true"))
//        //        {
//        //            DataTable dataTable;
//        //            Dictionary<string, string> dict = GetConfigurationByTenantId(tenantId, out dataTable);

//        //            if (dict.Count > 0)
//        //            {
//        //                //--Create connection string
//        //                connectionString = EdmxConnectionHelper.BuildSqlConnectionStringUsingProperties(edmxConnectionModel: new ConnectionPropertiesData()
//        //                {
//        //                    DataSource = dict["DataSource"],
//        //                    InitialCatalog = dict["InitialCatalog"],
//        //                    UserId = dict["UserId"],
//        //                    Password = dict["Password"]
//        //                });

//        //                Guid TenantId = Guid.Parse(dataTable.Rows[0]["TenantId"].ToString());

//        //                // Save connection in cache
//        //                SaveSqlDbConnectionMappingToTenantIdInCache(TenantId, connectionString);
//        //            }
//        //        }
//        //        else
//        //        {
//        //            // If application is not multitenant and not cloud enabled i.e. on-premises single tenant
//        //            // Then use connections from web.config file
//        //            connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

//        //            //--Save in cache
//        //            SaveEdmxDbConnectionMappingToTenantIdInCache(Guid.Empty, connectionString);
//        //        }

//        //    }
//        //    catch (Exception ex)
//        //    {
//        //        using (LogException _error = new LogException(typeof(TenantCache)))
//        //        {
//        //            var subdomain = TenantCache.GetSubDomainByTenantId((Guid)tenantId);
//        //            _error.Exception("GetSqlDbConnectionFromDatabaseByTenantId", ex, subdomain, "", connectionString);
//        //        }
//        //    }

//        //    return connectionString;
//        //}

//        //private static Dictionary<string, string> GetConfigurationByTenantId(Guid? tenantId, out DataTable dataTable)
//        //{
//        //    DataSet ds = new DataSet();
//        //    dataTable = new DataTable();
//        //    string consString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
//        //    using (SqlConnection conn = new SqlConnection(consString))
//        //    {
//        //        using (SqlCommand cmd = new SqlCommand("usp_GetConfigurationDetailsById", conn))
//        //        {
//        //            cmd.CommandTimeout = 60;
//        //            SqlDataAdapter adapt = new SqlDataAdapter(cmd);
//        //            adapt.SelectCommand.CommandType = CommandType.StoredProcedure;
//        //            adapt.SelectCommand.Parameters.Add(new SqlParameter("@TenantId", SqlDbType.NVarChar, 100));
//        //            adapt.SelectCommand.Parameters["@TenantId"].Value = tenantId.ToString();
//        //            adapt.Fill(ds);
//        //        }
//        //    }

//        //    dataTable = ds.Tables[0].Copy();

//        //    dataTable.AsEnumerable().Where(e => e.Field<bool>("IsEncrypted") == true).ToList()
//        //            .ForEach(o => o.SetField("Value", EncryptionHelper.SafeDecrypt(o["Value"].ToString())));

//        //    Dictionary<string, string> dict = new Dictionary<string, string>();
//        //    dataTable.AsEnumerable().ToList().ForEach(o => dict.Add(o.Field<string>("ConfigKey"), o.Field<string>("Value")));
//        //    return dict;
//        //}

//        //private static string GetSqlDbConnectionFromDatabaseBySubDomain(string subDomain)
//        //{
//        //    string connectionString = string.Empty;

//        //    try
//        //    {
//        //        // Check if application is set to multitenant mode.
//        //        if (!string.IsNullOrEmpty(_isMultiTenant) && _isMultiTenant.ToLower().Equals("true"))
//        //        {
//        //            Dictionary<string, string> dict = GetConfigurationBySubDomain(subDomain, out DataTable dataTable);

//        //            if (dict.Count > 0)
//        //            {
//        //                //--Create connection string
//        //                connectionString = SqlConnectionHelper.BuildSQLConnectionString(connectionPropertiesModel: new ConnectionPropertiesData()
//        //                {
//        //                    DataSource = dict["DataSource"],
//        //                    InitialCatalog = dict["InitialCatalog"],
//        //                    UserId = dict["UserId"],
//        //                    Password = dict["Password"]
//        //                });

//        //                //--Save Tenant and Subdomain mapping in cache

//        //                Guid tenantId = Guid.Parse(dataTable.Rows[0]["TenantId"].ToString());
//        //                SaveTenantIdMappingToSubDomainInCache(tenantId, subDomain);

//        //                // Save connection in cache
//        //                SaveSqlDbConnectionMappingToTenantIdInCache(tenantId, connectionString);
//        //            }
//        //        }
//        //        else
//        //        {

//        //            connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

//        //            SaveTenantIdMappingToSubDomainInCache(Guid.Empty, subDomain);

//        //            // Save connection in cache
//        //            SaveSqlDbConnectionMappingToTenantIdInCache(Guid.Empty, connectionString);

//        //        }
//        //    }
//        //    catch (Exception ex)
//        //    {
//        //        using (LogException _error = new LogException(typeof(TenantCache)))
//        //        {
//        //            _error.Exception("GetSqlDbConnectionFromDatabaseBySubDomain", ex, subDomain, "", connectionString);
//        //        }
//        //    }

//        //    return connectionString;
//        //}

//        //private static Dictionary<string, string> GetConfigurationBySubDomain(string subDomain, out DataTable dataTable)
//        //{
//        //    Dictionary<string, string> dict = new Dictionary<string, string>();

//        //    DataSet ds = new DataSet();
//        //    dataTable = new DataTable();
//        //    string consString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
//        //    using (SqlConnection conn = new SqlConnection(consString))
//        //    {
//        //        using (SqlCommand cmd = new SqlCommand("usp_GetTenantConfigurationDetailsBySubDomain", conn))
//        //        {
//        //            cmd.CommandTimeout = 60;
//        //            SqlDataAdapter adapt = new SqlDataAdapter(cmd);
//        //            adapt.SelectCommand.CommandType = CommandType.StoredProcedure;
//        //            adapt.SelectCommand.Parameters.Add(new SqlParameter("@Subdomain", SqlDbType.NVarChar, 100));
//        //            adapt.SelectCommand.Parameters["@Subdomain"].Value = subDomain;
//        //            adapt.Fill(ds);
//        //        }
//        //    }

//        //    dataTable = ds.Tables[0].Copy();

//        //    dataTable.AsEnumerable().Where(e => e.Field<bool>("IsEncrypted") == true).ToList()
//        //            .ForEach(o => o.SetField("Value", EncryptionHelper.SafeDecrypt(o["Value"].ToString())));

//        //    dataTable.AsEnumerable().ToList().ForEach(o => dict.Add(o.Field<string>("ConfigKey"), o.Field<string>("Value")));

//        //    return dict;
//        //}

//        //public static Dictionary<string, string> GetTenantConfigFromDatabaseByTenantId(Guid? tenantId)
//        //{
//        //    var dicTenantConfig = new Dictionary<string, string>();
//        //    if (tenantId == null || Convert.ToString(tenantId).Equals(string.Empty))
//        //    {
//        //        tenantId = Guid.Empty;
//        //    }
//        //    try
//        //    {
//        //        // Check if application is set to multitenant mode.
//        //        if (!string.IsNullOrEmpty(_isMultiTenant) && _isMultiTenant.ToLower().Equals("true"))
//        //        {
//        //            dicTenantConfig = GetConfigurationByTenantId(tenantId, out DataTable dataTable);

//        //            if (dicTenantConfig.Count > 0)
//        //            {
//        //                var connectionString = EdmxConnectionHelper.BuildEdmxConnectionStringUsingProperties(edmxConnectionModel: new ConnectionPropertiesData()
//        //                {
//        //                    DataSource = dicTenantConfig["DataSource"],
//        //                    InitialCatalog = dicTenantConfig["InitialCatalog"],
//        //                    UserId = dicTenantConfig["UserId"],
//        //                    Password = dicTenantConfig["Password"]
//        //                });

//        //                //Get the Tenant Configuration From tenant Database
//        //                using (TenantDbContext _tenantdb = new TenantDbContext(connectionString))
//        //                {
//        //                    var tenantConfigurationDetails = _tenantdb.usp_GetConfigurationDetails().ToList();

//        //                    if (tenantConfigurationDetails.Count() > 0)
//        //                    {
//        //                        var dicTenantSpecificConfig = new Dictionary<string, string>();
//        //                        dicTenantSpecificConfig = tenantConfigurationDetails.ToDictionary(x => x.ConfigKey, x => x.Value);

//        //                        if (dicTenantSpecificConfig.Count > 0)
//        //                        {
//        //                            dicTenantSpecificConfig.ToList().ForEach(k =>
//        //                            {
//        //                                dicTenantConfig.Add(k.Key, k.Value);
//        //                            });
//        //                        }
//        //                    }
//        //                }
//        //            }

//        //        }
//        //        else
//        //        {
//        //            // If application is not multitenant and not cloud enabled i.e. on-premises single tenant
//        //            // Then use connections from web.config file
//        //            using (TenantDbContext _db = new TenantDbContext())
//        //            {
//        //                var configurationDetails = _db.usp_GetConfigurationDetails().ToList();

//        //                if (configurationDetails.Count() > 0)
//        //                {
//        //                    dicTenantConfig = configurationDetails.ToDictionary(x => x.ConfigKey, x => Convert.ToString(x.Value).Trim());
//        //                }
//        //            }
//        //        }

//        //    }
//        //    catch (Exception ex)
//        //    {
//        //        using (LogException _error = new LogException(typeof(TenantCache)))
//        //        {
//        //            var subdomain = TenantCache.GetSubDomainByTenantId((Guid)tenantId);
//        //            _error.Exception("GetTenantConfigFromDatabaseByTenantId", ex, subdomain, "", dicTenantConfig);
//        //        }

//        //    }
//        //    return dicTenantConfig;
//        //}

//        //#endregion
//    }
//}
