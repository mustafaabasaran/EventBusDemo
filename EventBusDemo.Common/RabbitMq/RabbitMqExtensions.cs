using System.Reflection;
using Autofac;
using EventBusDemo.Common.MessageHandlers;
using EventBusDemo.Common.Messages;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using RawRabbit;
using RawRabbit.Common;
using RawRabbit.Configuration;
using RawRabbit.Enrichers.MessageContext;
using RawRabbit.Instantiation;

namespace EventBusDemo.Common.RabbitMq
{
    public static class RabbitMqExtensions
    {
        public static IBusSubscriber UseRabbitMq(this IApplicationBuilder app)
            => new BusSubscriber(app);

        public static void AddRabbitMq(this ContainerBuilder builder)
        {
            builder.Register(context =>
            {
                var configuration = context.Resolve<IConfiguration>();
                var options = configuration.GetOptions<RabbitMqOptions>("rabbitMq");
                return options;
            }).SingleInstance();

            builder.Register(context =>
            {
                var configuration = context.Resolve<IConfiguration>();
                var options = configuration.GetOptions<RawRabbitConfiguration>("rabbitMq");
                return options;
            }).SingleInstance();

            var assembly = Assembly.GetCallingAssembly();
            builder.RegisterAssemblyTypes(assembly)
                .AsClosedTypesOf(typeof(IEventHandler<>))
                .InstancePerDependency();

            builder.RegisterAssemblyTypes(assembly)
                .AsClosedTypesOf(typeof(ICommandHandler<>))
                .InstancePerDependency();

            builder.RegisterType<BusSubscriber>().As<IBusPublisher>()
                .InstancePerDependency();

            Configurebus(builder);
        }

        private static void Configurebus(ContainerBuilder builder)
        {
            builder.Register<IInstanceFactory>(context =>
            {
                var options = context.Resolve<RabbitMqOptions>();
                var configuration = context.Resolve<RawRabbitConfiguration>();
                var namingConventions = new CustomNamingConventions(options.Namespace);

                return RawRabbitFactory.CreateInstanceFactory(new RawRabbitOptions()
                {
                    DependencyInjection = ioc =>
                    {
                        ioc.AddSingleton(options);
                        ioc.AddSingleton(configuration);
                        ioc.AddSingleton<INamingConventions>(namingConventions);
                    },
                    Plugins = p => p
                        .UseAttributeRouting()
                        .UseRetryLater()
                        .UseMessageContext<CorrelationContext>()
                        .UseContextForwarding()
                });
            }).SingleInstance();
            builder.Register(context => context.Resolve<IInstanceFactory>().Create());
        }
    }

    internal class CustomNamingConventions : NamingConventions
    {
        public CustomNamingConventions(string defaultNameSpace)
        {
            ExchangeNamingConvention = type => (type.GetCustomAttribute<MessageNameSpaceAttribute>()?.Namespace ??
                                                defaultNameSpace).ToLowerInvariant();
            RoutingKeyConvention = type =>
                    $"#.{type.GetCustomAttribute<MessageNameSpaceAttribute>()?.Namespace ?? defaultNameSpace}.{type.Name.Underscore()}"
                    .ToLowerInvariant();
        }
    }
}