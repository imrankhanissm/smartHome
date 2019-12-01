using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using smartHome.Hubs;
using smartHome.Models;

namespace smartHome.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DevicesController : ControllerBase
    {
        private readonly AppDbContext context;
        private readonly DeviceRepository deviceRepository;
        private readonly IHubContext<HomeHub> hubContext;

        public DevicesController(AppDbContext context, DeviceRepository deviceRepository, IHubContext<HomeHub> hubContext)
        {
            this.context = context;
            this.deviceRepository = deviceRepository;
            this.hubContext = hubContext;
        }

        [HttpGet]
        public IEnumerable<Device> Status()
        {
            return context.Devices;
        }

        // GET: api/Devices/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Status([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var device = await context.Devices.FindAsync(id);

            if (device == null)
            {
                return NotFound();
            }

            return Ok(device);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Switch([FromRoute]int Id)
        {
            deviceRepository.Switch(Id);
            Device device = await context.Devices.FindAsync(Id);
            await hubContext.Clients.All.SendAsync("UpdateFromServer", device);
            return Ok();
        }





        // PUT: api/Devices/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDevice([FromRoute] int id, [FromBody] Device device)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != device.DeviceId)
            {
                return BadRequest();
            }

            context.Entry(device).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DeviceExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Devices
        [HttpPost]
        public async Task<IActionResult> PostDevice([FromBody] Device device)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            context.Devices.Add(device);
            await context.SaveChangesAsync();

            return CreatedAtAction("GetDevice", new { id = device.DeviceId }, device);
        }

        // DELETE: api/Devices/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDevice([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var device = await context.Devices.FindAsync(id);
            if (device == null)
            {
                return NotFound();
            }

            context.Devices.Remove(device);
            await context.SaveChangesAsync();

            return Ok(device);
        }

        private bool DeviceExists(int id)
        {
            return context.Devices.Any(e => e.DeviceId == id);
        }
    }
}