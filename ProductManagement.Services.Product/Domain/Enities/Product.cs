﻿using System;

namespace ProductManagement.Services.Product.Domain.Enities
{
    public class Product:IEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
    }
}