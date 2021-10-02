using System;
using Api.Commands.Reservations;
using Api.Mappers;
using Core.Models;
using FluentAssertions;
using NUnit.Framework;

namespace Unit.Api.Mappers
{
    [TestFixture]
    public class GenericReservationCommandMapperTest
    {
        [Test]
        public void ShouldReturnMappedModel()
        {
            var id = Guid.NewGuid ();
            var date = DateTime.Now;
            
            var command = new InsertReservationCommand ()
            {
                AssignedTo = id,
                DeskId = id,
                IsPeriodical = false,
                PeriodicDetail = new PeriodicDetail (),
                StartDate = date,
                EndDate = date
            };

            var mapper = new GenericReservationCommandMapper <InsertReservationCommand, Reservation> ();
            var model = mapper.ConvertToModel (command);

            model.Should ().BeOfType<Reservation> ();
            model.AssignedTo.Should ().Be (id);
            model.DeskId.Should ().Be (id);
            model.IsPeriodical.Should ().BeFalse ();
            model.PeriodicDetail.Should ().BeOfType<PeriodicDetail> ();
            model.StartDate.Should ().Be (date);
            model.EndDate.Should ().Be (date);
        }
    }
}
