using Api.Commands.Desk;
using Api.Controllers;
using Core.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;

namespace Unit.Api.Controllers
{
    [TestFixture]
    public class DesksControllerTest
    {
        private DesksController _controller;
        private Mock<IMediator> _mediatorMock;

        [SetUp]
        public void SetUp()
        {
            _mediatorMock = new Mock<IMediator> ();
            _controller = new DesksController (_mediatorMock.Object);
        }

        [Test]
        public async Task ShouldReturnModelGetMethod ()
        {
            var dataOutput = new Desk();
            var deskId = Guid.NewGuid();

            _mediatorMock.Setup(
                x => x.Send(It.IsAny<GetDeskCommand>(), It.IsAny<CancellationToken>())
            ).ReturnsAsync(dataOutput);

            var result = await _controller.GetById(deskId);

            result.Should().BeOfType<OkObjectResult>();

            var response = result as OkObjectResult;
            var data = response?.Value;
            data.Should().BeOfType<Desk>();
        }

        [Test]
        public async Task ShouldReturnAllModelsGetMethod()
        {
            var dataOutput = new List<Desk>() { new Desk() { Description = "aaa", Id = Guid.NewGuid(), IsBlocked = false, Localization = new Localization() { Floor = 1, Outbuilding = "Y", Coordination = new Coordination() { X = 1, Y = 2 } } } };

            _mediatorMock.Setup(
                x => x.Send(It.IsAny<GetAllDeskCommand>(), It.IsAny<CancellationToken>())
            ).ReturnsAsync(dataOutput);

            var result = await _controller.Get();

            result.Should().BeOfType<OkObjectResult>();

            var response = result as OkObjectResult;
            var data = response?.Value;
            data.Should().BeOfType<List<Desk>>();
        }

        [Test]
        public async Task ShouldReturnGuidModelInsertMethod()
        {
            var dataOutput = Guid.NewGuid();
            var command = new InsertDeskCommand();

            _mediatorMock.Setup(
                x => x.Send(It.IsAny<InsertDeskCommand>(), It.IsAny<CancellationToken>())
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
            var command = new UpdateDeskCommand();
            var deskId = Guid.NewGuid();

            _mediatorMock.Setup(
                x => x.Send(It.IsAny<UpdateDeskCommand>(), It.IsAny<CancellationToken>())
            ).ReturnsAsync(dataOutput);

            var result = await _controller.Update(deskId, command);
            
            result.Should().BeOfType<NoContentResult>();
        }

        [Test]
        public async Task ShouldNotReturnContentDeleteMethod()
        {
            const bool dataOutput = true;
            var deskId = Guid.NewGuid();

            _mediatorMock.Setup(
                x => x.Send(It.IsAny<DeleteDeskCommand>(), It.IsAny<CancellationToken>())
            ).ReturnsAsync(dataOutput);

            var result = await _controller.Delete(deskId);
            
            result.Should().BeOfType<AcceptedResult>();
        }

        [Test]
        public async Task ShouldReturnListModelsSearchMethod()
        {
            var dataOutput = new List<Desk>();
            const string searchQuery = "search";

            _mediatorMock.Setup(
                x => x.Send(It.IsAny<SearchDeskCommand>(), It.IsAny<CancellationToken>())
            ).ReturnsAsync(dataOutput);

            var result = await _controller.Search(searchQuery);
            
            var response = result as OkObjectResult;
            var data = response?.Value;
            data.Should().BeOfType<List<Desk>>();
        }
    }
}
