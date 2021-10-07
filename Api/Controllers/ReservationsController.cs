using Api.Commands.Reservations;
using Core.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Produces("application/json")]
    [Route("api/v1/reservations")]
    [ApiController]
    public class ReservationsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ReservationsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<Reservation>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Get all data about available reservations in the office")]
        public async Task<IActionResult> Get()
        {
            try
            {
                var data= await _mediator.Send(new GetAllReservationCommand());
                
                return Ok(data);
            }
            catch
            {
                return Problem (statusCode: 500);
            }
        }

        [HttpGet]
        [Route("reservation/{reservationId}")]
        [ProducesResponseType(typeof(Reservation), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Get data for specific reservation in the office")]
        public async Task<IActionResult> GetById([FromRoute] Guid reservationId)
        {
            try
            {
                var data =  await _mediator.Send(new GetReservationCommand() { Id = reservationId });
                
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
        [SwaggerOperation(Summary = "Put new reservation into the list of available reservations in the office")]
        public async Task<IActionResult> Insert([FromBody] InsertReservationCommand command)
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
        [Route("reservation/{reservationId}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Update information about specific reservation")]
        public async Task<IActionResult> Update([FromRoute] Guid reservationId, [FromBody] UpdateReservationCommand command)
        {
            try
            {
                command.UpdateId (reservationId);
                await _mediator.Send (command);
                
                return NoContent();
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
        [Route("reservation/{reservationId}")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Remove reservation from list of available reservations in the office")]
        public async Task<IActionResult> Delete([FromRoute] Guid reservationId)
        {
            try
            {
                await _mediator.Send (new DeleteReservationCommand () {Id = reservationId});
                
                return Accepted();
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
        [ProducesResponseType(typeof(List<Reservation>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Route("search/")]
        [SwaggerOperation(
            Summary = "Search reservations in  list of available reservations in the office",
            Description = "If you would like to search reservations you should use below documentation about query document: " +
                "<a href=\"https://docs.mongodb.com/manual/tutorial/query-documents/\">https://docs.mongodb.com/manual/tutorial/query-documents/</a>"
        )]
        public async Task<IActionResult> Search([FromQuery] string query)
        {
            try
            {
                var data = await _mediator.Send(new SearchReservationCommand() { Query = query });
                
                return Ok(data);
            }
            catch
            {
                return Problem (statusCode: 500);
            }
        }
    }
}
