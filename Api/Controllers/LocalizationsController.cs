using Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Route("api/v1/localizations")]
    [ApiController]
    public class LocalizationsController : ControllerBase
    {
        [HttpGet]
        public List<Localization> Get ()
        {
            return new List<Localization>() { };
        }

        [HttpGet]
        [Route("{localizationId}")]
        public Localization GetById([FromRoute] int localizationId)
        {
            return new Localization() { };
        }

        [HttpPut]
        public Guid Insert([FromBody] Localization localization)
        {
            return Guid.Empty;
        }

        [HttpPatch]
        public void Update([FromBody] Localization localization)
        {

        }

        [HttpDelete]
        public void Delete([FromBody] Localization localization)
        {

        }

        [HttpPost]
        [Route("search")]
        public List<Localization> Search([FromBody] Localization searcgQuery)
        {
            return new List<Localization>(); 
        }
    }
}
