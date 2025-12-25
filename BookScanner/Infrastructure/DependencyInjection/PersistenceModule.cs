using BookScanner.Domain.Repositories;
using BookScanner.Infrastructure.Persistence;
using Microsoft.Extensions.DependencyInjection;

namespace BookScanner.Infrastructure.DependencyInjection;

public class PersistenceModule : IServiceModule
{
    public void RegisterServices(IServiceCollection services)
    {
        services.AddSingleton<IBookRepository, InMemoryBookRepository>();
        services.AddSingleton<IBookListRepository, InMemoryBookListRepository>();
    }
}
