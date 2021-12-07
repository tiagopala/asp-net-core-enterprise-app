using Microsoft.AspNetCore.Mvc.Razor;
using System.Threading;

namespace EnterpriseApp.WebApp.MVC.Extensions
{
    public static class RazorHelpers
    {
        public static string FormatCurrency(this RazorPage _, decimal price)
            => price > 0 ? string.Format(Thread.CurrentThread.CurrentCulture, "{0:C}", price) : "Gratuito";

        public static string StockMessage(this RazorPage _, int quantity)
            => quantity > 0 ? $"Only {quantity} in stock" : "Product out of stock";
    }
}
