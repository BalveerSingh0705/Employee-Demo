using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace EmployeeManagement.API.Services
{
    public class MyHttpClient
    {
        private readonly HttpClient _httpClient;

        public MyHttpClient(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        /// <summary>
        /// Retrieves a protected resource from an API using the provided JWT token.
        /// </summary>
        /// <param name="token">The JWT token to authenticate the request.</param>
        /// <returns>Returns the string content of the protected resource.</returns>
        public async Task<string> GetProtectedResource(string token)
        {
            if (string.IsNullOrEmpty(token))
                throw new ArgumentNullException(nameof(token), "JWT token cannot be null or empty");

            // Set the Authorization header with the Bearer token
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            try
            {
                // Make a GET request to the protected resource API
                var response = await _httpClient.GetAsync("https://api.protected-resource.com/data");

                // Ensure that the request was successful (throws an exception if not)
                response.EnsureSuccessStatusCode();

                // Read and return the content of the response
                return await response.Content.ReadAsStringAsync();
            }
            catch (HttpRequestException ex)
            {
                // Handle potential HTTP errors (like 401 Unauthorized, 404 Not Found, etc.)
                throw new Exception("An error occurred while accessing the protected resource: " + ex.Message, ex);
            }
        }
    }
}
