using Microsoft.AspNetCore.Identity;

namespace smartHome.Models
{
    public class UserRole: IdentityRole<int> { }
    public class User : IdentityUser<int> { }
}
