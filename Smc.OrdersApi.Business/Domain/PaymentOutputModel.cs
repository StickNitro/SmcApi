using System;
using System.Collections.Generic;

namespace Smc.OrdersApi.Business.Domain
{
    public class PaymentOutputModel
    {
        public IEnumerable<PackingSlip> PackingSlips { get; set; } = new List<PackingSlip>();
        public Membership Membership { get; set; }
        public bool GenerateCommissionPayment { get; set; }
    }

    public class PackingSlip
    {
        public Guid Id { get; set; }
        public PackingSlipType Type { get; set; }
        public bool IncludeFirstAidVideo { get; set; }
    }

    public class Membership
    {
        public bool Upgrade { get; set; }
        public bool SendNotification { get; set; }
    }

    public enum PackingSlipType
    {
        Shipping,
        Royalty
    }
}
