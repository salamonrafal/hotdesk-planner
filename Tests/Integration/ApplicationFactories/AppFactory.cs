using System;
using Core.Infrastructure;
using Core.Models;
using Integration.ApplicationFactories.Mocks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Mongo2Go;
using MongoDB.Driver;

namespace Integration.ApplicationFactories
{
    public class AppFactory : WebApplicationFactory<Api.Startup>
    {
        public IConfiguration Configuration { get; set; }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            Environment.SetEnvironmentVariable("DB_USER", "Integration");
            Environment.SetEnvironmentVariable("DB_PASSWORD", "Integration");

            // will be called after the `ConfigureServices` from the Startup
            builder.ConfigureTestServices(services =>
            {
                services.AddSingleton(typeof(IRepository<Desk>), typeof(RepositoryMock<Desk>));
                services.AddSingleton(typeof(IRepository<Reservation>), typeof(RepositoryMock<Reservation>));
                services.AddSingleton(typeof(IRepository<User>), typeof(RepositoryMock<User>));
            });
        }
    }
}
