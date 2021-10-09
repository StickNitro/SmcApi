using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Shouldly;
using Smc.OrdersApi.Business.Extensions;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Smc.OrdersApi.Business.Tests.Extensions
{
    public class HttpRequestExtensionsTests
    {
        private readonly JsonSerializerSettings jsonSettings = new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore,
            ContractResolver = new CamelCasePropertyNamesContractResolver()
        };

        [Fact]
        public async Task DeserializeBody_Should_Deserialize()
        {
            var mockObject = new MockObject { MockProperty = "Mock Value" };

            var request = this.CreateMockRequest(mockObject);
            var result = await request.DeserializeBody<MockObject>();

            result.ShouldNotBeNull();
            result.MockProperty.ShouldBe("Mock Value");
        }

        [Fact]
        public async Task DeserializeBody_WithSettings_Should_Deserialize()
        {
            var mockObject = new MockObject { MockProperty = "Mock Value" };

            var request = this.CreateMockRequest(mockObject);
            var result = await request.DeserializeBody<MockObject>(this.jsonSettings);

            result.ShouldNotBeNull();
            result.MockProperty.ShouldBe("Mock Value");
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

        public class MockObject
        {
            public string MockProperty { get; set; }
        }
    }
}
