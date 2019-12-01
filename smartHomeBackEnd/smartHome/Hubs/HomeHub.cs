using Microsoft.AspNetCore.SignalR;
using smartHome.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace smartHome.Hubs
{
    public class HomeHub : Hub
    {
        private readonly AppDbContext context;
        private readonly DeviceRepository deviceRepository;
        private readonly AnalogDeviceRepository analogDeviceRepository;

        public HomeHub(AppDbContext context, AnalogDeviceRepository analogDeviceRepository, DeviceRepository deviceRepository)
        {
            this.context = context;
            this.analogDeviceRepository = analogDeviceRepository;
            this.deviceRepository = deviceRepository;
        }

        public async Task UpdateAnalogDevice(int id, byte value)
        {
            analogDeviceRepository.ChangeAnalogDeviceValue(id, value);
            AnalogDevice analogDevice = await context.AnalogDevices.FindAsync(id);
            await Clients.All.SendAsync("UpdateAnalogDevice", analogDevice);
        }

        public void Status()
        {
            Device[] devs = context.Devices.ToArray();
            AnalogDevice[] angDevs = context.AnalogDevices.ToArray();
            Clients.Caller.SendAsync("status", devs, angDevs);
        }

        public async Task Switch(int Id)
        {
            deviceRepository.Switch(Id);
            Device device = await context.Devices.FindAsync(Id);
            await Clients.All.SendAsync("UpdateFromServer", device);
        }

        public async Task SwitchT(int Id, string token)
        {
            Console.WriteLine("token is: " + token);
            deviceRepository.Switch(Id);
            Device device = await context.Devices.FindAsync(Id);
            await Clients.All.SendAsync("UpdateFromServer", device);
        }
    }
}
