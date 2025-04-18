using E_Commerce.Shared.DataObject;
using Newtonsoft.Json;
using System;

namespace E_Commerce.Shared.DataObject
{
    public class ServiceTokenData : BaseDataObject
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

        // Optional: Override ToString() for better debugging and logging
        public override string ToString()
        {
            return $"AccessToken: {AccessToken}, TokenType: {TokenType}, ExpiresIn: {ExpiresIn}, Expires: {Expires}, Issued: {Issued}, RefreshToken: {RefreshToken}";
        }

        // Optional: Method to check if the token is expired
        public bool IsExpired()
        {
            return DateTime.UtcNow >= Expires;
        }
    }
}
