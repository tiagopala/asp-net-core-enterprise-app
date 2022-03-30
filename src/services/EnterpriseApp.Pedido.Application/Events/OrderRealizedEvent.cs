using EnterpriseApp.Core.Messages;
using System;

namespace EnterpriseApp.Pedido.Application.Events
{
    public class OrderRealizedEvent : Event
    {
        public Guid OrderId { get; set; }
        public Guid CustomerId { get; set; }

        public OrderRealizedEvent(Guid orderId, Guid customerId)
        {
            OrderId = orderId;
            CustomerId = customerId;
        }
    }
}
