using System.IO;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

using Newtonsoft.Json;

using Microsoft.Extensions.Configuration;

using RaboBankTest.Application.Contracts;

using System;
using System.Threading;

using MediatR;

using RaboBankTest.Application.Feature;

using ExecutionContext = Microsoft.Azure.WebJobs.ExecutionContext;

namespace ProcessSqlData
{
    public class ProcessDataFunction
    {
        private readonly IMediator _mediator;

        public ProcessDataFunction(
            IMediator mediator)
        {
            _mediator = mediator;
        }

        [FunctionName("ProcessData")]
        public async Task Run(
            [TimerTrigger("0 */15 * * * *")] TimerInfo myTimer,
            ILogger log,
            ExecutionContext context)
        {
            log.LogInformation("Processing data...");

            try
            {
                var processDataRequest = new ProcessDataRequest()
                    {
                        ProcessDateTime = DateTime.UtcNow
                    };

                await _mediator.Send(
                    processDataRequest,
                    CancellationToken.None);
            }
            catch ( Exception ex )
            {
                log.LogError($"An error occurred: {ex}");

                throw;
            }

            log.LogInformation("Data processing completed.");
        }
    }
}
