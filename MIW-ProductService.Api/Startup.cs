using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MIW_ProductService.Api.Controllers;
using MIW_ProductService.Core.Services.Interfaces;
using MIW_ProductService.Dal.Contexts;

namespace MIW_ProductService.Api
{
    public class Startup
    {
    
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            string server = Configuration.GetSection("Mysql")["Server"];
            string username = Configuration.GetSection("Mysql")["Username"];
            string password = Configuration.GetSection("Mysql")["Password"];
            string database = Configuration.GetSection("Mysql")["Database"];
            string connectionString = $"server={server};user={username};password={password};database={database}";
            services.AddDbContext<ProductContext>(builder =>
                builder.UseMySQL(connectionString));
            
            services.AddTransient<IProductService, Core.Services.ProductService>();
            
            services.AddGrpc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ProductContext db)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            db.Database.EnsureCreated();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGrpcService<ProductController>();

                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");
                });
            });
        }
    }
}