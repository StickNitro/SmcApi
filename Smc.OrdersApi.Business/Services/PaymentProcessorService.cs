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

        public Task<PaymentOutputModel> ProcessPayment(PaymentInputModel paymentModel)
        {
            throw new NotImplementedException();
        }
    }
}
