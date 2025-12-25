using BookScanner.Application.UseCases;
using BookScanner.Domain.Entities;
using BookScanner.Domain.Enums;
using BookScanner.Infrastructure.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

namespace BookScanner.Tests.UseCases;

public class AddBookToListUseCaseTests
{
    private readonly ServiceProvider _serviceProvider;

    public AddBookToListUseCaseTests()
    {
        var services = new ServiceCollection();
        services.AddBookScannerServices();
        _serviceProvider = services.BuildServiceProvider();
    }

    [Fact]
    public async Task ExecuteAsync_AddsBookToDatabase_Successfully()
    {
        var useCase = _serviceProvider.GetRequiredService<AddBookToListUseCase>();
        var request = new AddBookToListRequest
        {
            Book = new Book
            {
                Isbn = "978-3-16-148410-0",
                Title = "Test Book",
                Author = "Test Author"
            },
            ListType = BookListType.Database
        };

        var response = await useCase.ExecuteAsync(request);

        Assert.True(response.Success);
        Assert.NotNull(response.Entry);
        Assert.Equal(BookListType.Database, response.Entry.ListType);
    }

    [Fact]
    public async Task ExecuteAsync_AddsSameBookTwice_ReturnsError()
    {
        var useCase = _serviceProvider.GetRequiredService<AddBookToListUseCase>();
        var book = new Book
        {
            Isbn = "978-3-16-148410-1",
            Title = "Test Book 2",
            Author = "Test Author 2"
        };

        var request1 = new AddBookToListRequest { Book = book, ListType = BookListType.Wishlist };
        await useCase.ExecuteAsync(request1);

        var request2 = new AddBookToListRequest { Book = book, ListType = BookListType.Wishlist };
        var response2 = await useCase.ExecuteAsync(request2);

        Assert.False(response2.Success);
        Assert.Contains("already in", response2.Message);
    }
}
