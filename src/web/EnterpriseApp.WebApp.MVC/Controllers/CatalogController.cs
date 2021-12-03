using EnterpriseApp.WebApp.MVC.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace EnterpriseApp.WebApp.MVC.Controllers
{
    [ApiController]
    public class CatalogController : Controller
    {
        private readonly ICatalogServiceRefit _catalogService;

        public CatalogController(ICatalogServiceRefit catalogService)
        {
            _catalogService = catalogService;
        }

        [HttpGet]
        [Route("")]
        [Route("catalog")]
        public async Task<IActionResult> Index()
            => View(await _catalogService.GetAllProducts());

        [HttpGet]
        [Route("product-details/{id}")]
        public async Task<IActionResult> ProductDetails(Guid id)
            => View(await _catalogService.GetProduct(id));
    }
}
