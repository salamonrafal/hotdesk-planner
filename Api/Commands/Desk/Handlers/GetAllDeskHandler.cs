using Core.Services;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using DeskModel = Core.Models.Desk;

namespace Api.Commands.Desk.Handlers
{
    public class GetAllDeskHandler : IRequestHandler<GetAllDeskCommand, List<DeskModel>>
    {
        private readonly IDeskService _service;

        public GetAllDeskHandler(IDeskService service)
        {
            _service = service;
        }

        public async Task<List<DeskModel>> Handle(GetAllDeskCommand command, CancellationToken cancellationToken)
        {
            return await _service.Get();
        }
    }
}
