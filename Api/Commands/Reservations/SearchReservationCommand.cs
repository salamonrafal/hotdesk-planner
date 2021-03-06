using Core.Models;
using MediatR;
using System.Collections.Generic;

namespace Api.Commands.Reservations
{
    public class SearchReservationCommand : IRequest<List<Reservation>>
    {
        public string Query { get; set; }
    }
}
