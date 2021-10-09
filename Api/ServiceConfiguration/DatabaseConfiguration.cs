using Core.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using System;

namespace Api.ServiceConfiguration
{
    public static class DatabaseConfiguration
    {
        public static void DatabaseConfigure(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<DatabaseOptions>(options => configuration.GetSection("Database").Bind(options));
            var connectionStringEnv = Environment.GetEnvironmentVariable ("SERVICE_MONGODB_CS");

            services.AddSingleton<IMongoClient, MongoClient>(_ => new MongoClient(!string.IsNullOrEmpty (connectionStringEnv) ? 
                connectionStringEnv : 
                configuration.GetSection ("SERVICE_MONGODB_CS").Value
            ));
        }
    }
}
