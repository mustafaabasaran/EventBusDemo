using System.Threading.Tasks;
using EventBusDemo.Common.Messages;
using EventBusDemo.Common.RabbitMq;

namespace EventBusDemo.Common.MessageHandlers
{
    public interface IEventHandler<in TEvent> where TEvent : IEvent
    {
        Task HandleAsync(TEvent @event, ICorrelationContext context);
    }
}