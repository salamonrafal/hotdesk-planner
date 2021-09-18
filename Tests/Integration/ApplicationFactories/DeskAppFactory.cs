using Core.Infrastructure;
using Core.Models;
using Integration.ApplicationFactories.Mocks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Integration.ApplicationFactories
{
    public class DeskAppFactory : WebApplicationFactory<Api.Startup>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            // will be called after the `ConfigureServices` from the Startup
            builder.ConfigureTestServices(services =>
            {
                services.AddSingleton(typeof(IRepository<Desk>), typeof(DeskRepositoryMock));
            });
        }
    }
}
