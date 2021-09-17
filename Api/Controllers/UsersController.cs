using Api.Commands.Users;
using Core.Exceptions;
using Core.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Net;
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
        public async Task<List<User>> Get()
        {
            try
            {
                Response.StatusCode = (int)HttpStatusCode.OK;
                return await _mediator.Send(new GetAllUserCommand() { });
            }
            catch (CommandEmptyException)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return null;
            }
            catch
            {
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return null;
            }
        }

        [HttpGet]
        [Route("user/{userId}")]
        [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Get data for specific users in the office space")]
        public async Task<User> GetById([FromRoute] Guid userId)
        {
            try
            {
                Response.StatusCode = (int) HttpStatusCode.OK;
                return await _mediator.Send(new GetUserCommand() { Id = userId });
            } catch (CommandEmptyException)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return null;
            } catch
            {
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return null;
            }
            
        }

        [HttpPut]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Put new user into the list of available users in the office space")]
        public async Task<Guid> Insert([FromBody] InsertUserCommand command)
        {
            try
            {
                Response.StatusCode = (int)HttpStatusCode.OK;
                return await _mediator.Send(command);
            }
            catch (CommandEmptyException)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Guid.Empty;
            }
            catch
            {
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return Guid.Empty;
            }
        }

        [HttpPatch]
        [Route("user/{userId}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Update information about specific user")]
        public async void Update([FromRoute] Guid userId, [FromBody] UpdateUserCommand command)
        {
            try
            {
                Response.StatusCode = (int)HttpStatusCode.Created;

                command.UpdateId(userId);
                await _mediator.Send(command);
            }
            catch (CommandEmptyException)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
            }
            catch
            {
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            }
        }

        [HttpDelete]
        [Route("user/{userId}")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Remove user from list of available reservations in the office space")]
        public async Task Delete([FromRoute] Guid userId)
        {
            try
            {
                Response.StatusCode = (int)HttpStatusCode.Accepted;
                await _mediator.Send(new DeleteUserCommand() { Id = userId });
            }
            catch (CommandEmptyException)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
            }
            catch
            {
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
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
        public async Task<List<User>> Search([FromQuery] string query)
        {
            try
            {
                Response.StatusCode = (int)HttpStatusCode.OK;
                return await _mediator.Send(new SearchUserCommand() { Query = query });
            }
            catch (CommandEmptyException)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return null;
            }
            catch
            {
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return null;
            }
        }
    }
}
