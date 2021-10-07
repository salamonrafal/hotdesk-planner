using Api.Mappers;
using Core.Models;
using Core.Services;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Api.Commands.Reservations.Handlers
{
    public class UpdateReservationHandler : IRequestHandler<UpdateReservationCommand, bool>
    {
        private readonly IReservationService _service;

        public UpdateReservationHandler(IReservationService service)
        {
            _service = service;
        }

        public async Task<bool> Handle(UpdateReservationCommand command, CancellationToken cancellationToken)
        {
            GenericReservationCommandMapper<UpdateReservationCommand, Reservation> mapper = new();
            var desk = mapper.ConvertToModel(command);
            desk.Id = command.Id;
            return await _service.Update(desk);
        }
    }
}
