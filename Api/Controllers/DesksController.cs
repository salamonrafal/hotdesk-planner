using Api.Commands.Desk;
using Core.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Swashbuckle.AspNetCore.Annotations;
using Microsoft.AspNetCore.Http;

namespace Api.Controllers
{
    [Produces("application/json")]
    [Route("api/v1/desks/")]
    [ApiController]
    public class DesksController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DesksController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<Desk>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Get all data about available desks in the office")]
        public async Task<List<Desk>> Get()
        {
            return await _mediator.Send(new GetAllDeskCommand() { }); ;
        }

        [HttpGet]
        [Route("desk/{deskId}")]
        [ProducesResponseType(typeof(Desk), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Get data for specific desks in the office")]
        public async Task<Desk> GetById([FromRoute] Guid deskId)
        {
            return await _mediator.Send(new GetDeskCommand() { Id = deskId }); ;
        }

        [HttpPut]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Put new desk into the list of available desks in the office")]
        public async Task<Guid> Insert([FromBody] InsertDeskCommand command)
        {
            var uuid = await _mediator.Send(command);
            return uuid;
        }

        [HttpPatch]
        [Route("desk/{deskId}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Update information about specific desk")]
        public async void Update([FromRoute] Guid deskId, [FromBody] UpdateDeskCommand command)
        {
            command.UpdateId(deskId);
            await _mediator.Send(command);
        }

        [HttpDelete]
        [Route("desk/{deskId}")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Remove desk from list of available desks in the office")]
        public async Task Delete([FromRoute] Guid deskId)
        {
            await _mediator.Send(new DeleteDeskCommand() { Id = deskId});
        }

        [HttpPost]
        [ProducesResponseType(typeof(List<Desk>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Route("search/")]
        [SwaggerOperation(
            Summary = "Search desks in  list of available desks in the office", 
            Description = "If you would like to search desks you should use below documentation about query document: " +
                "<a href=\"https://docs.mongodb.com/manual/tutorial/query-documents/\">https://docs.mongodb.com/manual/tutorial/query-documents/</a>"
        )]
        public async Task<List<Desk>> Search([FromQuery] string query)
        {
            return await _mediator.Send(new SearchDeskCommand() { Query = query }); ;
        }
    }
}
