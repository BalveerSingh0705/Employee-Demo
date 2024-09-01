using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using EmployeeManagement.Core.Common;
using EmployeeManagement.Exceptions.Manager;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using EmployeeManagement.Core.Entities;
namespace EmployeeManagement.Web.Helper
{
    /*
     The ProxyService sealed class to manage and maintain the WebAPI service communication.
     It also has al the proxy methods to communicate to service.
         */

    /// <summary>
    /// The ProxyService sealed class to manage and maintain the WebAPI service communication.
    /// It also has al the proxy methods to communicate to service.
    /// Use the Instance of this proxy class.
    /// </summary>
    public sealed class ProxyService : BaseClient, IDisposable
    {
        #region Private Variables

        private static readonly object _locker = new object();
        private static volatile ProxyService _instance;
        private static string baseAddress;
        private static string connectionString;
        private static string ServiceVirtualDirName = "https://localhost:7291/";
        BaseProxy baseProxy = BaseProxy.Instance;

        #endregion
        //https://localhost:44391
        #region Instance

        /// <summary>
        /// Creates and return the instance of ProxyService
        /// </summary>
        public static ProxyService Instance
        {
            get
            {
                // If instance is already available don't create new just return existing one.
                if (_instance != null) return _instance;

                lock (_locker)
                {
                    if (_instance == null)
                    {
                        // Create new instance if it's null
                        _instance = new ProxyService();
                    }
                }

                return _instance;
            }
        }

        #endregion

        #region Private Contructor

        private ProxyService()
        {
            baseAddress = "/" + System.Configuration.ConfigurationManager.AppSettings["ServiceBaseAddress"];
            connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["connectionStrings"].ToString();
        }

        #endregion

        #region GetTokenData
        /// <summary>
        /// The use of method is to get access token required for services. It requires user credentials data.
        /// </summary>
        /// <param name="userCredentials">The object of UserCredentialData</param>
        /// <returns>It returns the object of ServiceTokenData.</returns>
        public async Task<ServiceTokenData> GetAccessTokenAsync(UserCredentialData userCredentials)
        {
            try
            {
                using (LogException _error = new LogException(typeof(ProxyService), connectionString))
                {
                    var _client = new HttpClient();

                    _client.BaseAddress = new Uri(baseAddress);
                    _client.DefaultRequestHeaders.Accept.Clear();
                    _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    ServicePointManager.Expect100Continue = true;
                    ServicePointManager.DefaultConnectionLimit = 9999;
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                    string databaseConnection = string.Empty;

                    databaseConnection = System.Configuration.ConfigurationManager.ConnectionStrings["connectionString"].ToString();


                    //setup login data with db connection string
                    //for azure ad users pass "AD" and for form authenticateduser pass "formauth" in "userauthtype"
                    List<KeyValuePair<string, string>> keyValuePairs = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("grant_type", "password"),
                    new KeyValuePair<string, string>("username", userCredentials.Username),
                    new KeyValuePair<string, string>("dbconnection", databaseConnection),
                    new KeyValuePair<string, string>("password", userCredentials.Password)
                };

                    var formContent = new FormUrlEncodedContent(keyValuePairs);

                    //send request

                    HttpResponseMessage responseMessage = await _client.PostAsync("Token", formContent);

                    if (responseMessage.IsSuccessStatusCode)
                    {
                        return await responseMessage.Content.ReadAsAsync<ServiceTokenData>(new[] { new JsonMediaTypeFormatter() });
                    }
                }
            }
            catch (Exception ex)
            {
                using (LogException _error = new LogException(typeof(ProxyService), connectionString))
                {
                    _error.Exception("Error in GetAccessToken", ex, connectionString, userCredentials.Username, userCredentials);
                }
            }
            return new ServiceTokenData();
        }
        #endregion

        #region Private Methods

        private HttpContent CreateHttpContentFromJsonString(string data)
        {
            return new StringContent(data, Encoding.UTF8, "application/json");
        }

        #endregion

        #region IDisposable Support

        private bool _disposed; // To detect redundant calls

        // Implement IDisposable.
        // Do not make this method virtual.
        // A derived class should not be able to override this method.
        public new void Dispose()
        {
            Dispose(true);
            // This object will be cleaned up by the Dispose method.
            // Therefore, you should call GC.SupressFinalize to
            // take this object off the finalization queue
            // and prevent finalization code for this object
            // from executing a second time.
            GC.SuppressFinalize(this);
        }

