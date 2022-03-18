using System;
using System.Collections.Generic;

namespace EnterpriseApp.WebApp.MVC.Models
{
    public class OrderViewModel
    {
        #region Pedido
        public int Code { get; set; }
        // Autorizado = 1,
        // Pago = 2,
        // Recusado = 3,
        // Entregue = 4,
        // Cancelado = 5
        public int Status { get; set; }
        public DateTime Date { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal Discount { get; set; }
        public bool HasUsedVoucher { get; set; }
        public List<ItemOrderViewModel> OrderItems { get; set; } = new List<ItemOrderViewModel>();
        #endregion

        #region Order Item
        public class ItemOrderViewModel
        {
            public Guid ProductId { get; set; }
            public string Name { get; set; }
            public int Quantity { get; set; }
            public decimal Value { get; set; }
            public string Image { get; set; }
        }
        #endregion

        #region Address
        public AddressViewModel Address { get; set; }
        #endregion
    }
}
