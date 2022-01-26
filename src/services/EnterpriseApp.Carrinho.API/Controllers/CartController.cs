using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EnterpriseApp.Carrinho.API.Controllers
{
    [Route("[controller]")]
    public class CartController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok();
        }
    }
}
