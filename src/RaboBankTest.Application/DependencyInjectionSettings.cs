using Microsoft.Extensions.DependencyInjection;

using System.Reflection;

namespace RaboBankTest.Application
{
    public static class DependencyInjectionSettings
    {
        public static IServiceCollection SetupApplication(
            this IServiceCollection serviceCollection)
        {
            serviceCollection.AddAutoMapper(Assembly.GetExecutingAssembly());

            serviceCollection.AddMediatR(
                cfg =>
                    cfg.RegisterServicesFromAssembly(typeof(DependencyInjectionSettings).Assembly));

            return serviceCollection;
        }
    }
}
