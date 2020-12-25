using System;

namespace EventBusDemo.Common.RabbitMq
{
    public class CorrelationContext : ICorrelationContext
    {
        public Guid CorrelationId { get; }
        public Guid CustomerId { get; }

        public CorrelationContext()
        {
            
        }

        public CorrelationContext( Guid correlationId, Guid customerId)
        {
            CorrelationId = correlationId;
            CustomerId = customerId;
        }

        public static ICorrelationContext Create(Guid correlationId, Guid customerid)
        {
            return new CorrelationContext(correlationId, customerid);
        }
    }
}