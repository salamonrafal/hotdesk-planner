using Xunit;
using Integration.ApplicationFactories;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http;

namespace Integration.Fixtures
{
    abstract public class ReservationIntegrationFixture : IClassFixture<ReservationAppFactory>
    {
        protected readonly ReservationAppFactory _factory;
        protected readonly HttpClient _client;

        public ReservationIntegrationFixture(ReservationAppFactory fixture)
        {
            _factory = fixture;
            _client = fixture.CreateClient();
        }
    }
}
