using Core.Models;
using MediatR;
using System.Collections.Generic;

namespace Api.Commands.Reservations
{
    public class GetAllReservationCommand : CommonReservationCommand, IRequest<List<Reservation>>
    {
    }
}
