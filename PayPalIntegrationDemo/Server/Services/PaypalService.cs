using Newtonsoft.Json;
using PayPalIntegrationDemo.Server.Model;
using PayPalIntegrationDemo.Server.Settings;
using System.Net.Http.Headers;
using System.Text;

namespace PayPalIntegrationDemo.Server.Services
{
    public class PaypalService
    {
        private readonly PaypalSettings settings;

        public PaypalService(PaypalSettings settings)
        {
            this.settings = settings;
        }

        public async Task<string> CreateOrder(CreateOrderRequest order)
        {
            var json = $"{{ \"intent\": \"CAPTURE\", \"purchase_units\": [{{ \"amount\": {{ \"currency_code\": \"USD\", \"value\": \"{order.Amount}\" }}}}]}}";
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var client = await CreateClient();

            var res = await client.PostAsync(settings.OrdersUrl, content);
            res.EnsureSuccessStatusCode();

            var resContent = await res.Content.ReadAsStringAsync();

            return resContent;
        }

        public async Task<string> CaptureOrder(CaptureOrderRequest capture)
        {
            var url = $"{settings.OrdersUrl}{capture.OrderId}/capture";
            var content = new StringContent(string.Empty, Encoding.UTF8, "application/json");

            var client = await CreateClient();

            var res = await client.PostAsync(url, content);
            res.EnsureSuccessStatusCode();

            var resContent = await res.Content.ReadAsStringAsync();

            return resContent;
        }

        private async Task<HttpClient> CreateClient()
        {
            var token = await GenerateAccessToken();

            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            return client;
        }

        private async Task<string> GenerateAccessToken()
        {
            var content = new StringContent("grant_type=client_credentials");

            var auth = $"{settings.ClientId}:{settings.ClientSecret}";
            var authBase64 = Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes(auth));

            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authBase64);

            var res = await client.PostAsync(settings.AuthUrl, content);
            res.EnsureSuccessStatusCode();

            var resContent = await res.Content.ReadAsStringAsync();
            var token = JsonConvert.DeserializeObject<PaypalAccessToken>(resContent);

            return token.AccessToken;
        }
    }

}
