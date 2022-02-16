using EnterpriseApp.API.Core.Controllers;
using EnterpriseApp.BFF.Compras.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EnterpriseApp.BFF.Compras.Controllers
{
    [Authorize]
    [Route("purchase/shoppingcart")]
    public class ShoppingCartController : MainController
    {
        public ShoppingCartController()
        {
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return CustomResponse();
        }

        [HttpPost]
        [Route("items")]
        public async Task<IActionResult> AdicionarItemCarrinho()
        {
            return CustomResponse();
        }

        [HttpGet]
        [Route("items/quantity")]
        public async Task<IActionResult> ObterQuantidadeCarrinho()
        {
            return CustomResponse();
        }

        [HttpPut]
        [Route("items/{produtoId}")]
        public async Task<IActionResult> AtualizarItemCarrinho()
        {
            return CustomResponse();
        }

        [HttpDelete]
        [Route("items/{produtoId}")]
        public async Task<IActionResult> RemoverItemCarrinho()
        {
            return CustomResponse();
        }
    }
}
