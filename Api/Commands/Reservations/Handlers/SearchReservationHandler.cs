using Core.Models;
using Core.Services;
using MediatR;
using MongoDB.Bson;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Api.Commands.Reservations.Handlers
{
    public class SearchReservationHandler : IRequestHandler<SearchReservationCommand, List<Reservation>>
    {
        private readonly IReservationService _service;

        public SearchReservationHandler(IReservationService service)
        {
            _service = service;
        }

        public async Task<List<Reservation>> Handle(SearchReservationCommand command, CancellationToken cancellationToken)
        {
            var document = BsonDocument.Parse(command.Query);
            return await _service.Search(document);
        }
    }
}
