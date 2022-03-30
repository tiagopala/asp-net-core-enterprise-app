using EnterpriseApp.Core.Specifications.Validations;

namespace EnterpriseApp.Pedido.Domain.Vouchers.Specifications
{
    public class VoucherValidation : SpecValidator<Voucher>
    {
        public VoucherValidation()
        {
            Add("dataSpec", new Rule<Voucher>(new VoucherDateTimeSpecification(), "Voucher expired."));
            Add("qtdeSpec", new Rule<Voucher>(new VoucherQuantitySpecification(), "Voucher already used."));
            Add("ativoSpec", new Rule<Voucher>(new VoucherActiveSpecification(), "Voucher is not active."));
        }
    }
}
