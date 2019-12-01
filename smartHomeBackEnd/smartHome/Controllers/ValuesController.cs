using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using smartHome.Hubs;
using smartHome.Models;

namespace smartHome.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        public IHubContext<HomeHub> HomeHub { get; }

        public ValuesController(IHubContext<HomeHub> homeHub)
        {
            HomeHub = homeHub;
        }

        // GET api/values
        //[Authorize]
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            Device device = new Device { DeviceId = 1, DeviceName = "light", RelaySwitch = false };
            HomeHub.Clients.All.SendAsync("UpdateFromServer", device);
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
