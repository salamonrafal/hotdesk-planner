using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Commands.Reservations;
using Api.Controllers;
using Core.Models;
using FluentAssertions;
using Integration.Fixtures;
using Integration.Helpers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using static Integration.Helpers.MockCommands.ReservationModel;

namespace Integration.Tests
{
    [TestFixture (TestOf = typeof(ReservationsController))]
    [Author ("Rafał Salamon", "rasa+code@salamonrafal.pl")]
    public class ReservationsControllerTest : BaseFixture
    {
        private const string TestIdGet = "2618862c-5d36-45df-bd5f-fd3dfd95e6e5";
        private const string TestIdDelete = "b6371d6e-3ddb-4275-b537-c8d03357b47f";
        private const string TestIdUpdate = "ee88b74f-3463-4187-95c9-69fa0861b00d";
        private const string QuerySearch = "{\"assigned_to\": \"737fe3aa-999c-4c2e-a716-96dd1e7c1959\"}";
        private const int MaxElementsGetAll = 14;

        private readonly List<string> _testIdSearch = new ()
        {
            "ee88b74f-3463-4187-95c9-69fa0861b00d",
            "2618862c-5d36-45df-bd5f-fd3dfd95e6e5",
            "b6371d6e-3ddb-4275-b537-c8d03357b47f",
        };

        private ReservationsController? _controller;

        [TestFixture(TestOf = typeof(ReservationsController))]
        [Author("Rafał Salamon", "rasa+code@salamonrafal.pl")]
        [Category("Success")]
        public class SuccessScenarios : ReservationsControllerTest
        {
            [SetUp]
            public override async Task SetUpForSuccess()
            {
                await base.SetUpForSuccess ();
                _controller = new ReservationsController (Host?.Services.GetService<IMediator> ());
            }

            [Test]
            public async Task ShouldReturnAllReservations()
            {
                var actionResult = await _controller?.Get ()!;
                actionResult.Should ().BeOfType<OkObjectResult> ();

                if ( actionResult is OkObjectResult okResult )
                {
                    var response = okResult.Value as List<Reservation>;
                    response.Should ().HaveCount (MaxElementsGetAll);
                }
            }

            [Test]
            public async Task ShouldInsertReservationToStore()
            {
                var command = CreateInsertCommand ();

                var actionResult = await _controller?.Insert (command)!;

                actionResult.Should ().BeOfType<OkObjectResult> ();

                if ( actionResult is OkObjectResult okResult )
                {
                    var response = okResult.Value is Guid guid ? guid : default;
                    response.Should ().NotBeEmpty ();
                }
            }

            [Test]
            public async Task ShouldReturnReservationById()
            {
                var actionResult = await _controller?.GetById (Guid.Parse (TestIdGet))!;

                actionResult.Should ().BeOfType<OkObjectResult> ();

                if ( actionResult is OkObjectResult okResult )
                {
                    var response = okResult.Value as Reservation;

                    response.Should ().NotBeNull ();
                    response?.AssignedTo.Should ().Be ("737fe3aa-999c-4c2e-a716-96dd1e7c1959");
                }
            }

            [Test]
            public async Task ShouldDeleteReservationFromStore()
            {
                var actionResult = await _controller?.Delete (Guid.Parse (TestIdDelete))!;

                actionResult.Should ().BeOfType<AcceptedResult> ();
            }

            [Test]
            public async Task ShouldUpdateReservation()
            {
                var command = CreateUpdateCommand (startDate: DateTime.Today);
                var actionResult = await _controller?.Update (Guid.Parse (TestIdUpdate), command)!;

                actionResult.Should ().BeOfType<NoContentResult> ();
            }

            [Test]
            public async Task ShouldSearchReservation()
            {
                var actionResult = await _controller?.Search (QuerySearch)!;

                actionResult.Should ().BeOfType<OkObjectResult> ();

                if ( actionResult is OkObjectResult okResult )
                {
                    var response = okResult.Value as List<Reservation>;

                    response.ShouldBeOn (_testIdSearch);
                }
            }
        }
        