        // Dispose(bool disposing) executes in two distinct scenarios.
        // If disposing equals true, the method has been called directly
        // or indirectly by a user's code. Managed and unmanaged resources
        // can be disposed.
        // If disposing equals false, the method has been called by the
        // runtime from inside the finalizer and you should not reference
        // other objects. Only unmanaged resources can be disposed.
        private new void Dispose(bool disposing)
        {
            // Check to see if Dispose has already been called.
            if (_disposed) return;
            // If disposing equals true, dispose all managed
            // and unmanaged resources.
            if (disposing)
            {
                _instance = null;
                base.Dispose();
                // Dispose managed resources.
            }

            // Call the appropriate methods to clean up
            // unmanaged resources here.
            // If disposing is false,
            // only the following code is executed.
            // Note disposing has been done.
            _disposed = true;
        }

        public static async Task<bool> CreateEmployeeAsync(EmployeeEntity employeeEntity)
        {
            bool isSuccess = false;
            try
            {
                // Ensure the URL for the API endpoint is correct.
                string url = ServiceVirtualDirName + "api/EmployeeConfiguration/SaveOrUpdateEmployeeDetails";

                // Use the BaseProxy class to perform the POST operation.
                var response = await BaseProxy.Instance.PostAsyncMethod(url, employeeEntity);

                if (response.IsSuccessStatusCode)
                {
                    // Read the response content asynchronously.
                    string result = await response.Content.ReadAsStringAsync();

                    // Deserialize the result to a boolean.
                    isSuccess = JsonConvert.DeserializeObject<bool>(result);
                }
            }
            catch (Exception ex)
            {
                // Log the exception as needed.
                //using (LogException _error = new LogException(typeof(ProxyService), TenantCache.GetSqlDbConnectionFromCacheByTenantId(employeeEntity.TenantId)))
                //{
                //    _error.Exception("Error in CreateEmployeeAsync", ex, TenantCache.GetSubDomainByTenantId((Guid)employeeEntity.TenantId), UserInfo.GetUserName(), employeeEntity);
                //}

                // Optionally rethrow the exception or handle it in a way that informs the caller
                throw;
            }
            return isSuccess;
        }

        public static async Task<List<TableFormEntity>> GetEmployeeDetailsInTableForm()
        {
            List<TableFormEntity> tableFormEntities = new List<TableFormEntity>();

            try
            {
                // Ensure the URL for the API endpoint is correct.
                string url = ServiceVirtualDirName + "api/EmployeeConfiguration/GetEmployeeDetailsInTableForm";

                // Use the BaseProxy class to perform the GET operation.
                var response = await BaseProxy.Instance.GetAsyncMethod(url);

                if (response.IsSuccessStatusCode)
                {
                    string result = await response.Content.ReadAsStringAsync();
                    tableFormEntities = JsonConvert.DeserializeObject<List<TableFormEntity>>(result);
                }
            }
            catch (Exception ex)
            {
                // Log the exception as needed.
                //using (LogException _error = new LogException(typeof(ProxyService), TenantCache.GetSqlDbConnectionFromCacheByTenantId(employeeEntity.TenantId)))
                //{
                //    _error.Exception("Error in GetEmployeeDetailsInTableForm", ex, TenantCache.GetSubDomainByTenantId((Guid)employeeEntity.TenantId), UserInfo.GetUserName(), employeeEntity);
                //}

                // Optionally rethrow the exception or handle it in a way that informs the caller
                throw;
            }

            return tableFormEntities;
        }

        public static async Task<bool> DeleteSingleEmployeeDetails(EmployeeDataInIDEntity employeeDataInIDEntity)
        {
            bool isSuccess = false;
            try
            {
                // Ensure the URL for the API endpoint is correct.
                string url = ServiceVirtualDirName + "api/EmployeeConfiguration/DeleteSingleEmployeeDetails";

                // Use the BaseProxy class to perform the POST operation.
                var response = await BaseProxy.Instance.PostAsyncMethod(url, employeeDataInIDEntity);

                if (response.IsSuccessStatusCode)
                {
                    // Read the response content asynchronously.
                    string result = await response.Content.ReadAsStringAsync();

                    // Deserialize the result to a boolean.
                    isSuccess = JsonConvert.DeserializeObject<bool>(result);
                }
            }
            catch (Exception ex)
            {
                // Log the exception as needed.
                //using (LogException _error = new LogException(typeof(ProxyService), TenantCache.GetSqlDbConnectionFromCacheByTenantId(employeeEntity.TenantId)))
                //{
                //    _error.Exception("Error in CreateEmployeeAsync", ex, TenantCache.GetSubDomainByTenantId((Guid)employeeEntity.TenantId), UserInfo.GetUserName(), employeeEntity);
                //}

                // Optionally rethrow the exception or handle it in a way that informs the caller
                throw;
            }
            return isSuccess;
        }



