using EnterpriseApp.API.Core.Controllers;
using EnterpriseApp.Pedido.Application.DTO;
using EnterpriseApp.Pedido.Application.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace EnterpriseApp.Pedido.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class VouchersController : MainController
    {
        private readonly IVoucherQueries _voucherQueries;

        public VouchersController(IVoucherQueries voucherQueries)
        {
            _voucherQueries = voucherQueries;
        }

        [HttpGet("{code}")]
        [ProducesResponseType(typeof(VoucherDTO), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetByCode(string code)
        {
            if (string.IsNullOrEmpty(code))
            {
                AddError("Parameter 'code' must be informed.");
                return CustomResponse();
            }
                
            var voucher = await _voucherQueries.GetVoucherByCode(code);

            return voucher is null ? NotFound() : CustomResponse(voucher);
        }
    }
}
