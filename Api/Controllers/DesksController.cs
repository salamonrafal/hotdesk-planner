using Core.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace Api.Controllers
{
    [Route("api/v1/desks")]
    [ApiController]
    public class DesksController : ControllerBase
    {
        [HttpGet]
        public List<Desk> Get()
        {
            return new List<Desk>() { };
        }

        [HttpGet]
        [Route("{deskId}")]
        public Desk GetById([FromRoute] int deskId)
        {
            return new Desk() { };
        }

        [HttpPut]
        public Guid Insert([FromBody] Desk desk)
        {
            return Guid.Empty;
        }

        [HttpPatch]
        public void Update([FromBody] Desk desk)
        {

        }

        [HttpDelete]
        public void Delete([FromBody] Desk desk)
        {

        }

        [HttpPost]
        [Route("search")]
        public List<Desk> Search([FromBody] Desk searcgQuery)
        {
            return new List<Desk>();
        }
    }
}
