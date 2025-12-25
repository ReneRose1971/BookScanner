using BookScanner.Application.UseCases;
using Microsoft.Extensions.DependencyInjection;

namespace BookScanner.Infrastructure.DependencyInjection;

public class ApplicationModule : IServiceModule
{
    public void RegisterServices(IServiceCollection services)
    {
        services.AddScoped<AddBookToListUseCase>();
        services.AddScoped<GetBooksInListUseCase>();
    }
}
