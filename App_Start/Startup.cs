using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Datawarehouse_Backend.App_Data;
using Datawarehouse_Backend.Models;
using Datawarehouse_Backend.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Npgsql;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Identity;


namespace Datawarehouse_Backend.App_Start
{
    public class Startup
    {
        private string logindb;
        private string warehousedb;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(opt => {
                opt.AddPolicy("CorsPolicy", builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader().AllowCredentials().Build());
            });
            
            services.AddControllersWithViews();



            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(opt => 
                {
                    opt.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer=true,
                        ValidateAudience=true,
                        ValidateLifetime=true,
                        ValidateIssuerSigningKey=true,
                        ValidIssuer=Configuration["Jwt:Issuer"],
                        ValidAudience=Configuration["Jwt:Issuer"],
                        IssuerSigningKey=new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
                    };
                });
            services.AddMvc();
            
            // Configures database connection. One setup for the login database and one for the warehouse.  
            logindb = Configuration["loginDatabase"];
            Console.WriteLine(logindb);
            services.AddDbContext<LoginDatabaseContext>(opt =>
            opt.UseNpgsql(logindb));

            warehousedb = Configuration["warehouseDatabase"];
            Console.WriteLine(warehousedb);
            services.AddDbContext<WarehouseContext>(opt =>
            opt.UseNpgsql(warehousedb));

            
            //services.AddIdentity<User, Role>()
            //.AddUserStore<LoginDatabaseContext>()
            //.AddDefaultTokenProviders();
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
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();
            
            app.UseAuthentication();
           
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
