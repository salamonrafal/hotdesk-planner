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
        protected IHost _host;
        protected MongoDbRunner _runner;

        
        [SetUp]
        public virtual async Task SetUp()
        {
            _runner = MongoDbRunner.StartForDebugging();
            _runner.Import("hotdesk_planner", "desks", Path.Combine (Directory.GetCurrentDirectory (), "AppData/desks.json"), true);
            _runner.Import("hotdesk_planner", "desks", Path.Combine (Directory.GetCurrentDirectory (), "AppData/reservations.json"), true);
            _runner.Import("hotdesk_planner", "desks", Path.Combine (Directory.GetCurrentDirectory (), "AppData/users.json"), true);
            
            _host = ApplicationFactory.Create (testServices: services =>
            {
                services.AddSingleton<IMongoClient, MongoClient>(sp => new MongoClient(_runner.ConnectionString));
            });
            
            await _host.StartAsync ();
        }

        [TearDown]
        public void Stop()
        {
            _runner.Dispose(); 
            _host.Dispose ();
        }
    }
}