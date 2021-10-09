using Shouldly;
using Smc.OrdersApi.Business.Domain;
using Smc.OrdersApi.Business.Services;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Smc.OrdersApi.Business.Tests.Services
{
    public class VideoProductPaymentRuleTests
    {
        [Fact]
        public async Task Process_When_Type_VideoProduct_Should_Return_ShippingPackingSlip_WithIncludeVideo()
        {
            var mockModel = new PaymentInputModel()
            {
                Type = ProductType.Video,
                Name = "Learning to Ski"
            };

            var sut = new VideoRegulationPaymentRule();

            var result = await sut.Process(mockModel);

            result.ShouldNotBeNull();
            result.PackingSlips.Count().ShouldBe(1);
            result.PackingSlips.First().Type.ShouldBe(PackingSlipType.Shipping);
            result.PackingSlips.First().IncludeFirstAidVideo.ShouldBeTrue();
        }

        [Fact]
        public async Task Process_When_Type_NotVideoProduct_Should_Return_Null()
        {
            var mockModel = new PaymentInputModel()
            {
                Type = ProductType.Book,
                Name = "Learning to Ski"
            };

            var sut = new VideoRegulationPaymentRule();

            var result = await sut.Process(mockModel);

            result.ShouldBeNull();
        }

        [Fact]
        public async Task Process_When_Type_NotLearningToSki_Should_Return_Null()
        {
            var mockModel = new PaymentInputModel()
            {
                Type = ProductType.Video,
                Name = "Mock Video Name"
            };

            var sut = new VideoRegulationPaymentRule();

            var result = await sut.Process(mockModel);

            result.ShouldBeNull();
        }

        [Fact]
        public async Task Process_When_ModelIsNull_Should_Return_Null()
        {
            var sut = new VideoRegulationPaymentRule();

            var result = await sut.Process(null);

            Assert.Null(result);
        }
    }
}
