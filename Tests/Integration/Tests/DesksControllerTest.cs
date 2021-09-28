using System.IO;
using System.Threading.Tasks;
using Api.Commands.Desk;
using Api.Controllers;
using Core.Models;
using Integration.Fixtures;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace Integration.Tests
{
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
 
            var response = await _controller?.Get ()!;
        }

        [Test]
        public async Task ShouldInsertDeskToStore()
        {
            InsertDeskCommand command = new InsertDeskCommand ()
            {
                Description = "Desk 1",
                IsBlocked = false,
                Localization = new Localization ()
                {
                    Coordination = new Coordination () { X = 2, Y = 2},
                    Floor = 2,
                    Outbuilding = "Y"
                }
            };
            
            var response = await _controller?.Insert (command)!;
        }
    }
}
