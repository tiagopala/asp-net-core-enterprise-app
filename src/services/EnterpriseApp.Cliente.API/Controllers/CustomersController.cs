using EnterpriseApp.API.Core.Controllers;
using EnterpriseApp.Cliente.API.Application.Commands;
using EnterpriseApp.Cliente.API.Business.Interfaces;
using EnterpriseApp.Core.Mediator;
using EnterpriseApp.Core.Services.Interfaces;
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
        private readonly ICustomerRepository _customerRepository;
        private readonly IUserService _userService;

        public CustomersController(
            IMediatorHandler mediator,
            IUserService userService,
            ICustomerRepository customerRepository)
        {
            _mediator = mediator;
            _userService = userService;
            _customerRepository = customerRepository;
        }

        [HttpPost("addresses")]
        public async Task<IActionResult> AddAddress(RegisterCustomerCommand command)
        {
            var validationResult = await _mediator.SendCommand(command);

            if (validationResult.Errors.Any())
                return CustomResponse(validationResult);

            return CustomResponse();
        }

        [HttpGet("addresses")]
        public async Task<IActionResult> GetAddress()
        {
            var endereco = await _customerRepository.GetAddressById(_userService.GetUserId());

            return endereco == null ? NotFound() : CustomResponse(endereco);
        }
    }
}
