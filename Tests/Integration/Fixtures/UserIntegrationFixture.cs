using Xunit;
using Integration.ApplicationFactories;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http;

namespace Integration.Fixtures
{
    abstract public class UserIntegrationFixture : IClassFixture<UserAppFactory>
    {
        protected readonly UserAppFactory _factory;
        protected readonly HttpClient _client;

        public UserIntegrationFixture(UserAppFactory fixture)
        {
            _factory = fixture;
            _client = fixture.CreateClient();
        }
    }
}
