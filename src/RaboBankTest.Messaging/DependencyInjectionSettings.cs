using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Azure.Identity;
using Azure.Messaging.ServiceBus;

using Microsoft.Extensions.DependencyInjection;

using RaboBankTest.Application.Contracts;

namespace RaboBankTest.Messaging
{
    public static class DependencyInjectionSettings
    {
        public static IServiceCollection SetupMessaging(
            this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<IServiceBusPersisterConnection>(
                sp =>
                    {
                        var serviceBusConnectionString = Environment.GetEnvironmentVariable("ServiceBusConnectionString");

                        return new DefaultServiceBusPersisterConnection(serviceBusConnectionString);
                    });

            serviceCollection.AddSingleton<IMessageSender, MessageSender>(
                sp =>
                    {
                        var serviceBusPersisterConnection = sp.GetRequiredService<IServiceBusPersisterConnection>();

                        return new MessageSender(serviceBusPersisterConnection);
                    });

            return serviceCollection;
        }
    }
}
