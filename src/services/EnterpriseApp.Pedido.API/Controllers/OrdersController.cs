using EnterpriseApp.API.Core.Controllers;
using EnterpriseApp.Core.Mediator;
using EnterpriseApp.Core.Services.Interfaces;
using EnterpriseApp.Pedido.Application.Commands;
using EnterpriseApp.Pedido.Application.Queries;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EnterpriseApp.Pedido.API.Controllers
{
    [Route("api/[controller]")]
    public class OrdersController : MainController
    {
        private readonly IMediatorHandler _mediatorHandler;
        private readonly IUserService _userService;
        private readonly IOrderQueries _voucherQueries;

        public OrdersController(
            IUserService userService,
            IMediatorHandler mediatorHandler,
            IOrderQueries orderQueries)
        {
            _voucherQueries = orderQueries;
            _userService = userService;
            _mediatorHandler = mediatorHandler;
        }

        [HttpPost]
        public async Task<IActionResult> AddOrder(AddOrderCommand command)
        {
            command.CustomerId = _userService.GetUserId();
            return CustomResponse(await _mediatorHandler.SendCommand(command));
        }


    }
}
