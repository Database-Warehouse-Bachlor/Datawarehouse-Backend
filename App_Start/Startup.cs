using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Datawarehouse_Backend.Data;
using Datawarehouse_Backend.App_Data;
using Datawarehouse_Backend.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Npgsql;

namespace Datawarehouse_Backend.App_Start
{
    public class Startup
    {
        private string logindb;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            logindb = Configuration["loginDatabase"];
            
            Console.WriteLine(logindb);
            services.AddEntityFrameworkNpgsql().AddDbContext<DatabaseContext>(opt =>
            opt.UseNpgsql(logindb));
            
            
           // services.AddEntityFrameworkNpgsql().AddDbContext<DbContext>(options => options.UseNpgsql(Configuration.GetConnectionString("DbContext")));
            services.AddScoped<IUserRepo, MockUserRepo>();
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
