using EnterpriseApp.API.Core.Authentication;
using EnterpriseApp.Catalogo.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EnterpriseApp.Catalogo.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CatalogController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public CatalogController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [AllowAnonymous]
        [HttpGet("products")]
        public async Task<IEnumerable<Product>> GetProducts()
            => await _productRepository.GetProducts();

        [HttpGet("products/{id}")]
        [ClaimsAuthorize("Catalog", "View")]
        public async Task<Product> GetProduct(Guid id)
            => throw new Exception("Error");
            //=> await _productRepository.GetProduct(id);
    }
}
