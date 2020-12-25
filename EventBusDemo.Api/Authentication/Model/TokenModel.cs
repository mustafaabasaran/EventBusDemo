using System;

namespace EventBusDemo.Api.Authentication.Model
{
    public class TokenModel
    {
        public Guid CustomerId { get; set; }
        public string Email { get; set; }
    }
}