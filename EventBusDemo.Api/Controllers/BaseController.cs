using System;
using System.Reflection;
using EventBusDemo.Api.Authentication.Model;
using EventBusDemo.Common.RabbitMq;
using Microsoft.AspNetCore.Mvc;

namespace EventBusDemo.Api.Controllers
{
    public class BaseController : ControllerBase
    {
        protected readonly IBusPublisher BusPublisher;

        public BaseController(IBusPublisher busPublisher)
        {
            BusPublisher = busPublisher;
        }

        protected TokenModel CurrentUser
        {
            get
            {
                return HttpContext.Items["CurrentCustomer"] != null
                    ? HttpContext.Items["CurrentCustomer"] as TokenModel
                    : null;
            }
        }

        protected ICorrelationContext GetContext()
        {
            return GetContext(CurrentUser.CustomerId);
        }

        protected ICorrelationContext GetContext(Guid currentUserCustomerId)
        {
            return CorrelationContext.Create(Guid.NewGuid(), currentUserCustomerId);
        }
    }
}