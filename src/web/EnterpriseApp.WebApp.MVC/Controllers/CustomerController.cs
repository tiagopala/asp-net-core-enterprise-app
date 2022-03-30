using EnterpriseApp.WebApp.MVC.Models;
using EnterpriseApp.WebApp.MVC.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace EnterpriseApp.WebApp.MVC.Controllers
{
    [Authorize]
    public class CustomerController : MainController
    {
        private readonly ICustomersService _customersService;

        public CustomerController(ICustomersService customersService)
        {
            _customersService = customersService;
        }
        
        [HttpPost("addresses")]
        public async Task<IActionResult> NewAddress(AddressViewModel address)
        {
            var response = await _customersService.AddAddress(address);

            if (ResponsePossuiErros(response))
            {
                TempData["Erros"] = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)).ToList();
            }

            return RedirectToAction("EnderecoEntrega", "Pedido");
        }
    }
}
