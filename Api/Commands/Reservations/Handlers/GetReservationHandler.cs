using Api.Mappers;
using Core.Exceptions;
using Core.Models;
using Core.Services;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Api.Commands.Reservations.Handlers
{
    public class GetReservationHandler : IRequestHandler<GetReservationCommand, Reservation>
    {
        private readonly IReservationService _service;

        public GetReservationHandler(IReservationService service)
        {
            _service = service;
        }

        public async Task<Reservation> Handle(GetReservationCommand command, CancellationToken cancellationToken)
        {
            if (command == GetReservationCommand.Empty) throw new CommandEmptyException("Command is Empty");

            GenericReservationCommandMapper<GetReservationCommand, Reservation> mapper = new();
            var model = mapper.ConvertToModel(command);
            model.Id = command.Id;
            
            return await _service.Get(model);
        }
    }
}
