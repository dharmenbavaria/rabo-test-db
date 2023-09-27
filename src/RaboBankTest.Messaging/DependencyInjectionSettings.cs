using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Azure.Identity;
using Azure.Messaging.ServiceBus;

using Microsoft.Extensions.DependencyInjection;

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

            return serviceCollection;
        }
    }
}
