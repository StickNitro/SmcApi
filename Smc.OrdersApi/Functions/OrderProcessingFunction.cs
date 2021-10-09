using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Newtonsoft.Json;
using Smc.OrdersApi.Business.Domain;
using Smc.OrdersApi.Business.Extensions;
using Smc.OrdersApi.Business.Services;
using System.Threading.Tasks;

namespace Smc.OrdersApi.Functions
{
    public class OrderProcessingFunction
    {
        private readonly IPaymentProcessorService paymentProcessorService;
        private readonly JsonSerializerSettings serializerSettings;

        public OrderProcessingFunction(IPaymentProcessorService paymentProcessorService, JsonSerializerSettings serializerSettings)
        {
            this.paymentProcessorService = paymentProcessorService;
            this.serializerSettings = serializerSettings;
        }

        [FunctionName("ProcessOrder")]
        public async Task<IActionResult> ProcessOrder(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "order/{orderId}/payment")] HttpRequest request)
        {
            var inputModel = await request.DeserializeBody<PaymentInputModel>(this.serializerSettings);
            if (inputModel is null)
            {
                return new BadRequestObjectResult(new { Error = "Expected request body was invalid" });
            }

            var outputModel = await this.paymentProcessorService.ProcessPayment(inputModel);

            return new OkObjectResult(outputModel);
        }
    }
}
