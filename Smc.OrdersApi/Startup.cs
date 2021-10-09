using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Smc.OrdersApi;
using Smc.OrdersApi.Business.Services;

[assembly: FunctionsStartup(typeof(Startup))]
namespace Smc.OrdersApi
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services
                .AddLogging()
                .AddSingleton(serviceProvider => {
                    return new JsonSerializerSettings()
                    {
                        NullValueHandling = NullValueHandling.Ignore,
                        ContractResolver = new CamelCasePropertyNamesContractResolver()
                    };
                })
                .AddTransient<IPaymentProcessorService, PaymentProcessorService>()
                .AddScoped<IPaymentRule, PhysicalProductPaymentRule>();
        }
    }
}
