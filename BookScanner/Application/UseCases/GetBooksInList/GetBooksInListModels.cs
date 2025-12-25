using BookScanner.Domain.Entities;
using BookScanner.Domain.Enums;

namespace BookScanner.Application.UseCases;

public class GetBooksInListRequest
{
    public BookListType ListType { get; set; }
}

public class GetBooksInListResponse
{
    public IEnumerable<BookWithEntry> Books { get; set; } = [];
}

public class BookWithEntry
{
    public Book Book { get; set; } = null!;
    public BookListEntry Entry { get; set; } = null!;
}
