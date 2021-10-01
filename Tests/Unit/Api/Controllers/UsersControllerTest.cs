using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Api.Commands.Reservations;
using Api.Commands.Users;
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
    public class UsersControllerTest
    {
        private UsersController _controller;
        private Mock<IMediator> _mediatorMock;

        [SetUp]
        public void SetUp()
        {
            _mediatorMock = new Mock<IMediator> ();
            _controller = new UsersController(_mediatorMock.Object);
        }

        [Test]
        public async Task ShouldReturnModelGetMethod ()
        {
            var dataOutput = new User();
            var id = Guid.NewGuid();

            _mediatorMock.Setup(
                x => x.Send(It.IsAny<GetUserCommand>(), It.IsAny<CancellationToken>())
            ).ReturnsAsync(dataOutput);

            var result = await _controller.GetById(id);

            result.Should().BeOfType<OkObjectResult>();

            var response = result as OkObjectResult;
            var data = response?.Value;
            data.Should().BeOfType<User>();
        }

        [Test]
        public async Task ShouldReturnAllModelsGetMethod()
        {
            var dataOutput = new List<User> () {new User (){ }};
            _mediatorMock.Setup(
                x => x.Send(It.IsAny<GetAllUserCommand>(), It.IsAny<CancellationToken>())
            ).ReturnsAsync(dataOutput);

            var result = await _controller.Get();

            result.Should().BeOfType<OkObjectResult>();

            var response = result as OkObjectResult;
            var data = response?.Value;
            data.Should().BeOfType<List<User>>();
        }

        [Test]
        public async Task ShouldReturnGuidModelInsertMethod()
        {
            var dataOutput = Guid.NewGuid();
            var command = new InsertUserCommand();

            _mediatorMock.Setup(
                x => x.Send(It.IsAny<InsertUserCommand>(), It.IsAny<CancellationToken>())
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
            var command = new UpdateUserCommand();
            var id = Guid.NewGuid();

            _mediatorMock.Setup(
                x => x.Send(It.IsAny<UpdateUserCommand>(), It.IsAny<CancellationToken>())
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
                x => x.Send(It.IsAny<DeleteUserCommand>(), It.IsAny<CancellationToken>())
            ).ReturnsAsync(dataOutput);

            var result = await _controller.Delete(id);
            
            result.Should().BeOfType<AcceptedResult>();
        }

        [Test]
        public async Task ShouldReturnListModelsSearchMethod()
        {
            var dataOutput = new List<User>();
            const string searchQuery = "search";

            _mediatorMock.Setup(
                x => x.Send(It.IsAny<SearchUserCommand>(), It.IsAny<CancellationToken>())
            ).ReturnsAsync(dataOutput);

            var result = await _controller.Search(searchQuery);
            
            var response = result as OkObjectResult;
            var data = response?.Value;
            data.Should().BeOfType<List<User>>();
        }
    }
}
