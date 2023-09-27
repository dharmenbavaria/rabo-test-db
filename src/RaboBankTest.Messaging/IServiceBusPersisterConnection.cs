using Azure.Messaging.ServiceBus.Administration;
using Azure.Messaging.ServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaboBankTest.Messaging
{
    public interface IServiceBusPersisterConnection : IAsyncDisposable
    {
        ServiceBusClient TopicClient { get; }
    }
}
