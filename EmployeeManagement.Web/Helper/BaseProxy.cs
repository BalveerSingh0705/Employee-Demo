using Newtonsoft.Json;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;
using System.Net;
using EmployeeManagement.Core.Common;
using EmployeeManagement.Exceptions.Manager;
using System.Net.Http;
using System;

namespace EmployeeManagement.Web.Helper
{



    public class BaseProxy : BaseClient
    {
        private static readonly object _locker = new object();
        private static volatile BaseProxy _instance;
        private static string baseAddress;
        private static string connectionString;
        private readonly IHttpClientFactory _clientFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;

  
        public BaseProxy(IHttpClientFactory clientFactory, IHttpContextAccessor httpContextAccessor)
        {
            _clientFactory = clientFactory;
            _httpContextAccessor = httpContextAccessor;
        }
        private BaseProxy()
        {
            baseAddress = System.Configuration.ConfigurationManager.AppSettings["ServiceBaseAddress"];
            // connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["connectionString"].ToString();
            connectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=RayCompany;User ID=sa;Password=AnuragSingh123;TrustServerCertificate=true;";
        }

        public static BaseProxy Instance
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
                        _instance = new BaseProxy();
                    }
                }

                return _instance;
            }
        }

       
        public async Task<HttpResponseMessage> GetAsyncMethod(string serviceURL)
        {

           var ServiceClient = new HttpClient();
            var response = await ServiceClient.GetAsync(serviceURL);
            try
            {
                if (response.ReasonPhrase.ToLower().Equals("unauthorized") && response.IsSuccessStatusCode == false)
                {
                    // Generate Token
                    UserCredentialData objUserCredentialModel = new UserCredentialData
                    {
                        //Username = UserInfo.GetUserName(),
                        //Password = UserInfo.GetUserPassword()
                        Username = "Anurag123@Gmail.com", //UserInfo.GetUserName(),
                        Password = "Anurag123@Gmail.com"//UserInfo.GetUserPassword(
                    };

                    //--Getting token and store it in session.
                    ServiceTokenData tokenDetails = await GetAccessTokenAsync(objUserCredentialModel);

                    //HttpContext.Current.Session["ServiceToken"] = tokenDetails.AccessToken;

                    //Re-Call WebAPI
                    response = await ServiceClient.GetAsync(serviceURL);
                }
            }
            catch (Exception ex)
            {
                using (LogException _error = new LogException(typeof(BaseProxy), connectionString))
                {
                    _error.Exception("Error in GetAsyncMethod", ex, "", "Anurag123@Gmail.com", serviceURL);
                }
            }
            return response;
        }

        private HttpContent CreateHttpContent(object data)
        {

            return new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
        }
        public static void SetValue<T>(T obj, string propertyName, object value)
        {
            try
            {
                var propertyType = obj.GetType().GetProperty(propertyName);

                if (propertyType != null && value != null)
                {
                    propertyType.SetValue(obj, Convert.ChangeType(value, propertyType.PropertyType), null);
                }
            }
            catch (Exception) { }
        }
        #region Service Token

        /// <summary>
        /// The use of method is to get access token required for services. It requires user credentials data.
        /// </summary>
        /// <param name="userCredentials">The object of UserCredentialData</param>
        /// <returns>It returns the object of ServiceTokenData.</returns>
        public async Task<ServiceTokenData> GetAccessTokenAsync(UserCredentialData userCredentials)
        {
            try
            {
                using (HttpClient _client = new HttpClient())
                {
                    _client.BaseAddress = new Uri(baseAddress);
                    _client.DefaultRequestHeaders.Accept.Clear();
                    _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    ServicePointManager.Expect100Continue = true;
                    ServicePointManager.DefaultConnectionLimit = 9999;
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                    string databaseConnection = string.Empty;

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
                        var result = await responseMessage.Content.ReadAsAsync<ServiceTokenData>(new[] { new JsonMediaTypeFormatter() });

                        return result;
                    }
                }
            }
            catch (Exception ex)
            {
                using (LogException _error = new LogException(typeof(BaseProxy), connectionString))
                {
                    _error.Exception("GetAccessToken", ex, connectionString, "Anurag123@Gmail.com", userCredentials);
                }
            }
            return new ServiceTokenData();
        }

        public async Task<HttpResponseMessage> PostAsyncMethod(string serviceURL, object objectData)
        {
            HttpResponseMessage response = await MakePostRequest(serviceURL, objectData);

            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                // Generate Token
                UserCredentialData objUserCredentialModel = new UserCredentialData
                {
                    Username = "Anurag123@Gmail.com", //UserInfo.GetUserName(),
                    Password = "Anurag123@Gmail.com" //UserInfo.GetUserPassword()
                };

                try
                {
                    // Get and store token
                    ServiceTokenData tokenDetails = await GetAccessTokenAsync(objUserCredentialModel);
                    // Update the token in the session or wherever you store it
                    _httpContextAccessor.HttpContext.Session.SetString("ServiceToken", tokenDetails.AccessToken);

                    // Update headers with new token
                    AddTokenToHeaders(tokenDetails.AccessToken);

                    // Retry the request
                    response = await MakePostRequest(serviceURL, objectData);
                }
                catch (Exception tokenEx)
                {
                    // Log the exception related to token generation
                    LogException(tokenEx, "Error generating token");
                    throw; // Re-throw the exception after logging
                }
            }

            return response;
        }

        private async Task<HttpResponseMessage> MakePostRequest(string serviceURL, object objectData)
        {
            try
            {
                var json = JsonConvert.SerializeObject(objectData);
                //var content = new StringContent(json, Encoding.UTF8, "application/json");
                //var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

                //var httpClient = new HttpClient();



                var client = new HttpClient();
                var content = new StringContent(json, Encoding.UTF8, "application/json");

              HttpResponseMessage response = await client.PostAsync(serviceURL, content);

                //return await ServiceClient.PostAsync(serviceURL, httpContent);
                return response;
            }
            catch (Exception ex)
            {
                LogException(ex, "Error in MakePostRequest", new { ServiceURL = serviceURL, ObjectData = objectData });
                throw; // Re-throw the exception after logging
            }
        }

        private void LogException(Exception ex, string message, object additionalData = null)
        {
            using (LogException _error = new LogException(typeof(BaseProxy), connectionString))
            {
                _error.Exception(message, ex, "", "Anurag123@Gmail.com" /* UserInfo.GetUserName() */, additionalData);
            }
        }

        private void AddTokenToHeaders(string token)
        {
            ServiceClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }



        #endregion
    }
}