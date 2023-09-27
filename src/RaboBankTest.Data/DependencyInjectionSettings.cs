using Microsoft.Extensions.DependencyInjection;

using RaboBankTest.Application.Contracts;

namespace RaboBankTest.Data
{
    public static class DependencyInjectionSettings
    {
        public static IServiceCollection SetupData(
            this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IDataProcessor, DataProcessor>();

            return serviceCollection;
        }
    }
}
