using BookScanner.Application.UseCases;
using BookScanner.Domain.Entities;
using BookScanner.Domain.Enums;
using BookScanner.Infrastructure.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

namespace BookScanner.Tests.UseCases;

public class GetBooksInListUseCaseTests
{
    private readonly ServiceProvider _serviceProvider;

    public GetBooksInListUseCaseTests()
    {
        var services = new ServiceCollection();
        services.AddBookScannerServices();
        _serviceProvider = services.BuildServiceProvider();
    }

    [Fact]
    public async Task ExecuteAsync_ReturnsEmptyList_WhenNoBooks()
    {
        var useCase = _serviceProvider.GetRequiredService<GetBooksInListUseCase>();
        var request = new GetBooksInListRequest { ListType = BookListType.Database };

        var response = await useCase.ExecuteAsync(request);

        Assert.NotNull(response.Books);
        Assert.Empty(response.Books);
    }

    [Fact]
    public async Task ExecuteAsync_ReturnsBooksInList_AfterAdding()
    {
        var addUseCase = _serviceProvider.GetRequiredService<AddBookToListUseCase>();
        var getUseCase = _serviceProvider.GetRequiredService<GetBooksInListUseCase>();

        var book1 = new Book { Isbn = "111", Title = "Book 1", Author = "Author 1" };
        var book2 = new Book { Isbn = "222", Title = "Book 2", Author = "Author 2" };

        await addUseCase.ExecuteAsync(new AddBookToListRequest { Book = book1, ListType = BookListType.Database });
        await addUseCase.ExecuteAsync(new AddBookToListRequest { Book = book2, ListType = BookListType.Database });

        var response = await getUseCase.ExecuteAsync(new GetBooksInListRequest { ListType = BookListType.Database });

        Assert.Equal(2, response.Books.Count());
    }

    [Fact]
    public async Task ExecuteAsync_FiltersByListType_Correctly()
    {
        var addUseCase = _serviceProvider.GetRequiredService<AddBookToListUseCase>();
        var getUseCase = _serviceProvider.GetRequiredService<GetBooksInListUseCase>();

        var book1 = new Book { Isbn = "333", Title = "Book 3", Author = "Author 3" };
        var book2 = new Book { Isbn = "444", Title = "Book 4", Author = "Author 4" };

        await addUseCase.ExecuteAsync(new AddBookToListRequest { Book = book1, ListType = BookListType.Database });
        await addUseCase.ExecuteAsync(new AddBookToListRequest { Book = book2, ListType = BookListType.Wishlist });

        var databaseResponse = await getUseCase.ExecuteAsync(new GetBooksInListRequest { ListType = BookListType.Database });
        var wishlistResponse = await getUseCase.ExecuteAsync(new GetBooksInListRequest { ListType = BookListType.Wishlist });

        Assert.Single(databaseResponse.Books);
        Assert.Single(wishlistResponse.Books);
        Assert.Equal("Book 3", databaseResponse.Books.First().Book.Title);
        Assert.Equal("Book 4", wishlistResponse.Books.First().Book.Title);
    }
}
