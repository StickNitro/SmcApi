using Shouldly;
using Smc.OrdersApi.Business.Domain;
using Smc.OrdersApi.Business.Services;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Smc.OrdersApi.Business.Tests.Services
{
    public class PhysicalProductPaymentRuleTests
    {
        [Fact]
        public async Task Process_When_Type_PhysicalProduct_Should_Return_ShippingPackingSlip()
        {
            var mockModel = new PaymentInputModel()
            {
                Type = ProductType.Physical
            };

            var sut = new PhysicalProductPaymentRule();

            var result = await sut.Process(mockModel);

            result.ShouldNotBeNull();
            result.PackingSlips.Count().ShouldBe(1);
            result.PackingSlips.First().Type.ShouldBe(PackingSlipType.Shipping);
        }

        [Fact]
        public async Task Process_When_Type_NotPhysicalProduct_Should_Return_Null()
        {
            var mockModel = new PaymentInputModel()
            {
                Type = ProductType.Book
            };

            var sut = new PhysicalProductPaymentRule();

            var result = await sut.Process(mockModel);

            result.ShouldBeNull();
        }

        [Fact]
        public async Task Process_When_ModelIsNull_Should_Return_Null()
        {
            var sut = new PhysicalProductPaymentRule();

            var result = await sut.Process(null);

            Assert.Null(result);
        }
    }
}
