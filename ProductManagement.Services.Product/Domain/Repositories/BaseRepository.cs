using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using Couchbase.Core;
using Couchbase.IO;
using Couchbase.N1QL;
using ProductManagement.Services.Product.Domain.Enities;
using ProductManagement.Services.Product.Domain.Providers;
using ProductManagement.Services.Product.Domain.Queries;

namespace ProductManagement.Services.Product.Domain.Repositories
{
    public class BaseRepository<T>:IRepository<T> where T:class,IEntity
    {
        private readonly IBucket _bucket;

        public BaseRepository(IProductCouchbaseProvider productCouchbaseProvider)
        {
            _bucket = productCouchbaseProvider.GetBucket();
        }
        public async Task<T> CreateAsync(T data)
        {
            var key = Guid.NewGuid().ToString();
            var result = await _bucket.InsertAsync(key,data);
            if (result.Status != ResponseStatus.Success)
            {
                throw new OperationCanceledException(result.Message);
            }
            data.Id = Guid.Parse(result.Id);
            return data;
        }

        public Task<T> ReadAsync(Guid id)
        {
            return null;
        }

        public Task<bool> UpdateAsync(T data)
        {
            return null;
        }

        public Task<bool> DeleteAsync(Guid id)
        {
            return null;
        }
        

        public async Task<List<T>> GetAllAsync()
        {
            var queryRequest = QueryRequest.Create(ProductQueries.GetAll);
            var result = await _bucket.QueryAsync<T>(queryRequest);
            return result.Rows;
        }

        public Task<IEnumerable<T>> QueryAsync(Expression<Func<T, bool>> predicate = null)
        {
            return null;
        }
    }
}