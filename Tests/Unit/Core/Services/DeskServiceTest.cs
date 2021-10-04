using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Infrastructure;
using Core.Models;
using Core.Services;
using FluentAssertions;
using FluentValidation;
using MongoDB.Bson;
using MongoDB.Driver;
using Moq;
using NUnit.Framework;

namespace Unit.Core.Services
{
    [TestFixture]
    public class DeskServiceTest 
    {
        private Mock<IRepository<Desk>> _repository;
        private Mock<IValidator<Desk>> _validatorMock;
        private DeskService _service;
        
        [SetUp]
        public void SetUp()
        {
            _repository = new Mock<IRepository<Desk>> ();
            _validatorMock = new Mock<IValidator<Desk>> ();
            
            _service = new DeskService (_repository.Object, _validatorMock.Object);
        }
        
        [Test]
        public async Task ShouldReturnOneModel()
        {
            var modelInput = new Desk () {Id = Guid.NewGuid ()};
            var modelOutput = new Desk ()
            {
                Id = modelInput.Id,
                Description = "test",
                DocumentId = Guid.NewGuid (),
                IsBlocked = false,
                Localization = new Localization ()
            };

            _repository.Setup (x => x.SelectOne (It.IsAny<Desk>()))
                .ReturnsAsync (modelOutput);

            var result = await _service.Get (modelInput);

            result.Should ().NotBeNull ();
            result.Should ().BeOfType<Desk> ();
        }

        [Test]
        public async Task ShouldReturnListModels()
        {
            var model = new Desk ()
            {
                Id = Guid.NewGuid (),
                Description = "test",
                DocumentId = Guid.NewGuid (),
                IsBlocked = false,
                Localization = new Localization ()
            };
            var modelOutput = new List<Desk> () {model};
            
            _repository.Setup (x => x.Select())
                .ReturnsAsync (modelOutput);
            
            var result = await _service.Get ();

            result.Should ().NotBeNull ();
            result.Should ().BeOfType<List<Desk>> ();
            result.Should ().HaveCount (1);
            result.Should ().HaveElementAt (0, model);
        }

        [Test]
        public async Task ShouldReturnNewGuidForAddMethod()
        {
            var model = new Desk ()
            {
                Id = Guid.NewGuid (),
                Description = "test",
                DocumentId = Guid.NewGuid (),
                IsBlocked = false,
                Localization = new Localization ()
            };
            
            _repository.Setup (x => x.Insert(It.IsAny<Desk>()));
            
            var result = await _service.Add (model);
            
            result.Should ().NotBeEmpty ();
        }

        [Test]
        public async Task ShouldRunUpdateModelInStore()
        {
            var model = new Desk ()
            {
                Id = Guid.NewGuid (),
                Description = "test",
                DocumentId = Guid.NewGuid (),
                IsBlocked = false,
                Localization = new Localization ()
            };
            
            _repository.Setup (x => x.Update(It.IsAny<Desk>()))
                .ReturnsAsync (true);
            
            var result = await _service.Update (model);

            result.Should ().BeTrue ();
        }

        [Test]
        public async Task ShouldRunDeleteFromStore()
        {
            var model = new Desk ()
            {
                Id = Guid.NewGuid ()
            };
            
            _repository.Setup (x => x.Delete(It.IsAny<Desk>()))
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
            var model = new Desk ()
            {
                Id = id,
                Description = "test",
                DocumentId = Guid.NewGuid (),
                IsBlocked = false,
                Localization = new Localization ()
            };
            var modelOutput = new List<Desk> () {model};
            
            _repository.Setup (
                    x => x.Select(It.IsAny<QueryDocument> ())
                )
                .ReturnsAsync (modelOutput);
            
            var result = await _service.Search (queryObject);

            result.Should ().NotBeNull ();
            result.Should ().BeOfType<List<Desk>> ();
            result.Should ().HaveCount (1);
            result.Should ().HaveElementAt (0, model);
        }
    }
}
