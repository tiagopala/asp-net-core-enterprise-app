using EnterpriseApp.Cliente.API.Business.Interfaces;
using EnterpriseApp.Cliente.API.Business.Models;
using EnterpriseApp.Core.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EnterpriseApp.Cliente.API.Data.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        public readonly CustomerContext _customerContext;

        public CustomerRepository(CustomerContext customerContext)
            => _customerContext = customerContext;

        public IUnitOfWork UnitOfWork => _customerContext;

        public void Add(Customer customer)
            => _customerContext.Customers.Add(customer);

        public async Task<IEnumerable<Customer>> GetAll()
            => await _customerContext.Customers.ToListAsync();

        public async Task<Customer> GetByCpf(string cpf)
            => await _customerContext.Customers.FirstOrDefaultAsync(x => x.Cpf.Number == cpf);

        public void Dispose() 
            => _customerContext?.DisposeAsync();
    }
}
