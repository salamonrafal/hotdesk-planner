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
    [TestFixture (TestOf = typeof(DesksController))]
    [Author ("Rafał Salamon", "rasa+code@salamonrafal.pl")]
    public class DesksControllerTest: BaseFixture
    {
        private const string TestIdGet = "eb9ce8c0-eded-492c-9745-07203eeeaf74";
        private const string TestIdDelete = "5a3a2fec-db76-48dc-b731-60ceaa211151";
        private const string TestIdUpdate = "cd17861a-749a-47fd-8a3e-7054dd70416e";
        private const string QuerySearch = "{\"localization.floor\": 1, \"is_blocked\": true}";
        private const int MaxElementsGetAll = 6;
        private readonly List<string> _testIdSearch = new ()
        {
            "4e89e05c-92dc-42c2-bbc5-683c5a0d71ef", 
            "c33bd5d4-7af7-45fb-84e2-15bbbf7a1789"
        };
        
        private DesksController? _controller;
        

        [TestFixture(TestOf = typeof(DesksController))]
        [Author("Rafał Salamon", "rasa+code@salamonrafal.pl")]
        [Category("Success")]
        public class SuccessScenarios : DesksControllerTest
        {
            [SetUp]
            public override async Task SetUpForSuccess()
            {
                await base.SetUpForSuccess ();
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
                InsertDeskCommand command = MockCommands.DeskModel.CreateInsertCommand ();

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
                var actionResult = await _controller?.GetById (Guid.Parse (TestIdGet))!;

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
                var actionResult = await _controller?.Delete (Guid.Parse (TestIdDelete))!;

                actionResult.Should ().BeOfType<AcceptedResult> ();
            }

            [Test]
            public async Task ShouldUpdateDesk()
            {
                var command = MockCommands.DeskModel.CreateUpdateCommand (description: "Description updated");
                var actionResult = await _controller?.Update (Guid.Parse (TestIdUpdate), command)!;

                actionResult.Should ().BeOfType<NoContentResult> ();
            }

            [Test]
            public async Task ShouldSearchDesk()
            {
                var actionResult = await _controller?.Search (QuerySearch)!;

                actionResult.Should ().BeOfType<OkObjectResult> ();

                if ( actionResult is OkObjectResult okResult )
                {
                    var response = okResult.Value as List<Desk>;

                    response.ShouldBeOn (_testIdSearch);
                }
            }
        }
        
        
        [TestFixture(TestOf = typeof(DesksController))]
        [Author("Rafał Salamon", "rasa+code@salamonrafal.pl")]
        [Category("Alternative")]
        public class AlternativeScenarios: DesksControllerTest
        {
            [SetUp]
            public override async Task SetUpForAlternative()
            {
                await base.SetUpForAlternative ();
                _controller = new DesksController (Host?.Services.GetService<IMediator>());
            }
            
            [Test]
            public async Task ShouldReturnEmptyArrayForGetAllDesks()
            {
                var actionResult = await _controller?.Get ()!;

                actionResult.Should ().BeOfType<OkObjectResult> ();

                if ( actionResult is OkObjectResult okResult )
                {
                    var response = okResult.Value as List<Desk>;
                    response.Should ().HaveCount (0);
                }
            }
            
            [Test]
            public async Task ShouldReturnProblemStateForGetAllDesks()
            {
                IsThrowException = true;
                
                var actionResult = await _controller?.Get ()!;

                actionResult.Should ().BeOfType<ObjectResult> ();

                if ( actionResult is ObjectResult objectResult )
                {
                    objectResult.StatusCode.Should ().Be (500);
                }
            }
            
            [Test]
            public async Task ShouldReturnEmptyDeskById()
            {
                var actionResult = await _controller?.GetById (Guid.Parse (TestIdGet))!;

                actionResult.Should ().BeOfType<OkObjectResult> ();

                if ( actionResult is OkObjectResult okResult )
                {
                    var response = okResult.Value as Desk;

                    response.Should ().NotBeNull ();
                    response?.Description.Should ().BeNull ();
                    response?.Localization.Should ().BeNull ();
                    response?.IsBlocked.Should ().BeNull ();
                    response?.Id.Should ().Be (Guid.Empty);
                    response?.DocumentId.Should ().Be (Guid.Empty);
                }
            }
            
            [Test]
            public async Task ShouldThrowExceptionUnhandledForDeskById()
            {
                IsThrowException = true;
                
                var actionResult = await _controller?.GetById (Guid.Parse (TestIdGet))!;

                actionResult.Should ().BeOfType<ObjectResult> ();

                if ( actionResult is ObjectResult objectResult )
                {
                    objectResult.StatusCode.Should ().Be (500);
                }
            }
            
            [Test]
            public async Task ShouldReturnBadRequestForEmptyRequestForInsertDeskToStore()
            {
                InsertDeskCommand command = new InsertDeskCommand ();

                var actionResult = await _controller?.Insert (command)!;

                actionResult.Should ().BeOfType<BadRequestObjectResult> ();
            }
            
            [Test]
            public async Task ShouldReturnBadRequestForEmptyRequestForUpdateDeskToStore()
            {
                UpdateDeskCommand command = new UpdateDeskCommand () { Description = ""};

                var actionResult = await _controller?.Update (Guid.Parse (TestIdUpdate), command)!;

                actionResult.Should ().BeOfType<BadRequestObjectResult> ();
            }
            
            [Test]
            public async Task ShouldReturnBadRequestForEmptyRequestForDeleteDeskFromStore()
            {
                var actionResult = await _controller?.Delete (Guid.Empty)!;

                actionResult.Should ().BeOfType<BadRequestObjectResult> ();
            }
            
            [Test]
            public async Task ShouldThrowExceptionUnhandledForSearchDesk()
            {
                IsThrowException = true;
                
                var actionResult = await _controller?.Search ("")!;

                actionResult.Should ().BeOfType<ObjectResult> ();

                if ( actionResult is ObjectResult objectResult )
                {
                    objectResult.StatusCode.Should ().Be (500);
                }
            }
            
            [Test]
            public async Task ShouldThrowExceptionUnhandledForInsertDesk()
            {
                IsThrowException = true;
                InsertDeskCommand command = MockCommands.DeskModel.CreateInsertCommand ();
                
                var actionResult = await _controller?.Insert(command)!;

                actionResult.Should ().BeOfType<ObjectResult> ();

                if ( actionResult is ObjectResult objectResult )
                {
                    objectResult.StatusCode.Should ().Be (500);
                }
            }
            
            [Test]
            public async Task ShouldThrowExceptionUnhandledForDeleteDesk()
            {
                IsThrowException = true;
          
                var actionResult = await _controller?.Delete(Guid.Parse (TestIdDelete))!;

                actionResult.Should ().BeOfType<ObjectResult> ();

                if ( actionResult is ObjectResult objectResult )
                {
                    objectResult.StatusCode.Should ().Be (500);
                }
            }
            
            [Test]
            public async Task ShouldThrowExceptionUnhandledForUpdateDesk()
            {
                IsThrowException = true;
                
                var command = MockCommands.DeskModel.CreateUpdateCommand (description: "Description updated");
                var actionResult = await _controller?.Update(Guid.Parse (TestIdUpdate), command)!;

                actionResult.Should ().BeOfType<ObjectResult> ();

                if ( actionResult is ObjectResult objectResult )
                {
                    objectResult.StatusCode.Should ().Be (500);
                }
            }
        }
    }
}
