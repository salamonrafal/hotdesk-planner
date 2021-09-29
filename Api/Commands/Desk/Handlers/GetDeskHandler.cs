using Api.Mappers;
using Core.Exceptions;
using Core.Services;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using DeskModel = Core.Models.Desk;

namespace Api.Commands.Desk.Handlers
{
    public class GetDeskHandler : IRequestHandler<GetDeskCommand, DeskModel>
    {
        private readonly IDeskService _service;

        public GetDeskHandler(IDeskService service)
        {
            _service = service;
        }

        public async Task<DeskModel> Handle(GetDeskCommand command, CancellationToken cancellationToken)
        {
            if (command == GetDeskCommand.Empty) throw new CommandEmptyException("Command is Empty");

            GenericDeskCommandMapper<GetDeskCommand, DeskModel> mapper = new();
            var model = mapper.ConvertToModel(command);
            model.Id = command.Id;
            
            return await _service.Get(model);
        }
    }
}
