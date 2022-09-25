using System;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using smartHome.Hubs;
using smartHome.Models;
using smartHome.Services;

namespace smartHome
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSignalR();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
            // services.AddDbContextPool<AppDbContext>(options => options.UseMySql(Environment.GetEnvironmentVariable("DBConnectionString")));
            services.AddDbContextPool<AppDbContext>(options => {
                options.UseMySql(Environment.GetEnvironmentVariable("DBConnectionString"), MySqlServerVersion.AutoDetect(Environment.GetEnvironmentVariable("DBConnectionString")));
            });
            services.AddScoped<DeviceRepository>();
            services.AddScoped<AnalogDeviceRepository>();
            services.AddScoped<DataBaseService>();
            services.AddIdentity<User, UserRole>(options => { options.User.RequireUniqueEmail = true; }).AddEntityFrameworkStores<AppDbContext>();
            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/api/Auth/seed";
            });
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = false;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration.GetValue<string>("AppSettings:JWT:Secret"))),
                    ValidateAudience = false,
                    ValidateIssuer = false
                };
            });

            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder.AllowAnyHeader()
                    .AllowAnyMethod()
                    // .AllowCredentials()
                    .AllowAnyOrigin().
                    Build();
                    //.WithOrigins(Configuration.GetValue<string>("AppSettings:Client_url"));
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseAuthentication();
            // app.UseSignalR(routes =>
            // {
            //     routes.MapHub<HomeHub>("/Home");
            // });

            app.UseRouting();

            app.UseCors();

            app.UseEndpoints(routes => {
                routes.MapHub<HomeHub>("/Home");
                routes.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=api}/{action=index}");
            });
        }
    }
}
