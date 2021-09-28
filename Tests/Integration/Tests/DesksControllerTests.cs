using System;
using System.IO;
using Xunit;
using Integration.ApplicationFactories;
using System.Threading.Tasks;
using System.Net;
using FluentAssertions;
using System.Net.Http;
using Api.Controllers;
using Integration.Fixtures;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Mongo2Go;
using MongoDB.Driver;
using NUnit.Framework;

namespace Integration.Tests
{
    public class DesksControllerTests: BaseFixture
    {
        private DesksController? _controller;
        
        [SetUp]
        public override async Task SetUp()
        {
            await base.SetUp ();
            _controller = new DesksController (_host.Services.GetService<IMediator>());
        }

        [Test]
        public async Task get_retrieve_all_desks()
        {
 
            var content = await _controller?.Get ()!;
        }
    }
}
