using PayPalIntegrationDemo.Server.Services;
using PayPalIntegrationDemo.Server.Settings;

namespace PayPalIntegrationDemo.Server.Registration
{
    public static class PaypalRegistration
    {
        public static void AddPaypal(this IServiceCollection services, ConfigurationManager configurationManager)
        {
            var paypalSettings = configurationManager.GetSection(nameof(PaypalSettings)).Get<PaypalSettings>();
            services.AddSingleton(paypalSettings);

            services.AddScoped<PaypalService>();
        }
    }
}
