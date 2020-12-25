using EventBusDemo.Common.Messages;

namespace EventBusDemo.Common.RabbitMq
{
    public interface IBusSubscriber
    {
        IBusSubscriber SubscribeCommand<Tcommand>(string _namespace = null)
            where Tcommand : ICommand;

        IBusSubscriber SubscribeEvent<TEvent>(string _namespace = null)
            where TEvent : IEvent;
    }
}