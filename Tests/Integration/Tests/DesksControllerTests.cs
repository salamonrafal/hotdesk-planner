using Xunit;
using Integration.ApplicationFactories;

namespace Integration.Tests
{
    abstract public class DeskIntegrationTest : Fixtures.DeskIntegrationFixture
    {
        public DeskIntegrationTest(DeskAppFactory fixture) : base(fixture) { }
    }
}
