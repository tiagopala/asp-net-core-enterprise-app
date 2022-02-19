using EnterpriseApp.API.Core.Controllers;
using EnterpriseApp.Cliente.API.Application.Commands;
using EnterpriseApp.Core.Mediator;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace EnterpriseApp.Cliente.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomersController : MainController
    {
        private readonly IMediatorHandler _mediator;

        public CustomersController(IMediatorHandler mediator)
            => _mediator = mediator;

        [HttpPost]
        public async Task<IActionResult> AddCustomer(RegisterCustomerCommand command)
        {
            var validationResult = await _mediator.SendCommand(command);

            if (validationResult.Errors.Any())
                return CustomResponse(validationResult);

            return CustomResponse();
        }
    }
}
