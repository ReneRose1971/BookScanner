using BookScanner.Domain.Repositories;

namespace BookScanner.Application.UseCases;

public class AddBookToListUseCase
{
    private readonly IBookRepository _bookRepository;
    private readonly IBookListRepository _bookListRepository;

    public AddBookToListUseCase(
        IBookRepository bookRepository,
        IBookListRepository bookListRepository)
    {
        _bookRepository = bookRepository;
        _bookListRepository = bookListRepository;
    }

    public async Task<AddBookToListResponse> ExecuteAsync(AddBookToListRequest request)
    {
        var existingBook = await _bookRepository.GetByIsbnAsync(request.Book.Isbn);
        var book = existingBook ?? await _bookRepository.AddAsync(request.Book);

        var alreadyInList = await _bookListRepository.IsBookInListAsync(book.Id, request.ListType);
        if (alreadyInList)
        {
            return new AddBookToListResponse
            {
                Success = false,
                Message = $"Book is already in {request.ListType}"
            };
        }

        var entry = new Domain.Entities.BookListEntry
        {
            Id = Guid.NewGuid(),
            BookId = book.Id,
            ListType = request.ListType,
            AddedAt = DateTime.UtcNow,
            Notes = request.Notes
        };

        var addedEntry = await _bookListRepository.AddAsync(entry);

        return new AddBookToListResponse
        {
            Success = true,
            Message = "Book added successfully",
            Entry = addedEntry
        };
    }
}
