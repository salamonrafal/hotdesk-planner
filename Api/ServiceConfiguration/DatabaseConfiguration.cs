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
            
            var dbConfig = configuration.GetSection ("Database");
            
            var template = dbConfig.GetValue<string>("ConnectionString");
            var user = GetDatabaseUser(configuration);
            var password = GetDatabasePassword(configuration);
            var server = dbConfig.GetValue<string>("Server");
            var database = dbConfig.GetValue<string>("Database");
            var connectionString = string.Format(template, user, password, server, database);
            var databaseSettings = MongoClientSettings.FromConnectionString(connectionString);

            services.AddSingleton<IMongoClient, MongoClient>(_ => new MongoClient(databaseSettings));
        }

        private static string GetDatabaseUser(IConfiguration configuration)
        {
            var dbConfig = configuration.GetSection ("Database");

            return dbConfig.GetValue<string>("User");
        }
        
        private static string GetDatabasePassword(IConfiguration configuration)
        {
            var dbConfig = configuration.GetSection ("Database");
            
            return dbConfig.GetValue<string>("Password");
        }
    }
}
