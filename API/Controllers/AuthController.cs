using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using smartHome.Models;

namespace smartHome.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public UserManager<User> UserManager { get; }
        public IConfiguration Configuration { get; }

        private SignInManager<User> SignInManager;

        public AuthController(UserManager<User> UserManager, IConfiguration Configuration, SignInManager<User> signInManager)
        {
            this.UserManager = UserManager;
            this.Configuration = Configuration;
            this.SignInManager = signInManager;
        }

        // seed user
        [HttpGet]
        public async Task<string> Seed()
        {
            User user = new User
            {
                UserName = "imran",
                Email = "imrankhanissm@gmail.com"
            };
            await UserManager.CreateAsync(user, "Imran@1234");
            return "seeding success";
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            var user = await UserManager.FindByNameAsync(loginModel.userName);
            if (await UserManager.CheckPasswordAsync(user, loginModel.password))
            {
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration.GetValue<string>("AppSettings:JWT:Secret")));
                var jwtToken = new JwtSecurityToken(
                    claims: new[] { new Claim("username", user.UserName) },
                    expires: DateTime.Now.AddDays(Configuration.GetValue<int>("JWT:ExpTimeInDays")),
                    signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
                );
                return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(jwtToken) });
            }
            return Forbid();
        }
    }
}