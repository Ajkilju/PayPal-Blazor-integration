using Newtonsoft.Json;

namespace PayPalIntegrationDemo.Server.Model
{
    public class PaypalAccessToken
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        [JsonProperty("expires_in")]
        public int ExpiresIn { get; set; }
    }
}
