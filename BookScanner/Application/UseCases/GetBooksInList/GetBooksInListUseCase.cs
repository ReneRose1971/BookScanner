using BookScanner.Domain.Repositories;

namespace BookScanner.Application.UseCases;

public class GetBooksInListUseCase
{
    private readonly IBookRepository _bookRepository;
    private readonly IBookListRepository _bookListRepository;

    public GetBooksInListUseCase(
        IBookRepository bookRepository,
        IBookListRepository bookListRepository)
    {
        _bookRepository = bookRepository;
        _bookListRepository = bookListRepository;
    }

    public async Task<GetBooksInListResponse> ExecuteAsync(GetBooksInListRequest request)
    {
        var entries = await _bookListRepository.GetByListTypeAsync(request.ListType);
        var books = new List<BookWithEntry>();

        foreach (var entry in entries)
        {
            var book = await _bookRepository.GetByIdAsync(entry.BookId);
            if (book != null)
            {
                books.Add(new BookWithEntry
                {
                    Book = book,
                    Entry = entry
                });
            }
        }

        return new GetBooksInListResponse
        {
            Books = books
        };
    }
}
