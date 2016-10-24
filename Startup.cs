using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using aspnetcoreapp.Models;
using Microsoft.EntityFrameworkCore.Infrastructure;
using aspnetcoreapp.Database;

namespace aspnetcoreapp
{
    public class Startup
    {
        // public void Configure(IApplicationBuilder app)
        // {
        //     app.Run(context =>
        //     {
        //         return context.Response.WriteAsync("Hello, hello moooooo, from ASP.NET Core!");

        //     });
        // }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            //Add MVC module and specify Root
            app.UseMvc();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            //Perform environment specific task
            if (env.IsDevelopment())
            {
                //When it is dev environment
            }
            else
            {
                //Other then Development

            }

            using (var db = new ItemsContext())
            {
                try
                {
                    db.Database.EnsureCreated();
                }
                catch (Exception ex)
                {

                }
            }

            //Add static file module
            app.UseStaticFiles();
            //Use Identity Module
            //app.UseIdentity();
        }


        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc();

            services.AddSingleton<IRepository, Repository>();

            services.AddEntityFrameworkSqlite().AddDbContext<ItemsContext>();
        }
    }
}