namespace ProductManagement.API.Domain.Models
{
    public class ProductDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string CategoryName { get; set; } 
    }
}