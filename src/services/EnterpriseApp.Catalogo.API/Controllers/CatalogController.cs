using EnterpriseApp.Catalogo.API.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EnterpriseApp.Catalogo.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CatalogController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public CatalogController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet("products")]
        public async Task<IEnumerable<Product>> GetProducts()
            => await _productRepository.GetProducts();

        [HttpGet("products/{id}")]
        public async Task<Product> GetProduct(Guid id)
            => await _productRepository.GetProduct(id);
    }
}
