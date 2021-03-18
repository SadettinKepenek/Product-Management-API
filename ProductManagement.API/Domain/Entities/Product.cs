using System;

namespace ProductManagement.API.Domain.Entities
{
    public class Product
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string CategoryName { get; set; } 
        
    }
}