using System.Threading.Tasks;
using Api.Controllers;
using Integration.Fixtures;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

using NUnit.Framework;

namespace Integration.Tests
{
    public class UsersControllerTest: BaseFixture
    {
        private UsersController? _controller;
        
        [SetUp]
        public override async Task SetUp()
        {
            await base.SetUp ();
            _controller = new UsersController(Host?.Services.GetService<IMediator>());
        }

        [Test]
        public async Task ShouldReturnAllUsers()
        {
            var response = await _controller?.Get ()!;
        }
    }
}
