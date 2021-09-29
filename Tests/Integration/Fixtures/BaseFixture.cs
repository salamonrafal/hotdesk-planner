using System.IO;
using System.Threading.Tasks;
using Integration.ApplicationFactories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Mongo2Go;
using MongoDB.Driver;
using NUnit.Framework;
// ReSharper disable MemberCanBePrivate.Global

namespace Integration.Fixtures
{
    public class BaseFixture
    {
        protected const string DatabaseName = "hotdesk_planner";
        protected const string DesksCollectionName = "desks";
        protected const string ReservationsCollectionName = "reservations";
        protected const string UsersCollectionName = "users";
        
        protected IHost? Host;
        protected MongoDbRunner? Runner;
        
        [SetUp]
        public virtual async Task SetUp()
        {
            Runner = MongoDbRunner.StartForDebugging();
            Runner.Import(DatabaseName, DesksCollectionName, Path.Combine (Directory.GetCurrentDirectory (), $"AppData/{DesksCollectionName}.json"), true);
            Runner.Import(DatabaseName, ReservationsCollectionName, Path.Combine (Directory.GetCurrentDirectory (), $"AppData/{ReservationsCollectionName}.json"), true);
            Runner.Import(DatabaseName, UsersCollectionName, Path.Combine (Directory.GetCurrentDirectory (), $"AppData/{UsersCollectionName}.json"), true);
            
            Host = ApplicationFactory.Create (testServices: services =>
            {
                services.AddSingleton<IMongoClient, MongoClient>(sp => new MongoClient(Runner.ConnectionString));
            });
            
            await Host.StartAsync ();
        }

        [TearDown]
        public void Stop()
        {
            Runner?.Dispose(); 
            Host?.Dispose ();
        }
    }
}