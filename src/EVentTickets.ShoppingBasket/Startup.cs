using EventTickets.ShoppingBasket.Contexts;
using EventTickets.ShoppingBasket.Repositories;
using EventTickets.ShoppingBasket.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventTickets.ShoppingBasket
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
            services.AddDbContext<ShoppingBasketDbContext>(options => options.UseSqlite("Data Source=basket.db"));
            services.AddControllers();
            services.AddScoped<IBasketRepository, BasketRepository>();
            services.AddScoped<IBasketLineRepository, BasketLineRepository>();
            services.AddScoped<IEventRepository, EventRepository>();
            services.AddHttpClient<IEventCatalogService, EventCatalogService>(configuration => configuration.BaseAddress = new Uri("http://localhost:52441"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            using var scope = app.ApplicationServices.CreateScope();
            using var context = scope.ServiceProvider.GetRequiredService<ShoppingBasketDbContext>();
            context.Database.EnsureCreated();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
