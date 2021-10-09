using Smc.OrdersApi.Business.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Smc.OrdersApi.Business.Services
{
    public class BookProductPaymentRule : IPaymentRule
    {
        public Task<PaymentOutputModel> Process(PaymentInputModel model)
        {
            if (model is null)
                return Task.FromResult<PaymentOutputModel>(null);

            if (model.Type != ProductType.Book)
                return Task.FromResult<PaymentOutputModel>(null);

            return Task.FromResult(new PaymentOutputModel()
            {
                PackingSlips = new List<PackingSlip>
                {
                    new PackingSlip { Id = Guid.NewGuid(), Type = PackingSlipType.Shipping },
                    new PackingSlip { Id = Guid.NewGuid(), Type = PackingSlipType.Royalty }
                },
                GenerateCommissionPayment = true
            });
        }
    }
}
