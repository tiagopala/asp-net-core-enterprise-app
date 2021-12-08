using EnterpriseApp.Core.DomainObjects;
using System;

namespace EnterpriseApp.Cliente.API.Models
{
    public class Customer : Entity, IAggregateRoot
    {
        public string Name { get; }
        public Email Email { get; private set; }
        public Cpf Cpf { get; private set; }
        public bool Deleted { get; }
        public Address Address { get; private set; }

        // Entity Framework Relation
        protected Customer() { }

        public Customer(Guid id, string name, string email, string cpf)
        {
            Id = id;
            Name = name;
            Email = new(email);
            Cpf = new(cpf);
            Deleted = false;
        }

        public void ChangeEmail(string email)
            => Email = new(email);

        public void SetAddress(Address address)
            => Address = address;
    }
}
