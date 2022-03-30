using EnterpriseApp.Core.DomainObjects;
using System;

namespace EnterpriseApp.Cliente.API.Business.Models
{
    public class Address : Entity
    {
        public string Street { get; }
        public string Number { get; }
        public string Complement { get; }
        public string Neighbourhood { get; }
        public string Cep { get; }
        public string City { get; }
        public string State { get; }
        public Guid CustomerId { get; set; }

        // Entity Framework Constraint Relation to Customer
        public Customer Customer { get; protected set; }

        public Address(string street, string number, string complement, string neighbourhood, string cep, string city, string state, Guid customerId)
        {
            Street = street;
            Number = number;
            Complement = complement;
            Neighbourhood = neighbourhood;
            Cep = cep;
            City = city;
            State = state;
            CustomerId = customerId;
        }
    }
}
