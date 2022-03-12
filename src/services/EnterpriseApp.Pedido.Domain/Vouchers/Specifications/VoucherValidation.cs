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

            Add("dataSpec", new Rule<Voucher>(dataSpec, "Este voucher está expirado"));
            Add("qtdeSpec", new Rule<Voucher>(qtdeSpec, "Este voucher já foi utilizado"));
            Add("ativoSpec", new Rule<Voucher>(ativoSpec, "Este voucher não está mais ativo"));
        }
    }
}
