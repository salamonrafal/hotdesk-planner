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
    [TestFixture (TestOf = typeof(UsersController))]
    [Author ("Rafał Salamon", "rasa+code@salamonrafal.pl")]
    public class UsersControllerTest : BaseFixture
    {
        private const string TestIdGet = "8c78265c-5875-4cc2-bf3a-a05db0481d30";
        private const string TestIdDelete = "afb0568d-b0f8-495a-8386-ebf5ff1ef046";
        private const string TestIdUpdate = "abf10f5c-63bf-47b6-9be9-5035d05cf8b0";
        private const string QuerySearch = "{\"email\": \"test-search@mailinator.com\"}";
        private const int MaxElementsGetAll = 8;

        private readonly List<string> _testIdSearch = new ()
        {
            "215ccd66-a2f6-4f7c-ab1a-66914fb7dbe3",
            "fd11e1be-a1d1-4080-88a2-7103f5e5418a",
            "0a77a5ec-364d-4008-b813-5802915051e6",
        };

        private UsersController? _controller;

        [TestFixture (TestOf = typeof(UsersController))]
        [Author ("Rafał Salamon", "rasa+code@salamonrafal.pl")]
        [Category ("Success")]
        public class SuccessScenarios : UsersControllerTest
        {
            [SetUp]
            public override async Task SetUpForSuccess()
            {
                await base.SetUpForSuccess ();
                _controller = new UsersController (Host?.Services.GetService<IMediator> ());
            }

            [Test]
            public async Task ShouldReturnAllUsers()
            {
                var actionResult = await _controller?.Get ()!;
                actionResult.Should ().BeOfType<OkObjectResult> ();

                if ( actionResult is OkObjectResult okResult )
                {
                    var response = okResult.Value as List<User>;
                    response.Should ().HaveCount (MaxElementsGetAll);
                }
            }

            [Test]
            public async Task ShouldInsertUserToStore()
            {
                var command = MockCommands.UserModel.CreateInsertCommand ();

                var actionResult = await _controller?.Insert (command)!;

                actionResult.Should ().BeOfType<OkObjectResult> ();

                if ( actionResult is OkObjectResult okResult )
                {
                    var response = okResult.Value is Guid guid ? guid : default;
                    response.Should ().NotBeEmpty ();
                }
            }

            [Test]
            public async Task ShouldReturnUserById()
            {
                var actionResult = await _controller?.GetById (Guid.Parse (TestIdGet))!;

                actionResult.Should ().BeOfType<OkObjectResult> ();

                if ( actionResult is OkObjectResult okResult )
                {
                    var response = okResult.Value as User;

                    response.Should ().NotBeNull ();
                    response?.Name.Should ().Be ("Jan 1");
                    response?.Surname.Should ().Be ("Kowalski");
                    response?.Password.Should ().Be ("123456");
                    response?.Email.Should ().Be ("test@mailinator.com");
                    response?.UrlAvatar.Should ().Be ("http://wwww.google.pl/");
                }
            }

            [Test]
            public async Task ShouldDeleteUserFromStore()
            {
                var actionResult = await _controller?.Delete (Guid.Parse (TestIdDelete))!;

                actionResult.Should ().BeOfType<AcceptedResult> ();
            }

            [Test]
            public async Task ShouldUpdateUser()
            {
                var command = MockCommands.UserModel.CreateUpdateCommand ();
                var actionResult = await _controller?.Update (Guid.Parse (TestIdUpdate), command)!;

                actionResult.Should ().BeOfType<NoContentResult> ();
            }

            [Test]
            public async Task ShouldSearchUser()
            {
                var actionResult = await _controller?.Search (QuerySearch)!;

                actionResult.Should ().BeOfType<OkObjectResult> ();

                if ( actionResult is OkObjectResult okResult )
                {
                    var response = okResult.Value as List<User>;

                    response.ShouldBeOn (_testIdSearch);
                }
            }
        }
    }
}
