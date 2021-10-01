using Api.Commands.Desk;
using Api.Mappers;
using Core.Models;
using FluentAssertions;
using NUnit.Framework;

namespace Unit.Api.Mappers
{
    [TestFixture]
    public class GenericDeskCommandMapperTest
    {
        [Test]
        public void ShouldReturnMappedModel()
        {
            var command = new InsertDeskCommand ()
            {
                Description = "test",
                IsBlocked = false,
                Localization = new Localization ()
                {
                    Coordination = new Coordination ()
                    {
                        X = 1,
                        Y = 2.1f
                    },
                    Floor = 11,
                    Outbuilding = "y"
                }
            };

            var mapper = new GenericDeskCommandMapper<InsertDeskCommand, Desk> ();
            var model = mapper.ConvertToModel (command);
            model.Should ().BeOfType<Desk> ();
            model.Description.Should ().Be ("test");
            model.IsBlocked.Should ().BeFalse ();
            model.Localization.Should ().BeOfType<Localization> ();
            model.Localization.Coordination.Should ().BeOfType<Coordination> ();
            model.Localization.Coordination.X.Should ().Be (1);
            model.Localization.Coordination.Y.Should ().Be (2.1f);
            model.Localization.Floor.Should ().Be (11);
            model.Localization.Outbuilding.Should ().Be ("y");
            model.Id.Should ().BeEmpty ();
        }
    }
}
