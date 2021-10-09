using Shouldly;
using Smc.OrdersApi.Business.Domain;
using Smc.OrdersApi.Business.Services;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Smc.OrdersApi.Business.Tests.Services
{
    public class BookProductPaymentRuleTests
    {
        [Fact]
        public async Task Process_When_Type_BookProduct_Should_Return_ShippingAndRoyaltyPackingSlip()
        {
            var mockModel = new PaymentInputModel()
            {
                Type = ProductType.Book
            };

            var sut = new BookProductPaymentRule();

            var result = await sut.Process(mockModel);

            result.ShouldNotBeNull();
            result.PackingSlips.Count().ShouldBe(2);
            result.PackingSlips.First().Type.ShouldBe(PackingSlipType.Shipping);
            result.PackingSlips.Last().Type.ShouldBe(PackingSlipType.Royalty);
        }

        [Fact]
        public async Task Process_When_Type_BookProduct_Should_Return_GenerateCommissionPayment()
        {
            var mockModel = new PaymentInputModel()
            {
                Type = ProductType.Book
            };

            var sut = new BookProductPaymentRule();

            var result = await sut.Process(mockModel);

            result.ShouldNotBeNull();
            result.GenerateCommissionPayment.ShouldBeTrue();
        }

        [Fact]
        public async Task Process_When_Type_NotBookProduct_Should_Return_Null()
        {
            var mockModel = new PaymentInputModel()
            {
                Type = ProductType.Membership
            };

            var sut = new BookProductPaymentRule();

            var result = await sut.Process(mockModel);

            result.ShouldBeNull();
        }

        [Fact]
        public async Task Process_When_ModelIsNull_Should_Return_Null()
        {
            var sut = new BookProductPaymentRule();

            var result = await sut.Process(null);

            Assert.Null(result);
        }
    }
}
