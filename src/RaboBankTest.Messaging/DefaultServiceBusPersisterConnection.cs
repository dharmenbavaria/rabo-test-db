using Azure.Messaging.ServiceBus.Administration;
using Azure.Messaging.ServiceBus;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaboBankTest.Messaging
{
    public class DefaultServiceBusPersisterConnection : IServiceBusPersisterConnection
    {
        private readonly string _serviceBusConnectionString;

        private ServiceBusClient _topicClient;

        bool _disposed;

        public DefaultServiceBusPersisterConnection(
            string serviceBusConnectionString)
        {
            _serviceBusConnectionString = serviceBusConnectionString;
            _topicClient = new ServiceBusClient(_serviceBusConnectionString);
        }

        public ServiceBusClient TopicClient
        {
            get
            {
                if ( _topicClient.IsClosed )
                {
                    _topicClient = new ServiceBusClient(_serviceBusConnectionString);
                }

                return _topicClient;
            }
        }

        public async ValueTask DisposeAsync()
        {
            if ( _disposed )
                return;

            _disposed = true;
            await _topicClient.DisposeAsync();
        }
    }
}
