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
        public async Task<List<Reservation>> Get()
        {
            return await _mediator.Send(new GetAllReservationCommand() { }); ;
        }

        [HttpGet]
        [Route("reservation/{reservationId}")]
        [ProducesResponseType(typeof(Reservation), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Get data for specific reservation in the office")]
        public async Task<Reservation> GetById([FromRoute] Guid reservationId)
        {
            return await _mediator.Send(new GetReservationCommand() { Id = reservationId }); ;
        }

        [HttpPut]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Put new reservation into the list of available reservations in the office")]
        public async Task<Guid> Insert([FromBody] InsertReservationCommand command)
        {
            var uuid = await _mediator.Send(command);
            return uuid;
        }

        [HttpPatch]
        [Route("reservation/{reservationId}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Update information about specific reservation")]
        public async void Update([FromRoute] Guid reservationId, [FromBody] UpdateReservationCommand command)
        {
            command.UpdateId(reservationId);
            await _mediator.Send(command);
        }

        [HttpDelete]
        [Route("reservation/{reservationId}")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Remove reservation from list of available reservations in the office")]
        public async Task Delete([FromRoute] Guid reservationId)
        {
            await _mediator.Send(new DeleteReservationCommand() { Id = reservationId });
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
        public async Task<List<Reservation>> Search([FromQuery] string query)
        {
            return await _mediator.Send(new SearchReservationCommand() { Query = query }); ;
        }
    }
}
