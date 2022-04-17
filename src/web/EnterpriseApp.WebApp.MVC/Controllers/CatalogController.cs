using EnterpriseApp.WebApp.MVC.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace EnterpriseApp.WebApp.MVC.Controllers
{
    [ApiController]
    public class CatalogController : Controller
    {
        private readonly ICatalogService _catalogService;

        public CatalogController(ICatalogService catalogService)
        {
            _catalogService = catalogService;
        }

        [HttpGet]
        [Route("")]
        [Route("catalog")]
        public async Task<IActionResult> Index(
            [FromQuery] int pageSize = 2,
            [FromQuery] int pageIndex = 1,
            [FromQuery] string query = null)
        {
            var products = await _catalogService.GetAllProducts(pageSize, pageIndex, query);

            ViewBag.Pesquisa = query;

            return View(products);
        }

        [HttpGet]
        [Route("product-details/{id}")]
        public async Task<IActionResult> ProductDetails(Guid id)
            => View(await _catalogService.GetProduct(id));
    }
}
