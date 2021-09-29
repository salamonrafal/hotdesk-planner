using Api.Mappers;
using Core.Exceptions;
using Core.Models;
using Core.Services;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Api.Commands.Reservations.Handlers
{
    public class DeleteReservationHandler : IRequestHandler<DeleteReservationCommand, bool>
    {
        private readonly IReservationService _service;

        public DeleteReservationHandler(IReservationService service)
        {
            _service = service;
        }

        public async Task<bool> Handle(DeleteReservationCommand command, CancellationToken cancellationToken)
        {
            if (command == DeleteReservationCommand.Empty) throw new CommandEmptyException("Command is Empty");

            GenericReservationCommandMapper<DeleteReservationCommand, Reservation> mapper = new();
            Reservation model = mapper.ConvertToModel(command);
            model.Id = command.Id;
            return await _service.Remove(model);
        }
    }
}
