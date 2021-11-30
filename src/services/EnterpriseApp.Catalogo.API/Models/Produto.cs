using EnterpriseApp.Core.DomainObjects;
using System;

namespace EnterpriseApp.Catalogo.API.Models
{
    public class Produto : Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Active { get; set; }
        public decimal Price { get; set; }
        public DateTime InsertDate { get; set; }
        public string Image { get; set; }
        public int StockQuantity { get; set; }
    }
}
