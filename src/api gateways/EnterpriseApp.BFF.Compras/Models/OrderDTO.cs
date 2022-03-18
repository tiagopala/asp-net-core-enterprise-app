using EnterpriseApp.Core.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace EnterpriseApp.BFF.Compras.Models
{
    public class OrderDTO
    {
        #region Pedido
        public string Code { get; set; }
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
        public List<ItemCartDTO> OrderItems { get; set; } = new();
        #endregion

        #region Address
        public AddressDTO Address { get; set; }
        #endregion

        #region Card
        [Required(ErrorMessage = "Card number is required")]
        [DisplayName("Card Number")]
        public string CardNumber { get; set; }

        [Required(ErrorMessage = "Card Name is required")]
        [DisplayName("Card Name")]
        public string CardName { get; set; }

        [RegularExpression(@"(0[1-9]|1[0-2])\/[0-9]{2}", ErrorMessage = "Invalid card expiration format. Correct format: 'MM/AA'")]
        [CardExpiration(ErrorMessage = "Invalid expired card.")]
        [Required(ErrorMessage = "Expiration Date must be informed")]
        [DisplayName("Expiration Date 'MM/AA'")]
        public string CardExpirationDate { get; set; }

        [Required(ErrorMessage = "Security code must be informed")]
        [DisplayName("Security Code")]
        public string CardCvv { get; set; }
        #endregion
    }
}
