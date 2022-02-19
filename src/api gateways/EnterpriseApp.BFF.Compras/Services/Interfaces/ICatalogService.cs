using EnterpriseApp.BFF.Compras.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EnterpriseApp.BFF.Compras.Services.Interfaces
{
    public interface ICatalogService
    {
        Task<ItemProductDTO> GetById(Guid id);

        Task<IEnumerable<ItemProductDTO>> GetItems(IEnumerable<Guid> ids);
    }
}
