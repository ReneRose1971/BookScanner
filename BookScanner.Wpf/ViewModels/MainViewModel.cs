using BookScanner.Application.UseCases;
using BookScanner.Domain.Entities;
using BookScanner.Domain.Enums;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace BookScanner.Wpf.ViewModels;

public partial class MainViewModel : ObservableObject
{
    private readonly AddBookToListUseCase _addBookToListUseCase;
    private readonly GetBooksInListUseCase _getBooksInListUseCase;

    [ObservableProperty]
    private ObservableCollection<BookWithEntry> _databaseBooks = [];

    [ObservableProperty]
    private ObservableCollection<BookWithEntry> _wishlistBooks = [];

    [ObservableProperty]
    private string _searchText = string.Empty;

    [ObservableProperty]
    private string _statusMessage = string.Empty;

    public MainViewModel(
        AddBookToListUseCase addBookToListUseCase,
        GetBooksInListUseCase getBooksInListUseCase)
    {
        _addBookToListUseCase = addBookToListUseCase;
        _getBooksInListUseCase = getBooksInListUseCase;
    }

    [RelayCommand]
    private async Task LoadBooksAsync()
    {
        var databaseResponse = await _getBooksInListUseCase.ExecuteAsync(
            new GetBooksInListRequest { ListType = BookListType.Database });
        var wishlistResponse = await _getBooksInListUseCase.ExecuteAsync(
            new GetBooksInListRequest { ListType = BookListType.Wishlist });

        DatabaseBooks = new ObservableCollection<BookWithEntry>(databaseResponse.Books);
        WishlistBooks = new ObservableCollection<BookWithEntry>(wishlistResponse.Books);

        StatusMessage = $"Loaded {DatabaseBooks.Count} books in database, {WishlistBooks.Count} in wishlist";
    }

    [RelayCommand]
    private async Task AddToDatabaseAsync()
    {
        await AddBookToListAsync(BookListType.Database);
    }

    [RelayCommand]
    private async Task AddToWishlistAsync()
    {
        await AddBookToListAsync(BookListType.Wishlist);
    }

    private async Task AddBookToListAsync(BookListType listType)
    {
        if (string.IsNullOrWhiteSpace(SearchText))
        {
            StatusMessage = "Please enter a book title or ISBN";
            return;
        }

        var book = new Book
        {
            Isbn = SearchText,
            Title = $"Book: {SearchText}",
            Author = "Demo Author",
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        var request = new AddBookToListRequest
        {
            Book = book,
            ListType = listType
        };

        var response = await _addBookToListUseCase.ExecuteAsync(request);
        StatusMessage = response.Message;

        if (response.Success)
        {
            SearchText = string.Empty;
            await LoadBooksAsync();
        }
    }
}
