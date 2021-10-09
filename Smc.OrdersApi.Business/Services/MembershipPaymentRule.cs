using Smc.OrdersApi.Business.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Smc.OrdersApi.Business.Services
{
    public class MembershipPaymentRule : IPaymentRule
    {
        public Task<PaymentOutputModel> Process(PaymentInputModel model)
        {
            if (model is null)
                return Task.FromResult<PaymentOutputModel>(null);

            if (model.Type != ProductType.Membership)
                return Task.FromResult<PaymentOutputModel>(null);

            return Task.FromResult(
                new PaymentOutputModel
                {
                    Membership = new Membership()
                });
        }
    }
}
