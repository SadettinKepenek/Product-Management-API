using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using ProductManagement.Services.Product.Domain.Models;
using ProductManagement.Services.Product.Domain.Repositories;

namespace ProductManagement.Services.Product.Domain.Services
{
    public class ProductService : IProductService
    {
        private readonly IRepository<Enities.Product> _productRepository;
        private readonly IMapper _mapper;

        public ProductService(IRepository<Enities.Product> productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<List<ProductDto>> GetAll()
        {
            var products = await  _productRepository.GetAllAsync();
            return _mapper.Map<List<ProductDto>>(products);
        }

        public async Task Create(ProductDto productDto)
        {
            var product = _mapper.Map<Enities.Product>(productDto);
            await _productRepository.CreateAsync(product);
        }
    }
}