using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Extensions.DependencyInjection;

using ProcessSqlData;

using RaboBankTest.Application;
using RaboBankTest.Data;
using RaboBankTest.Messaging;

[assembly: FunctionsStartup(typeof(Startup))]

namespace ProcessSqlData
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Environment.CurrentDirectory)
                .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();

            builder.Services.AddSingleton(configuration);

            builder.Services.SetupMessaging()
                .SetupApplication()
                .SetupData();
        }
    }
}
