using Core.Models;
using Core.Services;
using MediatR;
using MongoDB.Bson;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Api.Commands.Users.Handlers
{
    public class SearchUserHandler : IRequestHandler<SearchUserCommand, List<User>>
    {
        public readonly IUserService _service;

        public SearchUserHandler(IUserService service)
        {
            _service = service;
        }

        public async Task<List<User>> Handle(SearchUserCommand command, CancellationToken cancellationToken)
        {
            var document = BsonDocument.Parse(command.Query);
            return await _service.Search(document);
        }
    }
}
