using Smc.OrdersApi.Business.Domain;
using System;
using System.Threading.Tasks;

namespace Smc.OrdersApi.Business.Services
{
    public class PhysicalProductPaymentRule : IPaymentRule
    {
        public Task<PaymentOutputModel> Process(PaymentInputModel model)
        {
            throw new NotImplementedException();
        }
    }
}
