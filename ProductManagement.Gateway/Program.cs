using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Provider.Consul;

namespace ProductManagement.Gateway
{
    public class Program
    {
        public static void Main(string[] args)
        {
            ServicePointManager.ServerCertificateValidationCallback +=
                (sender, certificate, chain, sslPolicyErrors) => true;
            new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    config
                        .SetBasePath(hostingContext.HostingEnvironment.ContentRootPath)
                        .AddJsonFile("appsettings.json", true, true)
                        .AddJsonFile($"appsettings.{hostingContext.HostingEnvironment.EnvironmentName}.json", true,
                            true)
                        .AddJsonFile("ocelot.json")
                        .AddEnvironmentVariables();
                })
                .ConfigureServices(s =>
                {
                    s.AddHealthChecks();
                    s.AddOcelot()
                        .AddConsul();
                })
                .ConfigureLogging((hostingContext, logging) =>
                {
                    //add your logging
                })
                .UseIISIntegration()
                .Configure(async app =>
                {
                    app.UseHealthChecks("/healthcheck");
                    await app.UseOcelot();
                })
                .UseUrls("http://*:5001")
                .Build()
                .Run();
        }
    }
}