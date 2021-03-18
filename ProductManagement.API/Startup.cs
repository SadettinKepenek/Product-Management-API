using System;
using System.Linq;
using Consul;
using Couchbase.Configuration.Client;
using Couchbase.Extensions.DependencyInjection;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using ProductManagement.API.Domain.Extensions;
using ProductManagement.API.Domain.Providers;
using ProductManagement.API.Domain.Repositories;
using ProductManagement.API.Domain.Services;
using ProductManagement.API.Domain.Validators;


namespace ProductManagement.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers(options => { options.AllowEmptyInputInBodyModelBinding = true; })
                .AddFluentValidation(o => { o.RegisterValidatorsFromAssemblyContaining<ProductDtoValidator>(); }
                )
                .AddJsonOptions(opt => { opt.JsonSerializerOptions.IgnoreNullValues = true; }
                );
            services.AddAutoMapper(typeof(Startup));
            services.AddServicesAndRepositories();
            services.ConfigureCouchbase(Configuration);
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "Product Management API", Version = "v1"});
            });

            services.AddSingleton<IConsulClient, ConsulClient>(p => new ConsulClient(consulConfig =>
            {
                var address = Configuration.GetValue<string>("Consul:Host");
                consulConfig.Address = new Uri(address);
            }));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IHostApplicationLifetime appLifetime)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Product Management API v1"));
            }


            app.UseConsul();

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}