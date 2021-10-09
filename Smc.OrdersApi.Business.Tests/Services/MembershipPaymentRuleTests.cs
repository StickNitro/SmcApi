using Shouldly;
using Smc.OrdersApi.Business.Domain;
using Smc.OrdersApi.Business.Services;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Smc.OrdersApi.Business.Tests.Services
{
    public class MembershipPaymentRuleTests
    {
        [Fact]
        public async Task Process_When_Type_Membership_Should_Return_NewMembership()
        {
            var mockModel = new PaymentInputModel()
            {
                Type = ProductType.Membership
            };

            var sut = new MembershipPaymentRule();

            var result = await sut.Process(mockModel);

            result.ShouldNotBeNull();
            result.Membership.ShouldNotBeNull();
            result.Membership.Upgrade.ShouldBeFalse();
        }

        [Fact]
        public async Task Process_When_Type_NotMembership_Should_Return_Null()
        {
            var mockModel = new PaymentInputModel()
            {
                Type = ProductType.Book
            };

            var sut = new MembershipPaymentRule();

            var result = await sut.Process(mockModel);

            result.ShouldBeNull();
        }

        [Fact]
        public async Task Process_When_ModelIsNull_Should_Return_Null()
        {
            var sut = new MembershipPaymentRule();

            var result = await sut.Process(null);

            Assert.Null(result);
        }
    }
}
