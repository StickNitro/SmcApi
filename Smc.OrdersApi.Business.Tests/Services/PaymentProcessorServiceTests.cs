using Shouldly;
using Smc.OrdersApi.Business.Domain;
using Smc.OrdersApi.Business.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Smc.OrdersApi.Business.Tests.Services
{
    public class PaymentProcessorServiceTests
    {
        [Fact]
        public async Task ProcessPayment_WhenNoPaymentRules_Should_ReturnDefaultPaymentResponse()
        {
            var mockModel = new PaymentInputModel();

            var sut = new PaymentProcessorService(new List<IPaymentRule>());

            var result = await sut.ProcessPayment(mockModel);

            result.ShouldBeOfType<PaymentOutputModel>();
            result.Membership.ShouldBeNull();
            result.PackingSlips.ShouldBeNull();
            result.GenerateCommissionPayment.ShouldBeFalse();
        }

        [Fact]
        public async Task ProcessPayment_WhenModelNull_Should_Throw()
        {
            var sut = new PaymentProcessorService(null);

            await Assert.ThrowsAsync<ArgumentNullException>(() => sut.ProcessPayment(null));
        }

        [Fact]
        public async Task ProcessPayment_WhenModelIsBook_Returns_CorrectModel()
        {
            var rules = new List<IPaymentRule>()
            {
                new PhysicalProductPaymentRule(),
                new BookProductPaymentRule(),
                new MembershipPaymentRule(),
                new VideoRegulationPaymentRule()
            };

            var mockInput = new PaymentInputModel
            {
                Type = ProductType.Book
            };

            var sut = new PaymentProcessorService(rules);

            var result = await sut.ProcessPayment(mockInput);

            result.ShouldNotBeNull();
            result.GenerateCommissionPayment.ShouldBeTrue();
            result.PackingSlips.ShouldNotBeNull();
            result.PackingSlips.Count().ShouldBe(2);
            result.PackingSlips.First().Type.ShouldBe(PackingSlipType.Shipping);
            result.PackingSlips.Last().Type.ShouldBe(PackingSlipType.Royalty);
            result.Membership.ShouldBeNull();
        }
    }
}
