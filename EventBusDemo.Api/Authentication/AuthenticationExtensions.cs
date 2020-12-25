using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventBusDemo.Api.Authentication.Model;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace EventBusDemo.Api.Authentication
{
    public static class AuthenticationExtensions
    {
        private const string SecretKey = "BakHele";

        public static void AddJwtAuthentication(this IServiceCollection services)
        {
            services.AddScoped<IHttpContextAccessor, HttpContextAccessor>()
                .AddScoped<IAuthenticationService, AuthenticationService>()
                .AddAuthentication(x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(x =>
                {
                    x.Audience = "SomeApp";
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.ClaimsIssuer = "bir.demo.api";
                    x.TokenValidationParameters = new TokenValidationParameters()
                    {
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(SecretKey)),
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidateIssuer = false,
                        ValidateAudience = true
                    };
                    x.Events = new JwtBearerEvents()
                    {
                        OnTokenValidated = (context) =>
                        {
                            var name = context.Principal.Identity.Name;
                            if (string.IsNullOrEmpty(name))
                            {
                                context.Fail("Unauthorized. Please re login");
                            }
                            context.HttpContext.Items.Add("CurrentCustomer",
                                new TokenModel
                                {
                                    Email = context.Principal.Identity.Name,
                                    CustomerId = Guid.Parse(context.Principal.Claims.First(s => s.Type == "CustomerId")
                                        .Value)
                                });
                            return Task.CompletedTask;
                        }
                    };
                });
        }
    }
}