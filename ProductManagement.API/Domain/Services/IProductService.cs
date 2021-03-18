using System.Collections.Generic;
using System.Threading.Tasks;
using ProductManagement.API.Domain.Models;

namespace ProductManagement.API.Domain.Services
{
    public interface IProductService
    {
        Task<List<ProductDto>> GetAll();
        Task Insert(ProductDto productDto);
    }
}