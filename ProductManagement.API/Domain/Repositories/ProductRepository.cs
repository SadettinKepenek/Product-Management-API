using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Couchbase.Core;
using Couchbase.N1QL;
using ProductManagement.API.Domain.Entities;
using ProductManagement.API.Domain.Providers;

namespace ProductManagement.API.Domain.Repositories
{
    public class ProductRepository : IProductRepository

    {
        private readonly IBucket _bucket;

        public ProductRepository(IProductBucketProvider productBucketProvider)
        {
            _bucket = productBucketProvider.GetBucket();
        }

        public async Task<List<Product>> GetAll()
        {
            var n1ql = @"SELECT Product.* FROM Product";
            var query = QueryRequest.Create(n1ql);
            var result = await _bucket.QueryAsync<Product>(query);
            return result.Rows;
        }

        public async Task Insert(Product product)
        {
            var key = Guid.NewGuid().ToString();
            await _bucket.InsertAsync(key, product);
        }
    }
}