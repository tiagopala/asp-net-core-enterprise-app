using System;

namespace EnterpriseApp.Core.Messages.Integration
{
    public class OrderInitializedIntegrationEvent : IntegrationEvent
    {
        #region Order
        public Guid CustomerId { get; set; }
        public Guid OrderID { get; set; }
        public int PaymentType { get; set; }
        public decimal Price { get; set; }
        #endregion

        #region Credit Card
        public string CardName { get; set; }
        public string CardNumber { get; set; }
        public string MonthYearDueDate { get; set; } // Vencimento
        public string CVV { get; set; } // Codigo do cartão
        #endregion
    }
}
