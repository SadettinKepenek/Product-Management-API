using AutoMapper;
using ProductManagement.API.Domain.Entities;
using ProductManagement.API.Domain.Models;

namespace ProductManagement.API.Domain.Mappers
{
    public class ProductProfile:Profile
    {
        public ProductProfile()
        {
            CreateMap<ProductDto, Product>();
            CreateMap<Product, ProductDto>();
        }
    }
}