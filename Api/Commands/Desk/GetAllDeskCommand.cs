using MediatR;
using System.Collections.Generic;
using DeskModel = Core.Models.Desk;

namespace Api.Commands.Desk
{
    public class GetAllDeskCommand : CommonDeskCommand, IRequest<List<DeskModel>>
    {
    }
}
