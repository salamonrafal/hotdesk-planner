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
        public async Task<IActionResult> Get()
        {
            try
            {
                var data = await _mediator.Send (new GetAllDeskCommand ());

                return Ok (data);
            }
            catch
            {
                return Problem (statusCode: 500);
            }
        }

        [HttpGet]
        [Route("desk/{deskId}")]
        [ProducesResponseType(typeof(Desk), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Get data for specific desks in the office")]
        public async Task<IActionResult> GetById([FromRoute] Guid deskId)
        {
            try
            {
                var data =  await _mediator.Send(new GetDeskCommand() { Id = deskId });
                
                return Ok(data);
            }
            catch
            {
                return Problem (statusCode: 500);
            }
            
        }

        [HttpPut]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Put new desk into the list of available desks in the office")]
        public async Task<IActionResult> Insert([FromBody] InsertDeskCommand command)
        {
            try
            {
                var data = await _mediator.Send(command);
                
                return Ok(data);
            }
            catch ( FluentValidation.ValidationException ex )
            {
                return BadRequest (ex.Message);
            }
            catch
            {
                return Problem (statusCode: 500);
            }
        }

        [HttpPatch]
        [Route("desk/{deskId}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Update information about specific desk")]
        public async Task<IActionResult> Update([FromRoute] Guid deskId, [FromBody] UpdateDeskCommand command)
        {
            try
            {
                command.UpdateId (deskId);
                await _mediator.Send (command);

                return NoContent ();
            }
            catch ( FluentValidation.ValidationException ex )
            {
                return BadRequest (ex.Message);
            }
            catch
            {
                return Problem (statusCode: 500);
            }
        }

        [HttpDelete]
        [Route("desk/{deskId}")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Remove desk from list of available desks in the office")]
        public async Task<IActionResult> Delete([FromRoute] Guid deskId)
        {
            try
            {
                await _mediator.Send (new DeleteDeskCommand () {Id = deskId});

                return Accepted ();
            }
            catch ( FluentValidation.ValidationException ex )
            {
                return BadRequest (ex.Message);
            }
            catch
            {
                return Problem (statusCode: 500);
            }
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
        public async Task<IActionResult> Search([FromQuery] string query)
        {
            try
            {
                var data = await _mediator.Send(new SearchDeskCommand() { Query = query });
                
                return Ok(data);
            }
            catch
            {
                return Problem (statusCode: 500);
            }
        }
    }
}
