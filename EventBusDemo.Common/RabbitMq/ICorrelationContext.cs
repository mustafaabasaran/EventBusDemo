using System;

namespace EventBusDemo.Common.RabbitMq
{
    public interface ICorrelationContext
    {
        Guid CorrelationId { get; }
        Guid CustomerId { get; }
    }
}