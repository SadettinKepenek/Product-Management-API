using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using ProductManagement.API.Domain.Entities;
using ProductManagement.API.Domain.Models;
using ProductManagement.API.Domain.Repositories;

namespace ProductManagement.API.Domain.Services
{
    public class ProductService:IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        public ProductService(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<List<ProductDto>> GetAll()
        {
            var products = await _productRepository.GetAll();
            return _mapper.Map<List<ProductDto>>(products);
        }

        public async Task Insert(ProductDto productDto)
        {
            var product = _mapper.Map<Product>(productDto);
            await _productRepository.Insert(product);
        }
    }
}