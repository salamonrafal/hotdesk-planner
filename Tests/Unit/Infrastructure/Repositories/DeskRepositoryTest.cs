using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Core.Infrastructure;
using Core.Models;
using Core.Options;
using FluentAssertions;
using Infrastructure.Repositories;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Moq;
using NUnit.Framework;
using Unit.Helpers;

namespace Unit.Infrastructure.Repositories
{
    [TestFixture]
    public class DeskRepositoryTest
    {
        private Mock<IMongoClient> _mongoClientMock;
        private IOptions<DatabaseOptions> _options;
        private Mock<IMongoCollection<Desk>> _mongoCollectionMock;
        private Mock<IMongoDatabase> _mongoDatabaseMock;
        
        private IRepository<Desk> _repository;

        [SetUp]
        public void SetUp()
        {
            _mongoClientMock = new Mock<IMongoClient> ();
            _mongoDatabaseMock = new Mock<IMongoDatabase> ();
            _mongoCollectionMock = new Mock<IMongoCollection<Desk>> ();

            _mongoDatabaseMock.Setup (
                    x => x.GetCollection<Desk> (
                        It.IsAny<string> (), 
                        It.IsAny<MongoCollectionSettings>()
                    )
                )
                .Returns (_mongoCollectionMock.Object);

            _mongoClientMock.Setup (
                    x => x.GetDatabase (
                        It.IsAny<string> (), 
                        It.IsAny<MongoDatabaseSettings>()
                    )
                )
                .Returns (_mongoDatabaseMock.Object);
            
            _options = Options.Create (new DatabaseOptions ()
            {
                Database = "test",
                Password = "test",
                Server = "test",
                Type = "test",
                User = "test",
                ConnectionString = "test"
            });
            
            _repository = new DeskRepository<Desk> (_mongoClientMock.Object, _options);
        }
        
        [Test]
        public async Task ShouldExecuteDeleteCommand()
        {
            _mongoCollectionMock.Setup (
                x => x.DeleteOneAsync (
                    It.IsAny<FilterDefinition<Desk>> (),
                    It.IsAny<CancellationToken>()
            ));
            
            var model = new Desk () {Id = Guid.NewGuid ()};
            var result = await _repository.Delete (model);

            result.Should ().BeTrue ();
        }

        [Test]
        public async Task ShouldExecuteInsertCommand()
        {
            _mongoCollectionMock.Setup (
                x => x.InsertOneAsync (
                    It.IsAny<Desk> (),
                    It.IsAny<InsertOneOptions>(),
                    It.IsAny<CancellationToken>()
                ));
            
            var model = new Desk () {Id = Guid.NewGuid ()};
            await _repository.Insert (model);
            
            _mongoCollectionMock.Verify(
                x => x.InsertOneAsync (
                    It.IsAny<Desk>(), 
                    It.IsAny<InsertOneOptions>(),
                    It.IsAny<CancellationToken>()
                ), 
                Times.Once()
            );
        }

        [Test]
        public async Task ShouldExecuteUpdateCommand()
        {
            _mongoCollectionMock.Setup (
                x => x.UpdateOneAsync (
                    It.IsAny<FilterDefinition<Desk>> (),
                   It.IsAny<UpdateDefinition<Desk>>(),
                    It.IsAny<UpdateOptions>(),
                    It.IsAny<CancellationToken>()
                ));
            
            var model = new Desk () {Id = Guid.NewGuid (), Description = "test"};
            var result = await _repository.Update (model);
            
            _mongoCollectionMock.Verify(
                x => x.UpdateOneAsync (
                    It.IsAny<FilterDefinition<Desk>> (),
                    It.IsAny<UpdateDefinition<Desk>>(),
                    It.IsAny<UpdateOptions>(),
                    It.IsAny<CancellationToken>()
                ), 
                Times.Once()
            );
            result.Should ().BeTrue ();
        }

        [Test]
        public async Task ShouldReturnOneModel()
        {
            var model = new Desk () {Id = Guid.NewGuid ()};
            var listToOutput = new List<Desk> ()
            {
                new Desk ()
                {
                    Id = model.Id,
                    Description = "test"
                }
            };

            _mongoCollectionMock
                .Setup (m => m.FindAsync (
                    It.IsAny<FilterDefinition<Desk>>(),
                    It.IsAny<FindOptions<Desk,Desk>>(),
                    It.IsAny<CancellationToken>()
                ))
            .ReturnsAsync(MocksExtensions.CreateAsyncCursor(listToOutput).Object).Verifiable();
            
            var result = await _repository.SelectOne (model);
            
            result.Should ().BeOfType<Desk> ();
            result.Id.Should ().Be (model.Id);
            result.Description.Should ().Be ("test");
        }

        [Test]
        public async Task ShouldReturnAllModels()
        {
            
            var listToOutput = new List<Desk> ()
            {
                new Desk ()
                {
                    Id = Guid.NewGuid (),
                    Description = "test1"
                },
                new Desk ()
                {
                    Id = Guid.NewGuid (),
                    Description = "test2"
                }
            };

            _mongoCollectionMock
                .Setup (m => m.FindAsync (
                    It.IsAny<FilterDefinition<Desk>>(),
                    It.IsAny<FindOptions<Desk,Desk>>(),
                    It.IsAny<CancellationToken>()
                ))
                .ReturnsAsync(MocksExtensions.CreateAsyncCursor(listToOutput).Object).Verifiable();
            
            var result = await _repository.Select ();

            result.Should ().HaveCount (2);
            result[0].Should ().BeOfType<Desk> ();
            result[0].Id.Should ().NotBeEmpty ();
            result[0].Description.Should ().Be ("test1");
            result[1].Id.Should ().NotBeEmpty ();
            result[1].Description.Should ().Be ("test2");
        }

        [Test]
        public async Task ShouldReturnListModelsFulfillingConditions()
        {
            var query = new QueryDocument ();
            
            var listToOutput = new List<Desk> ()
            {
                new Desk ()
                {
                    Id = Guid.NewGuid (),
                    Description = "test1"
                },
                new Desk ()
                {
                    Id = Guid.NewGuid (),
                    Description = "test2"
                }
            };

            _mongoCollectionMock
                .Setup (m => m.FindAsync (
                    It.IsAny<FilterDefinition<Desk>>(),
                    It.IsAny<FindOptions<Desk,Desk>>(),
                    It.IsAny<CancellationToken>()
                ))
                .ReturnsAsync(MocksExtensions.CreateAsyncCursor(listToOutput).Object).Verifiable();
            
            var result = await _repository.Select (query);

            result.Should ().HaveCount (2);
            result[0].Should ().BeOfType<Desk> ();
            result[0].Id.Should ().NotBeEmpty ();
            result[0].Description.Should ().Be ("test1");
            result[1].Id.Should ().NotBeEmpty ();
            result[1].Description.Should ().Be ("test2");
        }
    }
}
