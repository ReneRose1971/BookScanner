using Microsoft.Extensions.DependencyInjection;

namespace BookScanner.Infrastructure.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddBookScannerServices(this IServiceCollection services)
    {
        var modules = new IServiceModule[]
        {
            new PersistenceModule(),
            new ApplicationModule()
        };

        foreach (var module in modules)
        {
            module.RegisterServices(services);
        }

        return services;
    }
}
