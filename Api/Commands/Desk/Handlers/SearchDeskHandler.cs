using Api.Mappers;
using Core.Exceptions;
using Core.Services;
using MediatR;
using MongoDB.Bson;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Threading;
using System.Threading.Tasks;
using DeskModel = Core.Models.Desk;

namespace Api.Commands.Desk.Handlers
{
    public class SearchDeskHandler : IRequestHandler<SearchDeskCommand, List<DeskModel>>
    {
        public readonly IDeskService _service;

        public SearchDeskHandler(IDeskService service)
        {
            _service = service;
        }

        public async Task<List<DeskModel>> Handle(SearchDeskCommand command, CancellationToken cancellationToken)
        {
            var document = BsonDocument.Parse(command.Query);
            return await _service.Search(document);
        }
    }
}
