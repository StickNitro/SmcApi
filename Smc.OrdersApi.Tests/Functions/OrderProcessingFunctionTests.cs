using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Shouldly;
using Smc.OrdersApi.Business.Domain;
using Smc.OrdersApi.Business.Services;
using Smc.OrdersApi.Functions;
using System;
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

        private readonly Mock<IPaymentProcessorService> mockPaymentProcessor = new Mock<IPaymentProcessorService>();

        [Fact]
        public async Task ProcessOrder_Should_ReturnOkResult()
        {
            var mockInputModel = new PaymentInputModel
            {
                Id = Guid.NewGuid(),
                OrderId = Guid.NewGuid(),
                Name = "Mock Product",
                Type = ProductType.Physical
            };

            var sut = new OrderProcessingFunction(this.mockPaymentProcessor.Object, this.jsonSettings);

            var result = await sut.ProcessOrder(this.CreateMockRequest(mockInputModel), mockInputModel.OrderId);

            result.ShouldBeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task ProcessOrder_Should_CallPaymentProcessor_WithInputModel()
        {
            var mockInputModel = new PaymentInputModel
            {
                Id = Guid.NewGuid(),
                OrderId = Guid.NewGuid(),
                Name = "Mock Product",
                Type = ProductType.Physical
            };

            this.mockPaymentProcessor.Setup(x => x.ProcessPayment(It.IsAny<PaymentInputModel>()));

            var sut = new OrderProcessingFunction(this.mockPaymentProcessor.Object, this.jsonSettings);

            var result = await sut.ProcessOrder(this.CreateMockRequest(mockInputModel), mockInputModel.OrderId);

            result.ShouldBeOfType<OkObjectResult>();

            this.mockPaymentProcessor.Verify(x => x.ProcessPayment(It.Is<PaymentInputModel>(model =>
                model.Name == "Mock Product" &&
                model.Type == ProductType.Physical)));
        }

        [Fact]
        public async Task ProcessOrder_WhenInputModelNull_Should_ReturnBadRequest()
        {
            var sut = new OrderProcessingFunction(this.mockPaymentProcessor.Object, this.jsonSettings);

            var result = await sut.ProcessOrder(this.CreateMockRequest(), Guid.NewGuid());

            result.ShouldBeOfType<BadRequestObjectResult>();
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
