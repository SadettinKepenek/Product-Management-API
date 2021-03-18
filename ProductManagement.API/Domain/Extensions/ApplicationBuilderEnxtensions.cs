using System;
using System.Linq;
using Consul;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace ProductManagement.API.Domain.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseConsul(this IApplicationBuilder app)
        {
            var consulClient = app.ApplicationServices.GetRequiredService<IConsulClient>();
            var logger = app.ApplicationServices.GetRequiredService<ILoggerFactory>().CreateLogger("AppExtensions");
            var lifetime = app.ApplicationServices.GetRequiredService<IHostApplicationLifetime>();

            if (!(app.Properties["server.Features"] is FeatureCollection features)) return app;

            var applicationName = app.ApplicationServices.GetRequiredService<IConfiguration>()["ApplicationName"];
            var addresses = features.Get<IServerAddressesFeature>();
            if (addresses == null) return app;
            var address = addresses.Addresses.First();
            var uri = new Uri(address);
            var registration = new AgentServiceRegistration()
            {
                ID = $"{applicationName}-{uri.Port}",
                Name = applicationName,
                Address = uri.Host,
                Port = uri.Port
            };

            logger.LogInformation("Registering with Consul");
            consulClient.Agent.ServiceDeregister(registration.ID).GetAwaiter().GetResult();
            var result = consulClient.Agent.ServiceRegister(registration).GetAwaiter().GetResult();

            lifetime.ApplicationStopping.Register(() =>
            {
                logger.LogInformation("Unregistering from Consul");
                consulClient.Agent.ServiceDeregister(registration.ID).ConfigureAwait(true);
            });
            return app;
        }
    }
}