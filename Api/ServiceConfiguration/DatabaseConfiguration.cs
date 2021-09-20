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
            
            var template = configuration.GetSection("Database:ConnectionString").Value;
            var user = GetDatabaseUser(configuration);
            var password = GetDatabasePassword(configuration);
            var server = configuration.GetSection("Database:Server").Value;
            var database = configuration.GetSection("Database:Database").Value;
            var connectionString = string.Format(template, user, password, server, database);
            var databaseSettings = MongoClientSettings.FromConnectionString(connectionString);

            services.AddSingleton<IMongoClient, MongoClient>(sp => new MongoClient(databaseSettings));
        }

        private static string GetDatabaseUser(IConfiguration configuration)
        {
            var userConfiguration = configuration["Database:User"] != null
                ? configuration["Database:User"]
                : configuration["MongoDB:DB_USER"] ?? "";

            return Environment.GetEnvironmentVariable("DB_USER") != ""
                ? Environment.GetEnvironmentVariable("DB_USER")
                : userConfiguration;
        }
        
        private static string GetDatabasePassword(IConfiguration configuration)
        {
            var passwordConfiguration = configuration["Database:Password"] != null
                ? configuration["Database:Password"]
                : configuration["MongoDB:DB_PASSWORD"] ?? "";

            return Environment.GetEnvironmentVariable("DB_PASSWORD") != ""
                ? Environment.GetEnvironmentVariable("DB_PASSWORD")
                : passwordConfiguration;
        }
    }
}
