using MediatR;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DeskModel = Core.Models.Desk;

namespace Api.Commands.Desk
{
    public class SearchDeskCommand : IRequest<List<DeskModel>>
    {
        public string Query { get; set; }
    }
}
