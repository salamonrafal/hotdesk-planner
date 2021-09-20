using Xunit;
using Integration.ApplicationFactories;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http;

namespace Integration.Fixtures
{
    public abstract class IntegrationFixture : IClassFixture<AppFactory>
    {
        protected readonly AppFactory _factory;
        protected readonly HttpClient _client;

        public IntegrationFixture(AppFactory fixture)
        {
            _factory = fixture;
            _client = _factory.CreateClient();
        }
    }
}
