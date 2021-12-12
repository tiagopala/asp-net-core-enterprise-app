using EnterpriseApp.Cliente.API.Application.Commands;
using EnterpriseApp.Core.Mediator;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace EnterpriseApp.Cliente.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly IMediatorHandler _mediator;

        public CustomersController(IMediatorHandler mediator)
            => _mediator = mediator;

        [HttpPost]
        public async Task<IActionResult> AddCustomer(RegisterCustomerCommand command)
        {
            var validationResult = await _mediator.SendCommand(command);

            if (validationResult.Errors.Any())
                return BadRequest(new { Errors = validationResult.Errors.Select(x => x.ErrorMessage) });

            return Ok();
        }
    }
}
