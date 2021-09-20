using System;
using Xunit;
using Integration.ApplicationFactories;
using System.Threading.Tasks;
using System.Net;
using FluentAssertions;
using Integration.Fixtures;
using System.Net.Http;

namespace Integration.Tests
{
    public class DesksControllerTests : IntegrationFixture
    {
        public DesksControllerTests(AppFactory fixture) : base(fixture) { }

        [Fact]
        public async Task get_retrieve_all_desks()
        { 
            var response = await _client.GetAsync("/api/v1/desks");
            var content = await response.Content.ReadAsStringAsync();
            
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task post_retrieve_all_desks()
        {
            var response = await _client.PostAsync("/api/v1/desks", new StringContent(""));
            
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
