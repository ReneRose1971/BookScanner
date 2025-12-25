using BookScanner.Domain.Entities;
using BookScanner.Domain.Enums;

namespace BookScanner.Application.UseCases;

public class AddBookToListRequest
{
    public Book Book { get; set; } = null!;
    public BookListType ListType { get; set; }
    public string Notes { get; set; } = string.Empty;
}

public class AddBookToListResponse
{
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
    public BookListEntry? Entry { get; set; }
}
