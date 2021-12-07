﻿using System;

namespace EnterpriseApp.WebApp.MVC.Models
{
    public class ProductViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Active { get; set; }
        public decimal Price { get; set; }
        public DateTime InsertDate { get; set; }
        public string Image { get; set; }
        public int StockQuantity { get; set; }
    }
}
