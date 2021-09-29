using System;
using System.Collections.Generic;
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
        private const string DeskIdGet = "eb9ce8c0-eded-492c-9745-07203eeeaf74";
        private const string DeskIdDelete = "5a3a2fec-db76-48dc-b731-60ceaa211151";
        private const string DeskIdUpdate = "cd17861a-749a-47fd-8a3e-7054dd70416e";
        private const string QuerySearch = "{\"localization.floor\": 1, \"is_blocked\": true}";
        private const int MaxElementsGetAll = 6;
        private readonly string[] _deskIdSearch =
        {
            "4e89e05c-92dc-42c2-bbc5-683c5a0d71ef", 
            "c33bd5d4-7af7-45fb-84e2-15bbbf7a1789"
        };
        
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

            actionResult.Should ().BeOfType<OkObjectResult> ();
            
            if ( actionResult is OkObjectResult okResult )
            {
                var response = okResult.Value as List<Desk>;
                response.Should ().HaveCount (MaxElementsGetAll);
            }
        }

        [Test]
        public async Task ShouldInsertDeskToStore()
        {
            InsertDeskCommand command = MockCommands.DeskModel.CreateInsertDeskCommand ();

            var actionResult = await _controller?.Insert (command)!;
            
            actionResult.Should ().BeOfType<OkObjectResult> ();
            
            if ( actionResult is OkObjectResult okResult )
            {
                var response = okResult.Value is Guid guid ? guid : default;
                response.Should ().NotBeEmpty ();
            }
        }

        [Test]
        public async Task ShouldReturnDeskById()
        {
            GetDeskCommand command = MockCommands.DeskModel.CreateGetDeskCommand
                (id: Guid.Parse (DeskIdGet));

            var actionResult = await _controller?.GetById (command.Id)!;
            
            actionResult.Should ().BeOfType<OkObjectResult> ();
            
            if ( actionResult is OkObjectResult okResult )
            {
                var response = okResult.Value as Desk;

                response.Should ().NotBeNull ();
                response?.Description.Should ().Be ("Description");
                response?.IsBlocked.Should ().BeFalse ();
            }
        }

        [Test]
        public async Task ShouldDeleteDeskFromStore()
        {
            var actionResult = await _controller?.Delete (Guid.Parse (DeskIdDelete))!;
            
            actionResult.Should ().BeOfType<AcceptedResult> ();
        }
        
        [Test]
        public async Task ShouldUpdateDesk()
        {
            var command = MockCommands.DeskModel.CreateUpdateDeskCommand (description: "Description updated");
            var actionResult = await _controller?.Update (Guid.Parse (DeskIdUpdate), command)!;
            
            actionResult.Should ().BeOfType<NoContentResult> ();
        }

        [Test]
        public async Task ShouldSearchDesk()
        {
            var actionResult = await _controller?.Search(QuerySearch)!;
            
            actionResult.Should ().BeOfType<OkObjectResult> ();
            
            if ( actionResult is OkObjectResult okResult )
            {
                var response = okResult.Value as List<Desk>;
                response.Should ().HaveCount (2);
                response?[0].Id.Should ().Be (Guid.Parse (_deskIdSearch[1]));
                response?[1].Id.Should ().Be (Guid.Parse (_deskIdSearch[0]));
            }
        }
    }
}
