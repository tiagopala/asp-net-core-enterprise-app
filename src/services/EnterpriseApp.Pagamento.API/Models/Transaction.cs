using EnterpriseApp.Core.DomainObjects;
using EnterpriseApp.Pagamento.API.Enums;
using System;

namespace EnterpriseApp.Pagamento.API.Models
{
    public class Transaction : Entity
    {
        public string AuthorizationCode { get; set; }
        public string CreditCardBrand { get; set; }
        public DateTime? Date { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal Tax { get; set; }
        public TransactionStatusEnum Status { get; set; }
        public string TID { get; set; } // Transaction Id
        public string NSU { get; set; } // Meio ("Payme")
        public Guid PaymentId { get; set; }

        // EF Relation
        public Payment Payment { get; set; }
    }
}
