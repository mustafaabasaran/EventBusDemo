using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using EventBusDemo.Api.Authentication.Model;
using EventBusDemo.Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;

namespace EventBusDemo.Api.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public AuthenticationService(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public TokenModel GetCurrenctUser()
            => _contextAccessor.HttpContext?.Items?["CurrentCustomer"] != null
                ? _contextAccessor.HttpContext.Items["CurrentCustomer"] as TokenModel
                : null;
        
        public string GetToken(Customer customer)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("BakHele");

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Audience = "SomeApp",
                Issuer = "bir.demo.api",
                Subject = new ClaimsIdentity(new Claim[]
                {
                   new Claim(ClaimTypes.Name, customer.Email),
                   new Claim("CustomerId", customer.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(5),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                    
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}