using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AutoMapper;

using MediatR;

using RaboBankTest.Application.Contracts;
using RaboBankTest.Domain.Message;

namespace RaboBankTest.Application.Feature
{
    public class ProcessDataRequestHandler : IRequestHandler<ProcessDataRequest>
    {
        private readonly IDataProcessor _dataProcessor;

        private readonly IMessageSender _messageSender;

        private readonly IMapper _mapper;

        public ProcessDataRequestHandler(
            IDataProcessor dataProcessor,
            IMessageSender messageSender,
            IMapper mapper)
        {
            _dataProcessor = dataProcessor;
            _messageSender = messageSender;
            _mapper = mapper;
        }

        public async Task Handle(
            ProcessDataRequest request,
            CancellationToken cancellationToken)
        {
            var processDataAsync = await _dataProcessor.ProcessDataAsync(
                request.ProcessDateTime,
                cancellationToken);

            foreach ( var data in processDataAsync )
            {
                var @event = _mapper.Map<CustomModelNotification>(data);

                await _messageSender.Publish(
                    @event,
                    cancellationToken);
            }
        }
    }
}
