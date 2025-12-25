using BookScanner.Domain.Entities;
using BookScanner.Domain.Enums;

namespace BookScanner.Domain.Repositories;

public interface IBookListRepository
{
    Task<BookListEntry?> GetByIdAsync(Guid id);
    Task<IEnumerable<BookListEntry>> GetByListTypeAsync(BookListType listType);
    Task<IEnumerable<BookListEntry>> GetAllAsync();
    Task<BookListEntry> AddAsync(BookListEntry entry);
    Task<BookListEntry> UpdateAsync(BookListEntry entry);
    Task DeleteAsync(Guid id);
    Task<bool> IsBookInListAsync(Guid bookId, BookListType listType);
}
