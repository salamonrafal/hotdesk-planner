using Api.Commands.Users;
using Core.Exceptions;
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
    [Route("api/v1/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<User>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Get all data about available users in the office space")]
        public async Task<IActionResult> Get()
        {
            try
            {
                var data = await _mediator.Send(new GetAllUserCommand());
                
                return Ok(data);
            }
            catch (CommandEmptyException)
            {
                return BadRequest();
            }
            catch
            {
                return Problem (statusCode: 500);
            }
        }

        [HttpGet]
        [Route("user/{userId}")]
        [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Get data for specific users in the office space")]
        public async Task<IActionResult> GetById([FromRoute] Guid userId)
        {
            try
            {
                var data = await _mediator.Send(new GetUserCommand() { Id = userId });
                
                return Ok(data);
            } 
            catch ( FluentValidation.ValidationException ex )
            {
                return BadRequest (ex.Message);
            }
            catch (CommandEmptyException)
            {
                return BadRequest();
            } catch
            {
                return Problem (statusCode: 500);
            }
            
        }

        [HttpPut]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Put new user into the list of available users in the office space")]
        public async Task<IActionResult> Insert([FromBody] InsertUserCommand command)
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
            catch (CommandEmptyException)
            {
                return BadRequest();
            }
            catch
            {
                return Problem (statusCode: 500);
            }
        }

        [HttpPatch]
        [Route("user/{userId}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Update information about specific user")]
        public async Task<IActionResult> Update([FromRoute] Guid userId, [FromBody] UpdateUserCommand command)
        {
            try
            {
                command.UpdateId (userId);
                await _mediator.Send (command);
                
                return NoContent ();
            }
            catch ( FluentValidation.ValidationException ex )
            {
                return BadRequest (ex.Message);
            }
            catch ( CommandEmptyException )
            {
                return BadRequest();
            }
            catch
            {
                return Problem (statusCode: 500);
            }
        }

        [HttpDelete]
        [Route ("user/{userId}")]
        [ProducesResponseType (StatusCodes.Status202Accepted)]
        [ProducesResponseType (StatusCodes.Status400BadRequest)]
        [ProducesResponseType (StatusCodes.Status500InternalServerError)]
        [SwaggerOperation (Summary = "Remove user from list of available reservations in the office space")]
        public async Task<IActionResult> Delete ([FromRoute] Guid userId)
        {
            try
            {
                await _mediator.Send (new DeleteUserCommand () {Id = userId});
                
                return Accepted ();
            }
            catch ( FluentValidation.ValidationException ex )
            {
                return BadRequest (ex.Message);
            }
            catch ( CommandEmptyException )
            {
                return BadRequest();
            }
            catch
            {
                return Problem (statusCode: 500);
            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(List<User>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Route("search/")]
        [SwaggerOperation(
            Summary = "Search users in  list of available users in the office space",
            Description = "If you would like to search users, you should use below documentation about query document: " +
                "<a href=\"https://docs.mongodb.com/manual/tutorial/query-documents/\">https://docs.mongodb.com/manual/tutorial/query-documents/</a>"
        )]
        public async Task<IActionResult> Search([FromQuery] string query)
        {
            try
            {
                var data = await _mediator.Send(new SearchUserCommand() { Query = query });
                
                return Ok(data);
            }
            catch (CommandEmptyException)
            {
                return BadRequest();
            }
            catch
            {
                return Problem (statusCode: 500);
            }
        }
    }
}
