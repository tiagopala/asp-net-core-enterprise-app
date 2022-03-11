using EnterpriseApp.Core.DomainObjects;
using EnterpriseApp.Pedido.Domain.Vouchers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseApp.Pedido.Domain.Pedidos
{
    public class Order : Entity, IAggregateRoot
    {
        public int Code { get; private set; }
        public Guid CustomerId { get; private set; }
        public Guid? VoucherId { get; private set; }
        public bool HasUsedVoucher { get; private set; }
        public decimal Discount { get; private set; }
        public decimal TotalPrice { get; private set; }
        public DateTime CreationDate { get; private set; }
        public OrderStatus OrderStatus { get; private set; }

        private readonly List<OrderItem> _orderItems;
        public IReadOnlyCollection<OrderItem> OrderItems => _orderItems;

        public Address Address { get; private set; }

        // EF Rel.
        public Voucher Voucher { get; private set; }

        // EF ctor
        protected Order() { }

        public Order(Guid customerId, decimal totalPrice, List<OrderItem> orderItems, bool hasUsedVoucher = false, decimal discount = 0, Guid? voucherId = null)
        {
            CustomerId = customerId;
            TotalPrice = totalPrice;
            _orderItems = orderItems;

            Discount = discount;
            HasUsedVoucher = hasUsedVoucher;
            VoucherId = voucherId;
        }

        public void AuthorizeOrder()
        {
            OrderStatus = OrderStatus.Autorizado;
        }

        public void ApplyVoucher(Voucher voucher)
        {
            HasUsedVoucher = true;
            VoucherId = voucher.Id;
            Voucher = voucher;
        }

        public void ApplyAddress(Address address)
        {
            Address = address;
        }

        public void CalculateOrderPrice()
        {
            TotalPrice = OrderItems.Sum(p => p.CalculatePrice());
            CalculateTotalPriceDiscount();
        }

        public void CalculateTotalPriceDiscount()
        {
            if (!HasUsedVoucher) return;

            decimal desconto = 0;
            var valor = TotalPrice;

            if (Voucher.DiscountType == VoucherDiscountType.Percentage)
            {
                if (Voucher.Percent.HasValue)
                {
                    desconto = (valor * Voucher.Percent.Value) / 100;
                    valor -= desconto;
                }
            }
            else
            {
                if (Voucher.DiscountValue.HasValue)
                {
                    desconto = Voucher.DiscountValue.Value;
                    valor -= desconto;
                }
            }

            TotalPrice = valor < 0 ? 0 : valor;
            Discount = desconto;
        }
    }
}
