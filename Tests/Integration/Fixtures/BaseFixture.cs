using System.IO;
using System.Threading.Tasks;
using Api.Controllers;
using Integration.ApplicationFactories;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Mongo2Go;
using MongoDB.Driver;
using NUnit.Framework;

namespace Integration.Fixtures
{
    public class BaseFixture
    {
        private const string DatabaseName = "hotdesk_planner";
        private const string DesksCollectionName = "desks";
        private const string ReservationsCollectionName = "reservations";
        private const string UsersCollectionName = "users";
        
        protected IHost? Host;
        private MongoDbRunner? _runner;
        
        [SetUp]
        public virtual async Task SetUp()
        {
            _runner = MongoDbRunner.StartForDebugging();
            _runner.Import(DatabaseName, DesksCollectionName, Path.Combine (Directory.GetCurrentDirectory (), $"AppData/{DesksCollectionName}.json"), true);
            _runner.Import(DatabaseName, ReservationsCollectionName, Path.Combine (Directory.GetCurrentDirectory (), $"AppData/{ReservationsCollectionName}.json"), true);
            _runner.Import(DatabaseName, UsersCollectionName, Path.Combine (Directory.GetCurrentDirectory (), $"AppData/{UsersCollectionName}.json"), true);
            
            Host = ApplicationFactory.Create (testServices: services =>
            {
                services.AddSingleton<IMongoClient, MongoClient>(sp => new MongoClient(_runner.ConnectionString));
            });
            
            await Host.StartAsync ();
        }

        [TearDown]
        public void Stop()
        {
            _runner?.Dispose(); 
            Host?.Dispose ();
        }
    }
}