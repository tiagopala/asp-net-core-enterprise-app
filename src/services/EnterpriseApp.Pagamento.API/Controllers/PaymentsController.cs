using EnterpriseApp.API.Core.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace EnterpriseApp.Pagamento.API.Controllers
{
    [Route("api/[controller]")]
    public class PaymentsController : MainController
    {
        [HttpGet]
        public IActionResult Get()
            => Ok();
    }
}
