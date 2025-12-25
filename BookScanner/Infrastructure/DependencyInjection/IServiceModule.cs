using Microsoft.Extensions.DependencyInjection;

namespace BookScanner.Infrastructure.DependencyInjection;

public interface IServiceModule
{
    void RegisterServices(IServiceCollection services);
}
