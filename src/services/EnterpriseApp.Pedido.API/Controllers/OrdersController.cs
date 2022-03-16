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
        private readonly IOrderQueries _orderQueries;

        public OrdersController(
            IUserService userService,
            IMediatorHandler mediatorHandler,
            IOrderQueries orderQueries)
        {
            _orderQueries = orderQueries;
            _userService = userService;
            _mediatorHandler = mediatorHandler;
        }

        [HttpPost]
        public async Task<IActionResult> AddOrder(AddOrderCommand command)
        {
            command.CustomerId = _userService.GetUserId();
            return CustomResponse(await _mediatorHandler.SendCommand(command));
        }

        [HttpGet("last")]
        public async Task<IActionResult> GetLastOrder()
        {
            var order = await _orderQueries.GetLastOrder(_userService.GetUserId());

            return order is null ? NotFound() : CustomResponse(order);
        }

        [HttpGet("list")]
        public async Task<IActionResult> GetOrderList()
        {
            var orders = await _orderQueries.GetOrderListByCustomerId(_userService.GetUserId());

            return orders is null ? NotFound() : CustomResponse(orders);
        }
    }
}
