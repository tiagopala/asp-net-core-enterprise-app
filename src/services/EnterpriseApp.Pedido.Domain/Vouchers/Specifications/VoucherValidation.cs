using EnterpriseApp.Core.Specifications.Validations;

namespace EnterpriseApp.Pedido.Domain.Vouchers.Specifications
{
    public class VoucherValidation : SpecValidator<Voucher>
    {
        public VoucherValidation()
        {
            var dataSpec = new VoucherDateTimeSpecification();
            var qtdeSpec = new VoucherQuantitySpecification();
            var ativoSpec = new VoucherActiveSpecification();

            Add("dataSpec", new Rule<Voucher>(dataSpec, "Voucher expired."));
            Add("qtdeSpec", new Rule<Voucher>(qtdeSpec, "Voucher already used."));
            Add("ativoSpec", new Rule<Voucher>(ativoSpec, "Voucher is not active."));
        }
    }
}
