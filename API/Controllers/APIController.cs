using Microsoft.AspNetCore.Mvc;

namespace smartHome.Controllers
{
    [Route("")]
    [Route("API")]
    [Route("[controller]/[action]")]
    [ApiController]
    public class APIController : ControllerBase
    {
        public APIController(){}

        [HttpGet]
        public ActionResult<string> Index()
        {
            return Ok("API is running");
        }
    }
}
