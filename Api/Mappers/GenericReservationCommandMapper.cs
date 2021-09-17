using Api.Commands.Reservations;
using Core.Models;

namespace Api.Mappers
{
    public class GenericReservationCommandMapper<TClassInput, TClassOutput> : ICommandMapper<TClassInput, TClassOutput>
        where TClassInput : CommonReservationCommand where TClassOutput : Reservation
    {
        public TClassOutput ConvertToModel(TClassInput command) =>  (TClassOutput) command;
    }
}
