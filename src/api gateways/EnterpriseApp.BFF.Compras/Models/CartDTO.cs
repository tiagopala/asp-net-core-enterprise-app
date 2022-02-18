using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EnterpriseApp.BFF.Compras.Models
{
    public class CartDTO
    {
        public decimal TotalPrice { get; set; }
        public VoucherDTO Voucher { get; set; }
        public bool IsVoucherUsed { get; set; }
        public decimal Discount { get; set; }
        public List<ItemCarrinhoDTO> Itens { get; set; } = new List<ItemCarrinhoDTO>();
    }
}
