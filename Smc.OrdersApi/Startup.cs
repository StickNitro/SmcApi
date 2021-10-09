using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Smc.OrdersApi;

[assembly: FunctionsStartup(typeof(Startup))]
namespace Smc.OrdersApi
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services
                .AddLogging();
        }
    }
}
