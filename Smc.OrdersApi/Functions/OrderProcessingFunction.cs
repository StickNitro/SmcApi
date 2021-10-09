using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace Smc.OrdersApi.Functions
{
    public class OrderProcessingFunction
    {
        private readonly JsonSerializerSettings serializerSettings;

        public OrderProcessingFunction(JsonSerializerSettings serializerSettings)
        {
            this.serializerSettings = serializerSettings;
        }

        [FunctionName("ProcessOrder")]
        public async Task<IActionResult> ProcessOrder(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "order/{orderId}/payment")] HttpRequest request)
        {
            return new OkResult();
        }
    }
}
