using EnterpriseApp.Carrinho.API.Data;
using EnterpriseApp.Carrinho.API.Models;
using EnterpriseApp.Core.Services.Interfaces;
using Grpc.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace EnterpriseApp.Carrinho.API.Services.gRPC
{
    public class ShoppingCartGRpcService : ShoppingCartServices.ShoppingCartServicesBase
    {
        private readonly ILogger<ShoppingCartGRpcService> _logger;
        private readonly IUserService _userService;
        private readonly ShoppingCartContext _dbContext;

        public ShoppingCartGRpcService(
            ILogger<ShoppingCartGRpcService> logger,
            IUserService userService,
            ShoppingCartContext dbContext)
        {
            _logger = logger;
            _userService = userService;
            _dbContext = dbContext;
        }

        public override async Task<ShoppingCartCustomerResponse> GetShoppingCart(GetShoppingCartRequest request, ServerCallContext context)
        {
            _logger.LogInformation($"Serviço chamado - {nameof(GetShoppingCart)}");

            return await base.GetShoppingCart(request, context);
        }

        private async Task<ShoppingCartCustomer> GetShoppingCartFromDatabase()
            => await _dbContext.CartCustomer.Include(x => x.Items).FirstOrDefaultAsync(x => x.CustomerId == _userService.GetUserId());
    }
}
