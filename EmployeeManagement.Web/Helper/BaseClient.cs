using System;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;

namespace EmployeeManagement.Web.Helper
{
    /*
     The baseclient class is to maintain singletone instance of HttpClient
    */
    /// <summary>
    /// The baseclient class is to maintain singletone instance of HttpClient
    /// </summary>
    public abstract class BaseClient : IDisposable 
    {
        private static readonly object _locker = new object();
        private static volatile HttpClient? _client;

        /// <summary>
        /// Creates/Returns the instance of HttpClient
        /// </summary>
        public static HttpClient ServiceClient
        {
            get
            {
                // If client object is already available don't create new use existing.
                if (_client != null)
                {
                    ServicePointManager.Expect100Continue = true;
                    ServicePointManager.DefaultConnectionLimit = 9999;
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                    // Sets Authorization request header required for HttpClient
                    _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer","ABC"/* UserInfo.GetServiceToken()*/);
                    return _client;
                }

                // Lock is to handle mutithread requests
                lock (_locker)
                {
                    // If client object is null then create new
                    if (_client == null)
                    {
                        //HttpClientHandler handler = new HttpClientHandler()
                        //{
                        //    AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
                        //};

                        // Creating new instance of client.
                        _client = new HttpClient()
                        {
                            // Assigning the WebAPI Service URL
                            BaseAddress = new Uri(System.Configuration.ConfigurationManager.AppSettings["ServiceBaseAddress"]),
                            Timeout = TimeSpan.FromMinutes(5)
                        };

                        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12; // | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                        // Setting request headers
                        _client.DefaultRequestHeaders.Accept.Clear();
                        _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        // Sets Authorization request header required for HttpClient
                        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "ABC"/*UserInfo.GetServiceToken()*/);
                        ServicePointManager.Expect100Continue = true;
                        ServicePointManager.DefaultConnectionLimit = 9999;
                        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                    }
                }

                return _client;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_client != null)
                {
                    _client.Dispose();
                }
                _client = null;
            }
        }
    }
}