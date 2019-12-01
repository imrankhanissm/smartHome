using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace smartHome.Models
{
    public class UserRole: IdentityRole<int> { }
    public class User : IdentityUser<int> { }
}
