using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RaboBankTest.Domain.Message;

namespace RaboBankTest.Application.Contracts
{
    public interface IMessageSender
    {
        Task Publish(
            IntegrationEvent @event,
            CancellationToken cancellationToken);
    }
}
