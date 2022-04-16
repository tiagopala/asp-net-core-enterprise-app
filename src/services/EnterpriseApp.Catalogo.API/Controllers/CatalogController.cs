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
    public class CatalogController : Controller
    {
        private readonly IProductRepository _productRepository;

        public CatalogController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [AllowAnonymous]
        [HttpGet("products")]
        public async Task<PagedResult<Product>> GetProducts(
            [FromQuery] int pageSize = 8, 
            [FromQuery] int pageIndex = 1, 
            [FromQuery] string query = null)
        {
            return await _productRepository.GetProducts(pageSize, pageIndex, query);
        }

        [HttpGet("products/{id}")]
        [ClaimsAuthorize("Catalog", "View")]
        public async Task<Product> GetProduct(Guid id)
            => await _productRepository.GetProduct(id);

        [HttpGet("products/list/{ids}")]
        public async Task<IEnumerable<Product>> GetProductsList(string ids)
            => await _productRepository.GetProductsById(ids);
    }
}
