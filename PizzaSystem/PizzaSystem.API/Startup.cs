using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PizzaSystem.Data;
using PizzaSystem.Models.Interfaces;
using PizzaSystem.Models.Interfaces.Data;
using PizzaSystem.Models.Interfaces.Services;
using PizzaSystem.Service;
using PizzaSystem.Utils;

namespace PizzaSystem.API
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddSwaggerGen();

            //Configuring Repositories
            services.AddScoped<IIngredientRepository, IngredientFileRepository>();
            services.AddScoped<IOrderRepository, OrderFileRepository>();
            services.AddScoped<IPizzaRepository, PizzaFileRepository>();
            services.AddScoped<IProductRepository, ProductFileRepository>();

            //Configuring Services
            services.AddScoped<IIngredientService, IngredientService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IPizzaService, PizzaService>();
            services.AddScoped<IProductService, ProductService>();

            //Configuring Helpers
            services.AddScoped<IFileDatabase, FileDatabase>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
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
            app.UseMvc();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Pizza System");
            });
        }
    }
}
