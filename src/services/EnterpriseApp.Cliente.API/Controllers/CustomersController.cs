using EnterpriseApp.API.Core.Controllers;
using EnterpriseApp.Cliente.API.Application.Commands;
using EnterpriseApp.Cliente.API.Business.Interfaces;
using EnterpriseApp.Core.Mediator;
using EnterpriseApp.Core.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace EnterpriseApp.Cliente.API.Controllers
{
    [Authorize]
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
        public async Task<IActionResult> AddAddress(AddAddressCommand command)
        {
            command.CustomerId = _userService.GetUserId();

            var validationResult = await _mediator.SendCommand(command);

            if (validationResult.Errors.Any())
                return CustomResponse(validationResult);

            return CustomResponse();
        }

        [HttpGet("addresses")]
        public async Task<IActionResult> GetAddress()
        {
            var address = await _customerRepository.GetAddressById(_userService.GetUserId());

            return address == null ? NotFound() : CustomResponse(address);
        }
    }
}
