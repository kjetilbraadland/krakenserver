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
using Microsoft.AspNetCore.Mvc.Razor;

using React.AspNet;

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

            loggerFactory.AddConsole();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }

            //Add MVC module and specify Root
            // app.UseMvc();
            // app.UseMvc(routes =>
            // {
            //     routes.MapRoute(
            //         name: "default",
            //         template: "{controller=Home}/{action=Index}/{id?}");
            // });

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

            app.UseReact(config =>
            {
                // If you want to use server-side rendering of React components,
                // your components as well as all of their dependencies.
                // See http://reactjs.net/ for more information. Example:
                //config
                //  .AddScript("~/Scripts/First.jsx")
                //  .AddScript("~/Scripts/Second.jsx");

                // If you use an external build too (for example, Babel, Webpack,
                // Browserify or Gulp), you can improve performance by disabling
                // ReactJS.NET's version of Babel and loading the pre-transpiled
                // scripts. Example:
                //config
                //  .SetLoadBabel(false)
                //  .AddScriptWithoutTransform("~/Scripts/bundle.server.js");

                //config.AddScript("~/js/remarkable.min.js").AddScript("~/js/Tutorial.jsx");
                // config
                //     .SetReuseJavaScriptEngines(true)
                //     .AddScript("~/js/Admin.jsx")
                //     .SetUseDebugReact(true);
            });

            //Add static file module
            app.UseStaticFiles();
            app.UseMvcWithDefaultRoute();
            //Use Identity Module
            //app.UseIdentity();
        }


        public void ConfigureServices(IServiceCollection services)
        {

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddReact();           

            // Add framework services.
            services.AddMvc();           

            services.AddSingleton<IRepository, Repository>();

            services.AddEntityFrameworkSqlite().AddDbContext<ItemsContext>();
        }
    }
}