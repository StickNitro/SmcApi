using Smc.OrdersApi.Business.Domain;
using System.Threading.Tasks;

namespace Smc.OrdersApi.Business.Services
{
    public interface IPaymentRule
    {
        Task<PaymentOutputModel> Process(PaymentInputModel model);
    }
}
