using System;
using System.Linq;
using Couchbase.Configuration.Client;
using Couchbase.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProductManagement.API.Domain.Providers;
using ProductManagement.API.Domain.Repositories;
using ProductManagement.API.Domain.Services;

namespace ProductManagement.API.Domain.Extensions
{
    public static class ServiceCollectionExtensions
    {
      
        public static IServiceCollection ConfigureCouchbase(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddCouchbase(client =>
            {
                var ipList = configuration["CouchbaseServerUrl"].Split(',').Select(ip => new Uri(ip)).ToList();
                client.Servers = ipList;
                client.UseSsl = false;
                client.Username = configuration["CouchbaseUserName"];
                client.Password = configuration["CouchbasePassword"];
                client.UseConnectionPooling = true;
                client.ConnectionPool = new ConnectionPoolDefinition
                {
                    SendTimeout = 120000,
                    MaxSize = 20,
                    MinSize = 20
                };
                client.OperationLifespan = 90000;
            }).AddCouchbaseBucket<IProductBucketProvider>("Product");
            return services;
        }
        
        public static IServiceCollection AddServicesAndRepositories(this IServiceCollection services)
        {
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IProductRepository, ProductRepository>();
            return services;
        }
    }
}