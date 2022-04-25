using System.Text.Json.Serialization;

namespace AuthGuard.API.Models
{
    public struct TokenResult
    {
        [JsonPropertyName("username")]
        public string UserName { get; set; }

        [JsonPropertyName("accessToken")]
        public string AccessToken { get; set; }
        
        [JsonPropertyName("expiresIn")]
        public double ExpiresIn { get; set; }
    }
}