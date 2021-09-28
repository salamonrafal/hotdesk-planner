using System.Threading.Tasks;
using Api.Controllers;
using Integration.Fixtures;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

using NUnit.Framework;

namespace Integration.Tests
{
    public class ReservationsControllerTest: BaseFixture
    {
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
            var response = await _controller?.Get ()!;
        }
    }
}
