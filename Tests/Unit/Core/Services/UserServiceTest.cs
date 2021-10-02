using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Infrastructure;
using Core.Models;
using Core.Services;
using FluentAssertions;
using MongoDB.Bson;
using MongoDB.Driver;
using Moq;
using NUnit.Framework;

namespace Unit.Core.Services
{
    [TestFixture]
    public class UserServiceTest
    {
        private Mock<IRepository<User>> _repository;
        private UserService _service;
        
        [SetUp]
        public void SetUp()
        {
            _repository = new Mock<IRepository<User>> ();
            _service = new UserService (_repository.Object);
        }
        
        [Test]
        public async Task ShouldReturnOneModel()
        {
            var modelInput = new User () {Id = Guid.NewGuid ()};
            var modelOutput = new User ()
            {
                Email = "test",
                Name = "test",
                Id = modelInput.Id,
                Password = "test"
            };

            _repository.Setup (x => x.SelectOne (It.IsAny<User>()))
                .ReturnsAsync (modelOutput);

            var result = await _service.Get (modelInput);

            result.Should ().NotBeNull ();
            result.Should ().BeOfType<User> ();
        }

        [Test]
        public async Task ShouldReturnListModels()
        {
            var model = new User ()
            {
                Id = Guid.NewGuid (),
                Email = "test",
                Name = "test",
                Password = "test",
                DocumentId = Guid.NewGuid ()
            };
            var modelOutput = new List<User> () {model};
            
            _repository.Setup (x => x.Select())
                .ReturnsAsync (modelOutput);
            
            var result = await _service.Get ();

            result.Should ().NotBeNull ();
            result.Should ().BeOfType<List<User>> ();
            result.Should ().HaveCount (1);
            result.Should ().HaveElementAt (0, model);
        }

        [Test]
        public async Task ShouldReturnNewGuidForAddMethod()
        {
            var model = new User ()
            {
                Id = Guid.NewGuid (),
                Email = "test",
                Name = "test",
                Password = "test",
                DocumentId = Guid.NewGuid (),
            };
            
            _repository.Setup (x => x.Insert(It.IsAny<User>()));
            
            var result = await _service.Add (model);
            
            result.Should ().NotBeEmpty ();
        }

        [Test]
        public async Task ShouldRunUpdateModelInStore()
        {
            var model = new User ()
            {
                Id = Guid.NewGuid (),
                Email = "test",
                Name = "test",
                Password = "test",
                DocumentId = Guid.NewGuid (),
            };
            
            _repository.Setup (x => x.Update(It.IsAny<User>()))
                .ReturnsAsync (true);
            
            var result = await _service.Update (model);

            result.Should ().BeTrue ();
        }

        [Test]
        public async Task ShouldRunDeleteFromStore()
        {
            var model = new User ()
            {
                Id = Guid.NewGuid ()
            };
            
            _repository.Setup (x => x.Delete(It.IsAny<User>()))
                .ReturnsAsync (true);
            
            var result = await _service.Remove (model);
            
            result.Should ().BeTrue ();
        }

        [Test]
        public async Task ShouldReturnListModelsBaseOnQuery()
        {
            const string queryString = "{id :\"id\"}";
            var id = Guid.NewGuid ();
            var queryObject = BsonDocument.Parse (queryString);
            var model = new User ()
            {
                Id = id,
                Email = "test",
                Name = "test",
                Password = "test",
                DocumentId = Guid.NewGuid (),
            };
            var modelOutput = new List<User> () {model};
            
            _repository.Setup (
                    x => x.Select(It.IsAny<QueryDocument> ())
                )
                .ReturnsAsync (modelOutput);
            
            var result = await _service.Search (queryObject);

            result.Should ().NotBeNull ();
            result.Should ().BeOfType<List<User>> ();
            result.Should ().HaveCount (1);
            result.Should ().HaveElementAt (0, model);
        }
    }
}