        public static async Task<List<EmployeeEntity>> GetEmployeeDetailsClickOnEditButton(EmployeeDataInIDEntity employeeDataInIDEntity)
        {
            List<EmployeeEntity> employeeEntity = new List<EmployeeEntity>();

            try
            {
                // Ensure the URL for the API endpoint is correct.
                string url = ServiceVirtualDirName + "api/EmployeeConfiguration/GetEmployeeDetailsClickOnEditButton";

                // Use the BaseProxy class to perform the GET operation.
                var response = await BaseProxy.Instance.PostAsyncMethod(url, employeeDataInIDEntity);

                if (response.IsSuccessStatusCode)
                {
                    string result = await response.Content.ReadAsStringAsync();
                    employeeEntity = JsonConvert.DeserializeObject<List<EmployeeEntity>>(result);
                }
            }
            catch (Exception ex)
            {
                // Log the exception as needed.
                //using (LogException _error = new LogException(typeof(ProxyService), TenantCache.GetSqlDbConnectionFromCacheByTenantId(employeeEntity.TenantId)))
                //{
                //    _error.Exception("Error in GetEmployeeDetailsInTableForm", ex, TenantCache.GetSubDomainByTenantId((Guid)employeeEntity.TenantId), UserInfo.GetUserName(), employeeEntity);
                //}

                // Optionally rethrow the exception or handle it in a way that informs the caller
                throw;
            }

            return employeeEntity;
        }

        public static async Task<bool> SaveEmployeeChangesInfo(EmployeeEntity employeeEntity)
        {
            bool isSuccess = false;
            try
            {
                // Ensure the URL for the API endpoint is correct.
                string url = ServiceVirtualDirName + "api/EmployeeConfiguration/SaveEmployeeChangesInfo";

                // Use the BaseProxy class to perform the POST operation.
                var response = await BaseProxy.Instance.PostAsyncMethod(url, employeeEntity);

                if (response.IsSuccessStatusCode)
                {
                    // Read the response content asynchronously.
                    string result = await response.Content.ReadAsStringAsync();

                    // Deserialize the result to a boolean.
                    isSuccess = JsonConvert.DeserializeObject<bool>(result);
                }
            }
            catch (Exception ex)
            {
                // Log the exception as needed.
                //using (LogException _error = new LogException(typeof(ProxyService), TenantCache.GetSqlDbConnectionFromCacheByTenantId(employeeEntity.TenantId)))
                //{
                //    _error.Exception("Error in CreateEmployeeAsync", ex, TenantCache.GetSubDomainByTenantId((Guid)employeeEntity.TenantId), UserInfo.GetUserName(), employeeEntity);
                //}

                // Optionally rethrow the exception or handle it in a way that informs the caller
                throw;
            }
            return isSuccess;
        }

        public static async Task<List<AttendanceTableEntity>> GetEmployeeDetailsInAttendanceTable()
        {
            List<AttendanceTableEntity> attendanceTableEntity = new List<AttendanceTableEntity>();

            try
            {
                // Ensure the URL for the API endpoint is correct.
                string url = ServiceVirtualDirName + "api/EmployeeConfiguration/GetEmployeeDetailsInAttendanceTable";

                // Use the BaseProxy class to perform the GET operation.
                var response = await BaseProxy.Instance.GetAsyncMethod(url);

                if (response.IsSuccessStatusCode)
                {
                    string result = await response.Content.ReadAsStringAsync();
                    attendanceTableEntity = JsonConvert.DeserializeObject<List<AttendanceTableEntity>>(result);
                }
            }
            catch (Exception ex)
            {
                // Log the exception as needed.
                // Example: LogError("GetEmployeeAttendances", ex);

                // Optionally rethrow the exception or handle it in a way that informs the caller
                throw new Exception("An error occurred while fetching employee attendances.", ex);
            }

            return attendanceTableEntity;
        }

