using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ProductManagement.Services.Product.Domain.Repositories
{
    public interface IRepository<T> where T : class
    {
        Task<T> CreateAsync(T data);

        Task<T> ReadAsync(Guid id);

        Task<bool> UpdateAsync(T data);

        Task<bool> DeleteAsync(Guid id);

        Task<List<T>> GetAllAsync();

        Task<IEnumerable<T>> QueryAsync(Expression<Func<T, bool>> predicate = null);
    }
}