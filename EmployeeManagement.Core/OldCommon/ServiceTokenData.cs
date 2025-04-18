using Newtonsoft.Json;

namespace EmployeeManagement.Core.Common
{
    public class ServiceTokenData
    {
        [JsonProperty(PropertyName = "access_token")]
        public string AccessToken { get; set; }

        [JsonProperty(PropertyName = "token_type")]
        public string TokenType { get; set; }

        [JsonProperty(PropertyName = "expires_in")]
        public int ExpiresIn { get; set; }

        [JsonProperty(PropertyName = ".expires")]
        public DateTime Expires { get; set; }

        [JsonProperty(PropertyName = ".issued")]
        public DateTime Issued { get; set; }

        [JsonProperty(PropertyName = "refresh_token")]
        public string RefreshToken { get; set; }
    }

    public class UserCredentialData
    {
        /// <summary>
        /// The Password is the password of user used to login to AIMS system.
        /// </summary>
        public string Password { get; set; }
        public string Username { get; set; }
    }
}
