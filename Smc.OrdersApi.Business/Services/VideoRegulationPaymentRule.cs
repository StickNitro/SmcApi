using Smc.OrdersApi.Business.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Smc.OrdersApi.Business.Services
{
    public class VideoRegulationPaymentRule : IPaymentRule
    {
        public Task<PaymentOutputModel> Process(PaymentInputModel model)
        {
            if (model is null)
                return Task.FromResult<PaymentOutputModel>(null);

            if (model.Type == ProductType.Video && model.Name == "Learning to Ski")
                return Task.FromResult(
                    new PaymentOutputModel
                    {
                        PackingSlips = new List<PackingSlip>
                        {
                            new PackingSlip { Id = Guid.NewGuid(), Type = PackingSlipType.Shipping, IncludeFirstAidVideo = true }
                        }
                    });

            return Task.FromResult<PaymentOutputModel>(null);
        }
    }
}
