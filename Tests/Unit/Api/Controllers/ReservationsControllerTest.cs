using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Api.Commands.Reservations;
using Api.Controllers;
using Core.Models;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

namespace Unit.Api.Controllers
{
    [TestFixture]
    public class ReservationsControllerTest
    {
        private ReservationsController _controller;
        private Mock<IMediator> _mediatorMock;

        [SetUp]
        public void SetUp()
        {
            _mediatorMock = new Mock<IMediator> ();
            _controller = new ReservationsController(_mediatorMock.Object);
        }

        [Test]
        public async Task ShouldReturnModelGetMethod ()
        {
            var dataOutput = new Reservation();
            var id = Guid.NewGuid();

            _mediatorMock.Setup(
                x => x.Send(It.IsAny<GetReservationCommand>(), It.IsAny<CancellationToken>())
            ).ReturnsAsync(dataOutput);

            var result = await _controller.GetById(id);

            result.Should().BeOfType<OkObjectResult>();

            var response = result as OkObjectResult;
            var data = response?.Value;
            data.Should().BeOfType<Reservation>();
        }

        [Test]
        public async Task ShouldReturnAllModelsGetMethod()
        {
            var dataOutput = new List<Reservation> () {new Reservation ()};
            _mediatorMock.Setup(
                x => x.Send(It.IsAny<GetAllReservationCommand>(), It.IsAny<CancellationToken>())
            ).ReturnsAsync(dataOutput);

            var result = await _controller.Get();

            result.Should().BeOfType<OkObjectResult>();

            var response = result as OkObjectResult;
            var data = response?.Value;
            data.Should().BeOfType<List<Reservation>>();
        }

        [Test]
        public async Task ShouldReturnGuidModelInsertMethod()
        {
            var dataOutput = Guid.NewGuid();
            var command = new InsertReservationCommand();

            _mediatorMock.Setup(
                x => x.Send(It.IsAny<InsertReservationCommand>(), It.IsAny<CancellationToken>())
            ).ReturnsAsync(dataOutput);

            var result = await _controller.Insert(command);
            
            result.Should().BeOfType<OkObjectResult>();

            var response = result as OkObjectResult;
            var data = response?.Value;
            data.Should().BeOfType<Guid>();
        }

        [Test]
        public async Task ShouldNotReturnContentUpdateMethod()
        {
            const bool dataOutput = true;
            var command = new UpdateReservationCommand();
            var id = Guid.NewGuid();

            _mediatorMock.Setup(
                x => x.Send(It.IsAny<UpdateReservationCommand>(), It.IsAny<CancellationToken>())
            ).ReturnsAsync(dataOutput);

            var result = await _controller.Update(id, command);
            
            result.Should().BeOfType<NoContentResult>();
        }

        [Test]
        public async Task ShouldNotReturnContentDeleteMethod()
        {
            const bool dataOutput = true;
            var id = Guid.NewGuid();

            _mediatorMock.Setup(
                x => x.Send(It.IsAny<DeleteReservationCommand>(), It.IsAny<CancellationToken>())
            ).ReturnsAsync(dataOutput);

            var result = await _controller.Delete(id);
            
            result.Should().BeOfType<AcceptedResult>();
        }

        [Test]
        public async Task ShouldReturnListModelsSearchMethod()
        {
            var dataOutput = new List<Reservation>();
            const string searchQuery = "search";

            _mediatorMock.Setup(
                x => x.Send(It.IsAny<SearchReservationCommand>(), It.IsAny<CancellationToken>())
            ).ReturnsAsync(dataOutput);

            var result = await _controller.Search(searchQuery);
            
            var response = result as OkObjectResult;
            var data = response?.Value;
            data.Should().BeOfType<List<Reservation>>();
        }
    }
}
