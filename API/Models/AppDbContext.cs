using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace smartHome.Models
{
    public class AppDbContext : IdentityDbContext<User, UserRole, int>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Device> Devices { get; set; }
        public DbSet<AnalogDevice> AnalogDevices { get; set; }
    }
}
