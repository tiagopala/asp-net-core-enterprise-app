using EnterpriseApp.Cliente.API.Business.Models;
using EnterpriseApp.Core.DomainObjects;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EnterpriseApp.Cliente.API.Business.Interfaces
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        void Add(Customer customer);
        Task<IEnumerable<Customer>> GetAll();
        Task<Customer> GetByCpf(string cpf);
        void AddAddress(Address address);
        Task<Address> GetAddressById(Guid id);
    }
}
