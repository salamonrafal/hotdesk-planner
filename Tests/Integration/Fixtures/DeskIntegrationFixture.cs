using Xunit;
using Integration.ApplicationFactories;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http;

namespace Integration.Fixtures
{
    abstract public class DeskIntegrationFixture : IClassFixture<DeskAppFactory>
    {
        protected readonly DeskAppFactory _factory;
        protected readonly HttpClient _client;

        public DeskIntegrationFixture(DeskAppFactory fixture)
        {
            _factory = fixture;
            _client = fixture.CreateClient();
        }
    }
}
