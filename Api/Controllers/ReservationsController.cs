using Core.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace Api.Controllers
{
    [Route("api/v1/reservations")]
    [ApiController]
    public class ReservationsController : ControllerBase
    {
        [HttpGet]
        public List<Reservation> Get()
        {
            return new List<Reservation>() { };
        }

        [HttpGet]
        [Route("{reservationId}")]
        public Reservation GetById([FromRoute] int reservationId)
        {
            return new Reservation() { };
        }

        [HttpPut]
        public Guid Insert([FromBody] Reservation reservation)
        {
            return Guid.Empty;
        }

        [HttpPatch]
        public void Update([FromBody] Reservation reservation)
        {

        }

        [HttpDelete]
        public void Delete([FromBody] Reservation reservation)
        {

        }

        [HttpPost]
        [Route("search")]
        public List<Reservation> Search([FromBody] Localization searcgQuery)
        {
            return new List<Reservation>();
        }
    }
}
