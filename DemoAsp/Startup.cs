using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoAsp
{
    public class Startup
    {
        IConfiguration config;
        public Startup(IConfiguration configuration)
        {
            config = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseStaticFiles();   
            app.UseRouting();
            app.Use(async (context, next) =>
            {
                await context.Response.WriteAsync("Middleware1: incoming request \n ");
                await next();
                await context.Response.WriteAsync("Middleware1: outgoing request \n");

            });
            app.Use(async (context, next) =>
            {
                await context.Response.WriteAsync("Middleware2: incoming request \n ");
                await next();
                await context.Response.WriteAsync("Middleware2: outgoing request \n");

            });
            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Middleware3: incoming request and request generated \n ");

            });
            //  app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapGet("/", async context =>
            //    {
            //        await context.Response.WriteAsync($"Company = {config["MyCompanyName"]}");
            //      //      +System.Diagnostics.Process.GetCurrentProcess().ProcessName);
            //    });
            //});
        }
    }
}
