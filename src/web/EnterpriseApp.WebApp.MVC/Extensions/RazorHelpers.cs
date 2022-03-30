using Microsoft.AspNetCore.Mvc.Razor;
using System.Text;
using System.Threading;

namespace EnterpriseApp.WebApp.MVC.Extensions
{
    public static class RazorHelpers
    {
        public static string FormatCurrency(this RazorPage _, decimal price)
            => price > 0 ? string.Format(Thread.CurrentThread.CurrentCulture, "{0:C}", price) : "Gratuito";

        private static string FormatCurrency(decimal valor)
            => string.Format(Thread.CurrentThread.CurrentCulture, "{0:C}", valor);

        public static string StockMessage(this RazorPage _, int quantity)
            => quantity > 0 ? $"Only {quantity} in stock" : "Product out of stock";

        public static string UnitiesPerProduct(this RazorPage page, int quantity)
            => quantity > 1 ? $"{quantity} unities" : $"{quantity} unity";

        public static string UnitiesPerProductTotalPrice(this RazorPage _, int unities, decimal price)
            => $"{unities}x {FormatCurrency(price)} = Total: {FormatCurrency(price * unities)}";

        public static string SelectOptionsPerQuantity(this RazorPage page, int quantity, int selectedValue = 0)
        {
            var sb = new StringBuilder();
            for (var i = 1; i <= quantity; i++)
            {
                var selected = "";
                if (i == selectedValue) selected = "selected";
                sb.Append($"<option {selected} value='{i}'>{i}</option>");
            }

            return sb.ToString();
        }

        public static string ShowStatus(this RazorPage _, int status)
        {
            var statusMessage = "";
            var statusClass = "";

            switch (status)
            {
                case 1:
                    statusClass = "info";
                    statusMessage = "Pending";
                    break;
                case 2:
                    statusClass = "primary";
                    statusMessage = "Approved";
                    break;
                case 3:
                    statusClass = "danger";
                    statusMessage = "Refused";
                    break;
                case 4:
                    statusClass = "success";
                    statusMessage = "Delivered";
                    break;
                case 5:
                    statusClass = "warning";
                    statusMessage = "Cancelled";
                    break;

            }

            return $"<span class='badge badge-{statusClass}'>{statusMessage}</span>";
        }
    }
}
