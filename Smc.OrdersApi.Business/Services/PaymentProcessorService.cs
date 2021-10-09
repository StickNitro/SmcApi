using Smc.OrdersApi.Business.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Smc.OrdersApi.Business.Services
{
    public class PaymentProcessorService : IPaymentProcessorService
    {
        private readonly IEnumerable<IPaymentRule> paymentRules;

        public PaymentProcessorService(IEnumerable<IPaymentRule> paymentRules)
        {
            this.paymentRules = paymentRules;
        }

        public async Task<PaymentOutputModel> ProcessPayment(PaymentInputModel paymentModel)
        {
            if (paymentModel is null)
                throw new ArgumentNullException(nameof(paymentModel));

            foreach (var paymentRule in this.paymentRules)
            {
                var result = await paymentRule.Process(paymentModel);
                if (result != null)
                    return result;
            }

            return new PaymentOutputModel();
        }
    }
}
