using FirstCourse.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirstCourse
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940

        public IConfiguration _configuration { get; }

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

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

            app.UseRouting();

            app.Use(async (context, next) =>
            {
                await context.Response.WriteAsync("<div>Call from middleware 1</div>");
                await next.Invoke();
                await context.Response.WriteAsync("<div>Return from middleware 1</div>");
            });

            app.Use(async (context, next) =>
            {
                await context.Response.WriteAsync("<div>Call from middleware 2</div>");
                await next.Invoke();
                await context.Response.WriteAsync("<div>Return from middleware 2</div>");
            });

            app.UseMiddleware<Middleware1>();

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("<div>Call from middleware 3</div>");
                await context.Response.WriteAsync(_configuration.GetSection("Message").Value);
                await context.Response.WriteAsync(_configuration.GetSection("ConnectionStrings:OracleConnectionString").Value);
                await context.Response.WriteAsync(_configuration.GetSection("Student:0:Name").Value);
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Hello World!");
                });
            });
        }
    }
}
