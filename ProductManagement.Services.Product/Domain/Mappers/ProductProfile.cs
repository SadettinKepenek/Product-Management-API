using AutoMapper;
using ProductManagement.Services.Product.Domain.Models;

namespace ProductManagement.Services.Product.Domain.Mappers
{
    public class ProductProfile:Profile
    {
        public ProductProfile()
        {
            CreateMap<Enities.Product, ProductDto>().ReverseMap();
        }
    }
}