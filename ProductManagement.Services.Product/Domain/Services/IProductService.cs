using System.Collections.Generic;
using System.Threading.Tasks;
using ProductManagement.Services.Product.Domain.Models;

namespace ProductManagement.Services.Product.Domain.Services
{
    public interface IProductService
    {
        Task<List<ProductDto>> GetAll();
        Task Create(ProductDto productDto);
    }
}