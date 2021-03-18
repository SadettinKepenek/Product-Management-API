using System.Collections.Generic;
using System.Threading.Tasks;
using ProductManagement.API.Domain.Entities;

namespace ProductManagement.API.Domain.Repositories
{
    public interface IProductRepository
    {
        Task<List<Product>> GetAll();
        Task Insert(Product product);
    }
}