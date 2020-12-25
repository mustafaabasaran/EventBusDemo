using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using EventBusDemo.Common;
using EventBusDemo.Common.Models;
using EventBusDemo.Common.RabbitMq;
using EventBusDemo.Customers.Commands;
using EventBusDemo.Customers.Data;
using EventBusDemo.Customers.Events;
using EventBusDemo.Customers.HttpServices;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace EventBusDemo.Customers
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<CustomerDbContext>(optionsAction => 
                optionsAction.UseNpgsql(Configuration.GetConnectionString("DefaultConnection"))
                );

            var httpServices = Configuration.GetOptions<HttpServiceOptions>("HttpServices");
            services.AddHttpClient<IProductHttpService, ProductHttpService>(client =>
            {
                client.BaseAddress = new Uri(httpServices.ProductHttpServiceUrl);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            return services.BuildContainer();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
            app.UseRabbitMq()
                .SubscribeCommand<CreateCustomer>()
                .SubscribeCommand<AddProductToBasket>()
                .SubscribeEvent<OrderCompleted>();

            SeedData.Initialize(app.ApplicationServices);
        }
    }
}