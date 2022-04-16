using EnterpriseApp.Core.DomainObjects;
using EnterpriseApp.Pagamento.API.Enums;
using System;
using System.Collections.Generic;

namespace EnterpriseApp.Pagamento.API.Models
{
    public class Payment : Entity, IAggregateRoot
    {
        public Guid OrderId { get; set; }
        public PaymentTypeEnum PaymentType { get; set; }
        public decimal Price { get; set; }
        public CreditCard CreditCard { get; set; }

        // EF Relation
        public ICollection<Transaction> Transactions { get; set; }

        public Payment()
        {
            Transactions = new List<Transaction>();
        }

        public void AddTransaction(Transaction transaction)
            => Transactions.Add(transaction);
    }
}
