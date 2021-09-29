using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Api.Commands.Desk;
using Api.Controllers;
using Core.Models;
using FluentAssertions;
using Integration.Fixtures;
using Integration.Helpers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace Integration.Tests
{
    [TestFixture]
    public class DesksControllerTest: BaseFixture
    {
        private DesksController? _controller;
        
        [SetUp]
        public override async Task SetUp()
        {
            await base.SetUp ();
            _controller = new DesksController (Host?.Services.GetService<IMediator>());
        }

        [Test]
        public async Task ShouldReturnAllDesks()
        {
            var actionResult = await _controller?.Get ()!;
            
            if ( actionResult is OkObjectResult okResult )
            {
                var response = okResult.Value as List<Desk>;
                response.Should ().HaveCount (2);
            }
        }

        [Test]
        public async Task ShouldInsertDeskToStore()
        {
            InsertDeskCommand command = MockCommands.DeskModel.CreateInsertDeskCommand ();

            var actionResult = await _controller?.Insert (command)!;

            if ( actionResult is OkObjectResult okResult )
            {
                var response = okResult.Value is Guid guid ? guid : default;
            }
        }

        [Test]
        public async Task ShouldReturnDeskById()
        {
            GetDeskCommand command = MockCommands.DeskModel.CreateGetDeskCommand
                (id: Guid.Parse ("eb9ce8c0-eded-492c-9745-07203eeeaf74"));

            var actionResult = await _controller?.GetById (command.Id)!;
            
            if ( actionResult is OkObjectResult okResult )
            {
                var response = okResult.Value as Desk;

                response.Should ().NotBeNull ();
                response?.Description.Should ().Be ("Description");
                response?.IsBlocked.Should ().BeFalse ();
            }
        }

        [Test]
        public async Task ShouldDeleteDesk()
        {
            
        }

        [Test]
        public async Task ShouldSearchDesk()
        {
            
        }
    }
}
