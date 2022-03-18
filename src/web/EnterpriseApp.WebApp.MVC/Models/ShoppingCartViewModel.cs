using System;
using System.Collections.Generic;

namespace EnterpriseApp.WebApp.MVC.Models
{
    public class ShoppingCartViewModel
    {
        public decimal TotalPrice { get; set; }
        public bool HasUsedVoucher { get; set; }
        public decimal Discount { get; set; }
        public VoucherViewModel Voucher { get; set; }
        public List<ItemShoppingCartViewModel> Items { get; set; } = new List<ItemShoppingCartViewModel>();
    }

    public class ItemShoppingCartViewModel
    {
        public Guid ProductId { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string Image { get; set; }
    }
}
