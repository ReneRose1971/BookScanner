using BookScanner.Domain.Enums;

namespace BookScanner.Domain.Entities;

public class BookListEntry
{
    public Guid Id { get; set; }
    public Guid BookId { get; set; }
    public BookListType ListType { get; set; }
    public DateTime AddedAt { get; set; }
    public string Notes { get; set; } = string.Empty;
}
