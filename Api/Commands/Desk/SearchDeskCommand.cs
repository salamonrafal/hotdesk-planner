using MediatR;
using System.Collections.Generic;
using DeskModel = Core.Models.Desk;

namespace Api.Commands.Desk
{
    public class SearchDeskCommand : IRequest<List<DeskModel>>
    {
        public string Query { get; set; }
    }
}
