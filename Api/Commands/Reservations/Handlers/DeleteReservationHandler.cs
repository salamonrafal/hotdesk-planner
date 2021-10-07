using Api.Mappers;
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
            GenericReservationCommandMapper<DeleteReservationCommand, Reservation> mapper = new();
            Reservation model = mapper.ConvertToModel(command);
            model.Id = command.Id;
            return await _service.Remove(model);
        }
    }
}
