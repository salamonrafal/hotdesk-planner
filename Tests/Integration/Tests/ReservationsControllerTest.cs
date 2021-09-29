using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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
    public class ReservationsControllerTest: BaseFixture
    {
        private const string TestIdGet = "2618862c-5d36-45df-bd5f-fd3dfd95e6e5";
        private const string TestIdDelete = "b6371d6e-3ddb-4275-b537-c8d03357b47f";
        private const string TestIdUpdate = "ee88b74f-3463-4187-95c9-69fa0861b00d";
        private const string QuerySearch = "{\"assigned_to\": \"737fe3aa-999c-4c2e-a716-96dd1e7c1959\"}";
        private const int MaxElementsGetAll = 14;
        private readonly List<string> _testIdSearch = new List<string> ()
        {
            "ee88b74f-3463-4187-95c9-69fa0861b00d",
            "2618862c-5d36-45df-bd5f-fd3dfd95e6e5", 
            "b6371d6e-3ddb-4275-b537-c8d03357b47f",
        };
        
        private ReservationsController? _controller;
        
        [SetUp]
        public override async Task SetUp()
        {
            await base.SetUp ();
            _controller = new ReservationsController (Host?.Services.GetService<IMediator>());
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
            var command = MockCommands.ReservationModel.CreateInsertCommand ();

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
            var command = MockCommands.ReservationModel.CreateUpdateCommand (startDate: DateTime.Today);
            var actionResult = await _controller?.Update (Guid.Parse (TestIdUpdate), command)!;
            
            actionResult.Should ().BeOfType<NoContentResult> ();
        }

        [Test]
        public async Task ShouldSearchReservation()
        {
            var actionResult = await _controller?.Search(QuerySearch)!;

            actionResult.Should ().BeOfType<OkObjectResult> ();
            
            if ( actionResult is OkObjectResult okResult )
            {
                var response = okResult.Value as List<Reservation>;
                
                response.ShouldBeOn (_testIdSearch);
            }
        }
    }
}
