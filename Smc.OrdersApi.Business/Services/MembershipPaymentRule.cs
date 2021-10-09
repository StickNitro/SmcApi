using Smc.OrdersApi.Business.Domain;
using System.Threading.Tasks;

namespace Smc.OrdersApi.Business.Services
{
    public class MembershipPaymentRule : IPaymentRule
    {
        public Task<PaymentOutputModel> Process(PaymentInputModel model)
        {
            if (model is null)
                return Task.FromResult<PaymentOutputModel>(null);

            if (model.Type != ProductType.Membership && model.Type != ProductType.MembershipUpgrade)
                return Task.FromResult<PaymentOutputModel>(null);

            return Task.FromResult(
                new PaymentOutputModel
                {
                    Membership = new Membership
                    {
                        Upgrade = model.Type == ProductType.MembershipUpgrade,
                        SendNotification = true
                    }
                });
        }
    }
}
