using Smc.OrdersApi.Business.Domain;
using System.Threading.Tasks;

namespace Smc.OrdersApi.Business.Services
{
    public interface IPaymentProcessorService
    {
        Task<PaymentOutputModel> ProcessPayment(PaymentInputModel paymentModel);
    }
}
