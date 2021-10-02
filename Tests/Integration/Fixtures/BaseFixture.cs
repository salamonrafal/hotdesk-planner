using System.Collections.Generic;
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
        protected const string DirectoryAppDataPath = "AppData/";
        protected const string TestAppDataFileExtension = ".json";
        
        protected IHost? Host;
        protected MongoDbRunner? Runner;
        
        public virtual async Task SetUpForSuccess()
        {
            Runner = MongoDbRunner.StartForDebugging();

            ImportAllTestData ();
            
            Host = ApplicationFactory.Create (testServices: services =>
            {
                services.AddSingleton<IMongoClient, MongoClient>(_ => new MongoClient(Runner.ConnectionString));
            });
            
            await Host.StartAsync ();
        }

        [TearDown]
        public void Stop()
        {
            Runner?.Dispose(); 
            Host?.Dispose ();
        }

        protected void ExportTestData(List<string>? toExport = null, string fileSuffix = "_export")
        {
            if ( toExport == null || toExport.Count == 0)
            {
                Runner?.Export(DatabaseName, DesksCollectionName, Path.Combine (Directory.GetCurrentDirectory (), $"{DirectoryAppDataPath}{DesksCollectionName}{fileSuffix}{TestAppDataFileExtension}"));
                Runner?.Export(DatabaseName, ReservationsCollectionName, Path.Combine (Directory.GetCurrentDirectory (), $"{DirectoryAppDataPath}{ReservationsCollectionName}{fileSuffix}{TestAppDataFileExtension}"));
                Runner?.Export(DatabaseName, UsersCollectionName, Path.Combine (Directory.GetCurrentDirectory (), $"{DirectoryAppDataPath}{UsersCollectionName}{fileSuffix}{TestAppDataFileExtension}"));
            }
            else
            {
                if (!string.IsNullOrEmpty (toExport.Find (x => x.Contains (DesksCollectionName))))
                    Runner?.Export(DatabaseName, DesksCollectionName, Path.Combine (Directory.GetCurrentDirectory (), $"{DirectoryAppDataPath}{DesksCollectionName}{fileSuffix}{TestAppDataFileExtension}"));
                
                if (!string.IsNullOrEmpty (toExport.Find (x => x.Contains (ReservationsCollectionName))))
                    Runner?.Export(DatabaseName, ReservationsCollectionName, Path.Combine (Directory.GetCurrentDirectory (), $"{DirectoryAppDataPath}{ReservationsCollectionName}{fileSuffix}{TestAppDataFileExtension}"));
                
                if (!string.IsNullOrEmpty (toExport.Find (x => x.Contains (UsersCollectionName))))
                    Runner?.Export(DatabaseName, UsersCollectionName, Path.Combine (Directory.GetCurrentDirectory (), $"{DirectoryAppDataPath}{UsersCollectionName}{fileSuffix}{TestAppDataFileExtension}"));
            }
        }

        protected void ImportAllTestData()
        {
            Runner?.Import(DatabaseName, DesksCollectionName, Path.Combine (Directory.GetCurrentDirectory (), $"{DirectoryAppDataPath}{DesksCollectionName}{TestAppDataFileExtension}"), true);
            Runner?.Import(DatabaseName, ReservationsCollectionName, Path.Combine (Directory.GetCurrentDirectory (), $"{DirectoryAppDataPath}{ReservationsCollectionName}{TestAppDataFileExtension}"), true);
            Runner?.Import(DatabaseName, UsersCollectionName, Path.Combine (Directory.GetCurrentDirectory (), $"{DirectoryAppDataPath}{UsersCollectionName}{TestAppDataFileExtension}"), true);
        }
    }
}