        [TestFixture(TestOf = typeof(DesksController))]
        [Author("Rafał Salamon", "rasa+code@salamonrafal.pl")]
        [Category("Alternative")]
        public class AlternativeScenarios: ReservationsControllerTest
        {
            [SetUp] 
            public override async Task SetUpForAlternative()
            {
                await base.SetUpForAlternative ();
                _controller = new ReservationsController (Host?.Services.GetService<IMediator>());
            }
            
            [Test]
            public async Task ShouldReturnEmptyArrayForGetAllReservations()
            {
                var actionResult = await _controller?.Get ()!;

                actionResult.Should ().BeOfType<OkObjectResult> ();

                if ( actionResult is OkObjectResult okResult )
                {
                    var response = okResult.Value as List<Reservation>;
                    response.Should ().HaveCount (0);
                }
            }
            
            [Test]
            public async Task ShouldReturnProblemStateForGetAllReservations()
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
            public async Task ShouldReturnEmptyReservationById()
            {
                var actionResult = await _controller?.GetById (Guid.Parse (TestIdGet))!;

                actionResult.Should ().BeOfType<OkObjectResult> ();

                if ( actionResult is OkObjectResult okResult )
                {
                    var response = okResult.Value as Reservation;
                    response?.AssignedTo.Should ().BeNull ();
                    response?.DeskId.Should ().BeNull ();
                    response?.StartDate.Should ().BeNull ();
                    response?.EndDate.Should ().BeNull ();
                    response?.PeriodicDetail.Should ().BeNull ();
                    response?.IsPeriodical.Should ().BeNull ();
                    response.Should ().NotBeNull ();
                    response?.Id.Should ().Be (Guid.Empty);
                    response?.DocumentId.Should ().Be (Guid.Empty);
                }
            }
            
            [Test]
            public async Task ShouldThrowExceptionUnhandledForReservationById()
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
            public async Task ShouldReturnBadRequestForEmptyRequestForInsertReservationToStore()
            {
                InsertReservationCommand command = new InsertReservationCommand ();

                var actionResult = await _controller?.Insert (command)!;

                actionResult.Should ().BeOfType<BadRequestObjectResult> ();
            }
            
            [Test]
            public async Task ShouldReturnBadRequestForEmptyRequestForUpdateReservationToStore()
            {
                var date = DateTime.Now;
                
                UpdateReservationCommand command = new UpdateReservationCommand ()
                {
                    StartDate = date, 
                    EndDate = date
                };

                var actionResult = await _controller?.Update (Guid.Parse (TestIdUpdate), command)!;

                actionResult.Should ().BeOfType<BadRequestObjectResult> ();
            }
            
            [Test]
            public async Task ShouldReturnBadRequestForEmptyRequestForDeleteReservationFromStore()
            {
                var actionResult = await _controller?.Delete (Guid.Empty)!;

                actionResult.Should ().BeOfType<BadRequestObjectResult> ();
            }
            
            [Test]
            public async Task ShouldThrowExceptionUnhandledForSearchReservation()
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
            public async Task ShouldThrowExceptionUnhandledForInsertReservation()
            {
                IsThrowException = true;
                InsertReservationCommand command = CreateInsertCommand ();
                
                var actionResult = await _controller?.Insert(command)!;

                actionResult.Should ().BeOfType<ObjectResult> ();

                if ( actionResult is ObjectResult objectResult )
                {
                    objectResult.StatusCode.Should ().Be (500);
                }
            }
            
            [Test]
            public async Task ShouldThrowExceptionUnhandledForDeleteReservation()
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
            public async Task ShouldThrowExceptionUnhandledForUpdateReservation()
            {
                IsThrowException = true;
                
                var command = CreateUpdateCommand ();
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
