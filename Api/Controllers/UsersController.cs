using Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Route("api/v1/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        [HttpGet]
        public List<User> Get ()
        {
            return new List<User>() { };
        }

        [HttpGet]
        [Route("{userId}")]
        public User GetById([FromRoute] int userId)
        {
            return new User() { };
        }

        [HttpPut]
        public Guid Insert([FromBody] User user)
        {
            return Guid.Empty;
        }

        [HttpPatch]
        public void Update([FromBody] User user)
        {

        }

        [HttpDelete]
        public void Delete([FromBody] User  user)
        {

        }

        [HttpPost]
        [Route("search")]
        public List<User> Search([FromBody] User searcgQuery)
        {
            return new List<User>(); 
        }
    }
}
