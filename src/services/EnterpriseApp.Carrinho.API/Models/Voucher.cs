namespace EnterpriseApp.Carrinho.API.Models
{
    public class Voucher
    {
        public decimal? Percent { get; set; }
        public decimal? DiscountValue { get; set; }
        public string Code { get; set; }
        public VoucherDiscountType DiscountType { get; set; }
    }

    public enum VoucherDiscountType
    {
        Percentage = 0,
        Value = 1
    }
}
