﻿using Microsoft.AspNetCore.Mvc.Razor;
using System.Text;
using System.Threading;

namespace EnterpriseApp.WebApp.MVC.Extensions
{
    public static class RazorHelpers
    {
        public static string FormatCurrency(this RazorPage _, decimal price)
            => price > 0 ? string.Format(Thread.CurrentThread.CurrentCulture, "{0:C}", price) : "Gratuito";

        public static string StockMessage(this RazorPage _, int quantity)
            => quantity > 0 ? $"Only {quantity} in stock" : "Product out of stock";

        public static string UnitiesPerProduct(this RazorPage page, int quantity)
            => quantity > 1 ? $"{quantity} unities" : $"{quantity} unity";

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
    }
}
