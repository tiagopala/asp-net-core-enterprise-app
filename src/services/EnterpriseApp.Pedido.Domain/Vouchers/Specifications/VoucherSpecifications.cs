using EnterpriseApp.Core.Specifications;
using System;
using System.Linq.Expressions;

namespace EnterpriseApp.Pedido.Domain.Vouchers.Specifications
{
    // Arquivo responsável por agrupar as especificações referentes a minha entidade voucher

    public class VoucherDateTimeSpecification : Specification<Voucher>
    {
        public override Expression<Func<Voucher, bool>> ToExpression()
            => voucher => voucher.MaximumValidationDate >= DateTime.Now;
    }

    public class VoucherQuantitySpecification : Specification<Voucher>
    {
        public override Expression<Func<Voucher, bool>> ToExpression()
            => voucher => voucher.Quantity>= 0;
    }

    public class VoucherActiveSpecification : Specification<Voucher>
    {
        public override Expression<Func<Voucher, bool>> ToExpression()
            => voucher => voucher.Active && !voucher.Used;
    }
}
