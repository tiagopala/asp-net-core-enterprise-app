using EnterpriseApp.Core.DomainObjects;

namespace EnterpriseApp.Cliente.API.Models
{
    public class Customer : Entity, IAggregateRoot
    {
        public string Name { get; }
        public string Email { get; }
        public string Cpf { get; }
        public bool Deleted { get; }
        public Address Address { get; }

        // Entity Framework Relation
        protected Customer() { }

        public Customer(string name, string email, string cpf)
        {
            Name = name;
            Email = email;
            Cpf = cpf;
            Deleted = false;
        }
    }
}
