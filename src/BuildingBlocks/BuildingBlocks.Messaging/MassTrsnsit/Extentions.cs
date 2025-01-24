using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace BuildingBlocks.Messaging.MassTrsnsit
{
    public static class Extentions
    {
        public static IServiceCollection AddMessageBroker(this IServiceCollection services, IConfiguration configuration, Assembly? assembly = null) 
        {
            services.AddMassTransit(config =>
            {
                config.SetKebabCaseEndpointNameFormatter();
                if (assembly != null)
                    config.AddConsumers(assembly);

                config.UsingRabbitMq((context, configurator) =>
                {
                    var host = configuration["MessageBroker:Host"];
                    var username = configuration["MessageBroker:Username"];
                    var password = configuration["MessageBroker:Password"];
                    configurator.Host(new Uri(host!), host =>
                    {
                        host.Username(username!);
                        host.Password(password!);
                    });
                    configurator.ConfigureEndpoints(context);
                });
            });
            return services;
        }
    }
}
