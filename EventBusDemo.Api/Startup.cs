using System;
using System.Net.Http.Headers;
using EventBusDemo.Api.Authentication;
using EventBusDemo.Api.HttpServices;
using EventBusDemo.Common;
using EventBusDemo.Common.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;


namespace EventBusDemo.Api
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddJwtAuthentication();

            var httpServices = Configuration.GetOptions<HttpServiceOptions>("HttpServices");
            services.AddHttpClient<ICustomerHttpService, CustomerHttpService>(client =>
            {
                client.BaseAddress = new Uri(httpServices.CustomerHttpServiceUrl);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            });
            services.AddHttpClient<IProductHttpService, ProductHttpService>(client =>
            {
                client.BaseAddress = new Uri(httpServices.ProductHttpServiceUrl);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            });

            return services.BuildContainer();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAuthentication();
            app.UseMvc();
        }
    }
}