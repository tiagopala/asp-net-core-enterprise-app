using System.Collections.Generic;

namespace EnterpriseApp.BFF.Compras.Models
{
    public class CartDTO
    {
        public decimal TotalPrice { get; set; }
        public VoucherDTO Voucher { get; set; }
        public bool HasUsedVoucher { get; set; }
        public decimal Discount { get; set; }
        public List<ItemCartDTO> Items { get; set; } = new List<ItemCartDTO>();
    }
}