        public static async Task<bool> SendEmployeeAttendanceDetails(List<AttendanceDataSendEntity>  attendanceDataSendEntity)
        {
            bool isSuccess = false;
            try
            {
                // Ensure the URL for the API endpoint is correct.
                string url = ServiceVirtualDirName + "api/EmployeeConfiguration/SendEmployeeAttendanceDetails";

                // Use the BaseProxy class to perform the POST operation.
                var response = await BaseProxy.Instance.PostAsyncMethod(url, attendanceDataSendEntity);

                if (response.IsSuccessStatusCode)
                {
                    // Read the response content asynchronously.
                    string result = await response.Content.ReadAsStringAsync();

                    // Deserialize the result to a boolean.
                    isSuccess = JsonConvert.DeserializeObject<bool>(result);
                }
            }
            catch (Exception ex)
            {
                // Log the exception as needed.
                //using (LogException _error = new LogException(typeof(ProxyService), TenantCache.GetSqlDbConnectionFromCacheByTenantId(employeeEntity.TenantId)))
                //{
                //    _error.Exception("Error in CreateEmployeeAsync", ex, TenantCache.GetSubDomainByTenantId((Guid)employeeEntity.TenantId), UserInfo.GetUserName(), employeeEntity);
                //}

                // Optionally rethrow the exception or handle it in a way that informs the caller
                throw;
            }
            return isSuccess;
        }

        public static async Task<bool>AdvanceSalary(AdvanceSalaryEntity advanceSalaryEntity)
        {
            bool isSuccess = false;
            try   
            {
                // Ensure the URL for the API endpoint is correct.
                string url = ServiceVirtualDirName + "api/FinanceConfiguration/AdvanceSalary";

                // Use the BaseProxy class to perform the POST operation.
                var response = await BaseProxy.Instance.PostAsyncMethod(url, advanceSalaryEntity);

                if (response.IsSuccessStatusCode)
                {
                    // Read the response content asynchronously.
                    string result = await response.Content.ReadAsStringAsync();

                    // Deserialize the result to a boolean.
                    isSuccess = JsonConvert.DeserializeObject<bool>(result);
                }
            }
            catch (Exception ex)
            {
                // Log the exception as needed.
                //using (LogException _error = new LogException(typeof(ProxyService), TenantCache.GetSqlDbConnectionFromCacheByTenantId(employeeEntity.TenantId)))
                //{
                //    _error.Exception("Error in CreateEmployeeAsync", ex, TenantCache.GetSubDomainByTenantId((Guid)employeeEntity.TenantId), UserInfo.GetUserName(), employeeEntity);
                //}

                // Optionally rethrow the exception or handle it in a way that informs the caller
                throw;
            }
            return isSuccess;
        }

        public static async Task<IdEntity> AddEmployeePage()
        {
            IdEntity idEntity = null;

            try
            {
                // Ensure the URL for the API endpoint is correct.
                string url = ServiceVirtualDirName + "api/EmployeeConfiguration/GetNextEmpID";

                // Use the BaseProxy class to perform the GET operation.
                var response = await BaseProxy.Instance.GetAsyncMethod(url);

                if (response.IsSuccessStatusCode)
                {
                    string result = await response.Content.ReadAsStringAsync();
                    idEntity = JsonConvert.DeserializeObject<IdEntity>(result);
                }
            }
            catch (Exception ex)
            {
                // Log the exception as needed.
                // Example: LogError("AddEmployeePage", ex);

                // Optionally rethrow the exception or handle it in a way that informs the caller.
                throw new Exception("An error occurred while fetching the next employee ID.", ex);
            }

            return idEntity;
        }


        public static async Task<List<EmployeeSalaryEntity>> FinalSalary(SalaryRequestEntity salaryRequestEntity)
        {
            List<EmployeeSalaryEntity> employeeSalaryEntities = new List<EmployeeSalaryEntity>();

            try
            {
                // Ensure the URL for the API endpoint is correct.
                string url = ServiceVirtualDirName + "api/FinanceConfiguration/FinalSalary";

                // Use the BaseProxy class to perform the POST operation.
                var response = await BaseProxy.Instance.PostAsyncMethod(url, salaryRequestEntity);

                if (response.IsSuccessStatusCode)
                {
                    string result = await response.Content.ReadAsStringAsync();
                    employeeSalaryEntities = JsonConvert.DeserializeObject<List<EmployeeSalaryEntity>>(result);
                }
            }
            catch (Exception ex)
            {
                // Log the exception as needed.
                // Example: LogError("FinalSalary", ex);

                // Optionally rethrow the exception or handle it in a way that informs the caller
                throw new Exception("An error occurred while fetching employee salary details.", ex);
            }

            return employeeSalaryEntities;
        }



    }
}







        #endregion

