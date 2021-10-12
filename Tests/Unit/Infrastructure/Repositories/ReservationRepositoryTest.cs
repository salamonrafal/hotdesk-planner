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
    public class ReservationRepositoryTest
    {
         private Mock<IMongoClient> _mongoClientMock;
        private IOptions<DatabaseOptions> _options;
        private Mock<IMongoCollection<Reservation>> _mongoCollectionMock;
        private Mock<IMongoDatabase> _mongoDatabaseMock;
        
        private IRepository<Reservation> _repository;

        [SetUp]
        public void SetUp()
        {
            _mongoClientMock = new Mock<IMongoClient> ();
            _mongoDatabaseMock = new Mock<IMongoDatabase> ();
            _mongoCollectionMock = new Mock<IMongoCollection<Reservation>> ();

            _mongoDatabaseMock.Setup (
                    x => x.GetCollection<Reservation> (
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
            });
            
            _repository = new ReservationRepository<Reservation> (_mongoClientMock.Object, _options);
        }
        
        [Test]
        public async Task ShouldExecuteDeleteCommand()
        {
            _mongoCollectionMock.Setup (
                x => x.DeleteOneAsync (
                    It.IsAny<FilterDefinition<Reservation>> (),
                    It.IsAny<CancellationToken>()
            ));
            
            var model = new Reservation () {Id = Guid.NewGuid ()};
            var result = await _repository.Delete (model);

            result.Should ().BeTrue ();
        }

        [Test]
        public async Task ShouldExecuteInsertCommand()
        {
            _mongoCollectionMock.Setup (
                x => x.InsertOneAsync (
                    It.IsAny<Reservation> (),
                    It.IsAny<InsertOneOptions>(),
                    It.IsAny<CancellationToken>()
                ));
            
            var model = new Reservation () {Id = Guid.NewGuid ()};
            await _repository.Insert (model);
            
            _mongoCollectionMock.Verify(
                x => x.InsertOneAsync (
                    It.IsAny<Reservation>(), 
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
                    It.IsAny<FilterDefinition<Reservation>> (),
                   It.IsAny<UpdateDefinition<Reservation>>(),
                    It.IsAny<UpdateOptions>(),
                    It.IsAny<CancellationToken>()
                ));
            
            var model = new Reservation () {Id = Guid.NewGuid (), StartDate = DateTime.Now};
            var result = await _repository.Update (model);
            
            _mongoCollectionMock.Verify(
                x => x.UpdateOneAsync (
                    It.IsAny<FilterDefinition<Reservation>> (),
                    It.IsAny<UpdateDefinition<Reservation>>(),
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
            var now = DateTime.Now;
            var model = new Reservation () {Id = Guid.NewGuid ()};
            var listToOutput = new List<Reservation> ()
            {
                new Reservation ()
                {
                    Id = model.Id,
                    StartDate = now
                }
            };

            _mongoCollectionMock
                .Setup (m => m.FindAsync (
                    It.IsAny<FilterDefinition<Reservation>>(),
                    It.IsAny<FindOptions<Reservation,Reservation>>(),
                    It.IsAny<CancellationToken>()
                ))
            .ReturnsAsync(MocksExtensions.CreateAsyncCursor(listToOutput).Object).Verifiable();
            
            var result = await _repository.SelectOne (model);
            
            result.Should ().BeOfType<Reservation> ();
            result.Id.Should ().Be (model.Id);
            result.StartDate.Should ().Be (now);
        }

        [Test]
        public async Task ShouldReturnAllModels()
        {
            var now = DateTime.Now;
            var listToOutput = new List<Reservation> ()
            {
                new Reservation ()
                {
                    Id = Guid.NewGuid (),
                    StartDate = now
                },
                new Reservation ()
                {
                    Id = Guid.NewGuid (),
                    StartDate = now
                }
            };

            _mongoCollectionMock
                .Setup (m => m.FindAsync (
                    It.IsAny<FilterDefinition<Reservation>>(),
                    It.IsAny<FindOptions<Reservation,Reservation>>(),
                    It.IsAny<CancellationToken>()
                ))
                .ReturnsAsync(MocksExtensions.CreateAsyncCursor(listToOutput).Object).Verifiable();
            
            var result = await _repository.Select ();

            result.Should ().HaveCount (2);
            result[0].Should ().BeOfType<Reservation> ();
            result[0].Id.Should ().NotBeEmpty ();
            result[0].StartDate.Should ().Be (now);
            result[1].Id.Should ().NotBeEmpty ();
            result[1].StartDate.Should ().Be (now);
        }

        [Test]
        public async Task ShouldReturnListModelsFulfillingConditions()
        {
            var query = new QueryDocument ();
            var now = DateTime.Now;
            
            var listToOutput = new List<Reservation> ()
            {
                new Reservation ()
                {
                    Id = Guid.NewGuid (),
                    StartDate = now
                },
                new Reservation ()
                {
                    Id = Guid.NewGuid (),
                    StartDate = now
                }
            };

            _mongoCollectionMock
                .Setup (m => m.FindAsync (
                    It.IsAny<FilterDefinition<Reservation>>(),
                    It.IsAny<FindOptions<Reservation,Reservation>>(),
                    It.IsAny<CancellationToken>()
                ))
                .ReturnsAsync(MocksExtensions.CreateAsyncCursor(listToOutput).Object).Verifiable();
            
            var result = await _repository.Select (query);

            result.Should ().HaveCount (2);
            result[0].Should ().BeOfType<Reservation> ();
            result[0].Id.Should ().NotBeEmpty ();
            result[0].StartDate.Should ().Be (now);
            result[1].Id.Should ().NotBeEmpty ();
            result[1].StartDate.Should ().Be (now);
        }
    }
}
