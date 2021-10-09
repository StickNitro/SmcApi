using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Shouldly;
using Smc.OrdersApi.Functions;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Smc.OrdersApi.Tests.Functions
{
    public class OrderProcessingFunctionTests
    {
        private readonly JsonSerializerSettings jsonSettings = new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore,
            ContractResolver = new CamelCasePropertyNamesContractResolver()
        };

        [Fact]
        public async Task ProcessOrder_Should_ReturnOkResult()
        {
            var sut = new OrderProcessingFunction(null);

            var result = await sut.ProcessOrder(this.CreateMockRequest());

            result.ShouldBeOfType<OkResult>();
        }

        private HttpRequest CreateMockRequest(object body = null)
        {
            var json = JsonConvert.SerializeObject(body, this.jsonSettings);
            var ms = new MemoryStream(Encoding.UTF8.GetBytes(json));

            var context = new DefaultHttpContext();
            var request = context.Request;
            request.Body = ms;
            request.ContentType = "application/json";

            return request;
        }
    }
}
