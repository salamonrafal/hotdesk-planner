using Api.Mappers;
using Core.Exceptions;
using Core.Models;
using Core.Services;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Api.Commands.Reservations.Handlers
{
    public class UpdateReservationHandler : IRequestHandler<UpdateReservationCommand, bool>
    {
        public readonly IReservationService _service;

        public UpdateReservationHandler(IReservationService service)
        {
            _service = service;
        }

        public async Task<bool> Handle(UpdateReservationCommand command, CancellationToken cancellationToken)
        {
            if (command == UpdateReservationCommand.Empty) throw new CommandEmptyException("Command is Empty");

            GenericReservationCommandMapper<UpdateReservationCommand, Reservation> mapper = new();
            Reservation desk = mapper.ConvertToModel(command);
            desk.Id = command.Id;
            return await _service.Update(desk);
        }
    }
}
