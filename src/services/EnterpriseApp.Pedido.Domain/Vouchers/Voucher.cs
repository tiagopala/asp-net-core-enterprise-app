using EnterpriseApp.Core.DomainObjects;
using EnterpriseApp.Pedido.Domain.Vouchers.Specifications;
using System;

namespace EnterpriseApp.Pedido.Domain.Vouchers
{
    public class Voucher : Entity, IAggregateRoot
    {
        public string Code { get; private set; }
        public decimal? Percent { get; private set; }
        public decimal? DiscountValue { get; private set; }
        public int Quantity { get; private set; }
        public VoucherDiscountType DiscountType { get; private set; }
        public DateTime CreationDate { get; private set; }
        public DateTime? UsedDate { get; private set; }
        public DateTime MaximumValidationDate { get; private set; }
        public bool Active { get; private set; }
        public bool Used { get; private set; }

        public bool IsValidForUse()
            => new VoucherDateTimeSpecification()
                .And(new VoucherQuantitySpecification())
                .And(new VoucherActiveSpecification())
                .IsSatisfiedBy(this);

        public void SetAsUsed()
        {
            Used = true;
            Active = false;
            Quantity = 0;
        }
    }
}
