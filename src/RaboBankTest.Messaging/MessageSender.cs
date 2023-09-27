using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

using Azure.Messaging.ServiceBus;

using RaboBankTest.Application.Contracts;
using RaboBankTest.Domain.Message;

namespace RaboBankTest.Messaging
{
    public class MessageSender : IMessageSender
    {
        private readonly ServiceBusSender _sender;

        private readonly string _topicName = "custom_event_bus";

        private const string INTEGRATION_EVENT_SUFFIX = "IntegrationEvent";

        public MessageSender(
            IServiceBusPersisterConnection serviceBusPersisterConnection)
        {
            _sender = serviceBusPersisterConnection.TopicClient.CreateSender(_topicName);
        }

        public async Task Publish(
            IntegrationEvent @event,
            CancellationToken cancellationToken)
        {
            var eventName = @event.GetType()
                .Name.Replace(
                    INTEGRATION_EVENT_SUFFIX,
                    "");

            var jsonMessage = JsonSerializer.Serialize(
                @event,
                @event.GetType());

            var body = Encoding.UTF8.GetBytes(jsonMessage);

            var message = new ServiceBusMessage
                {
                    MessageId = Guid.NewGuid()
                        .ToString(),
                    Body = new BinaryData(body),
                    Subject = eventName,
                };

            await _sender.SendMessageAsync(
                message,
                cancellationToken);
        }
    }
}
