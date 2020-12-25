using System.Threading.Tasks;
using EventBusDemo.Common.Messages;
using EventBusDemo.Common.RabbitMq;

namespace EventBusDemo.Common.MessageHandlers
{
    public interface ICommandHandler<in TCommand> where TCommand : ICommand
    {
        Task HandleAsync(TCommand command, ICorrelationContext context);
    }
}