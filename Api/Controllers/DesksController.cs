using Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Route("api/v1/desks")]
    [ApiController]
    public class DesksController : ControllerBase
    {
        [HttpGet]
        public List<Desk> Get ()
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
        public int Insert([FromBody] Desk desk)
        {
            return 0;
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
