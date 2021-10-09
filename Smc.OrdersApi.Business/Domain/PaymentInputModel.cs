using System;

namespace Smc.OrdersApi.Business.Domain
{
    public class PaymentInputModel
    {
        public Guid Id { get; set; }
        public Guid OrderId { get; set; }
        public ProductType Type { get; set; }
        public string Name { get; set; }
    }
}
