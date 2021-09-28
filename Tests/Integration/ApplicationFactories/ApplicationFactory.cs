using System;
using Api;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Integration.ApplicationFactories
{
    internal static class ApplicationFactory
    {
        public static IHost Create(
            Action<WebHostBuilderContext, IConfigurationBuilder>? configureDelegate = null,
            Action<IServiceCollection>? testServices = null
        )
        {
            return Host.CreateDefaultBuilder ()
                .ConfigureWebHost
                (
                    builder =>
                    {
                        if ( configureDelegate != null )
                            builder.ConfigureAppConfiguration (configureDelegate);

                        builder.UseTestServer ();
                        builder.UseEnvironment ("Integration");
                        builder.UseStartup<Startup> ();

                        if ( testServices != null )
                        {
                            builder.ConfigureTestServices (testServices);
                        }
                        else
                        {
                            builder.ConfigureTestServices
                            (
                                services =>
                                {

                                }
                            );
                        }
                    }
                )
                .Build ();
        }
    }
}