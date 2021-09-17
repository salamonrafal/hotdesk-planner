using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Api.Controllers
{

    public class TestData
    {
        public string MongoDBUser { get; set; }
    }

    [Route("api/v1/test")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly IConfiguration _config;

        public TestController(IConfiguration config)
        {
            _config = config;
        }

        [HttpGet]
        public TestData Get()
        {
            return new TestData()
            {
                MongoDBUser = _config["MongoDB:DB_USER"]
            };
        }
    }
}
