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
    public class ReservationServiceTest
    {
        private Mock<IRepository<Reservation>> _repository;
        private Mock<IValidator<Reservation>> _validatorMock;
        private ReservationService _service;
        
        [SetUp]
        public void SetUp()
        {
            _repository = new Mock<IRepository<Reservation>> ();
            _validatorMock = new Mock<IValidator<Reservation>> ();
            
            _service = new ReservationService (_repository.Object, _validatorMock.Object);
        }
        
        [Test]
        public async Task ShouldReturnOneModel()
        {
            var modelInput = new Reservation () {Id = Guid.NewGuid ()};
            var modelOutput = new Reservation ()
            {
                Id = modelInput.Id,
                AssignedTo = Guid.NewGuid (),
                DeskId = Guid.NewGuid (),
                IsPeriodical = false,
                EndDate = DateTime.Now,
                StartDate = DateTime.Now,
                PeriodicDetail = new PeriodicDetail ()
            };

            _repository.Setup (x => x.SelectOne (It.IsAny<Reservation>()))
                .ReturnsAsync (modelOutput);

            var result = await _service.Get (modelInput);

            result.Should ().NotBeNull ();
            result.Should ().BeOfType<Reservation> ();
        }

        [Test]
        public async Task ShouldReturnListModels()
        {
            var model = new Reservation ()
            {
                Id = Guid.NewGuid (),
                AssignedTo = Guid.NewGuid (),
                DeskId = Guid.NewGuid (),
                IsPeriodical = false,
                EndDate = DateTime.Now,
                StartDate = DateTime.Now,
                PeriodicDetail = new PeriodicDetail (),
                DocumentId = Guid.NewGuid ()
            };
            var modelOutput = new List<Reservation> () {model};
            
            _repository.Setup (x => x.Select())
                .ReturnsAsync (modelOutput);
            
            var result = await _service.Get ();

            result.Should ().NotBeNull ();
            result.Should ().BeOfType<List<Reservation>> ();
            result.Should ().HaveCount (1);
            result.Should ().HaveElementAt (0, model);
        }

        [Test]
        public async Task ShouldReturnNewGuidForAddMethod()
        {
            var model = new Reservation ()
            {
                Id = Guid.NewGuid (),
                AssignedTo = Guid.NewGuid (),
                DeskId = Guid.NewGuid (),
                IsPeriodical = false,
                EndDate = DateTime.Now,
                StartDate = DateTime.Now,
                PeriodicDetail = new PeriodicDetail (),
                DocumentId = Guid.NewGuid (),
            };
            
            _repository.Setup (x => x.Insert(It.IsAny<Reservation>()));
            
            var result = await _service.Add (model);
            
            result.Should ().NotBeEmpty ();
        }

        [Test]
        public async Task ShouldRunUpdateModelInStore()
        {
            var model = new Reservation ()
            {
                Id = Guid.NewGuid (),
                AssignedTo = Guid.NewGuid (),
                DeskId = Guid.NewGuid (),
                IsPeriodical = false,
                EndDate = DateTime.Now,
                StartDate = DateTime.Now,
                PeriodicDetail = new PeriodicDetail (),
                DocumentId = Guid.NewGuid (),
            };
            
            _repository.Setup (x => x.Update(It.IsAny<Reservation>()))
                .ReturnsAsync (true);
            
            var result = await _service.Update (model);

            result.Should ().BeTrue ();
        }

        [Test]
        public async Task ShouldRunDeleteFromStore()
        {
            var model = new Reservation ()
            {
                Id = Guid.NewGuid ()
            };
            
            _repository.Setup (x => x.Delete(It.IsAny<Reservation>()))
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
            var model = new Reservation ()
            {
                Id = id,
                AssignedTo = Guid.NewGuid (),
                DeskId = Guid.NewGuid (),
                IsPeriodical = false,
                EndDate = DateTime.Now,
                StartDate = DateTime.Now,
                PeriodicDetail = new PeriodicDetail (),
                DocumentId = Guid.NewGuid (),
            };
            var modelOutput = new List<Reservation> () {model};
            
            _repository.Setup (
                    x => x.Select(It.IsAny<QueryDocument> ())
                )
                .ReturnsAsync (modelOutput);
            
            var result = await _service.Search (queryObject);

            result.Should ().NotBeNull ();
            result.Should ().BeOfType<List<Reservation>> ();
            result.Should ().HaveCount (1);
            result.Should ().HaveElementAt (0, model);
        }
    }
}
