using EnterpriseApp.Core.Validation;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace EnterpriseApp.WebApp.MVC.Models
{
    public class OrderTransactionViewModel
    {
        #region Order
        public decimal TotalPrice { get; set; }
        public decimal Discount { get; set; }
        public string VoucherCode { get; set; }
        public bool HasUsedVoucher { get; set; }
        public List<ItemShoppingCartViewModel> Items { get; set; } = new List<ItemShoppingCartViewModel>();
        #endregion

        #region Address
        public AddressViewModel Address { get; set; }
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
