using Core.Models;
using Core.Services;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Api.Commands.Reservations.Handlers
{
    public class GetAllReservationHandler : IRequestHandler<GetAllReservationCommand, List<Reservation>>
    {
        private readonly IReservationService _service;

        public GetAllReservationHandler(IReservationService service)
        {
            _service = service;
        }

        public async Task<List<Reservation>> Handle(GetAllReservationCommand command, CancellationToken cancellationToken)
        {
            return await _service.Get();
        }
    }
}
