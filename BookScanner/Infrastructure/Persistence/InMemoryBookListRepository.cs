using BookScanner.Domain.Entities;
using BookScanner.Domain.Enums;
using BookScanner.Domain.Repositories;

namespace BookScanner.Infrastructure.Persistence;

public class InMemoryBookListRepository : IBookListRepository
{
    private readonly List<BookListEntry> _entries = [];

    public Task<BookListEntry?> GetByIdAsync(Guid id)
    {
        var entry = _entries.FirstOrDefault(e => e.Id == id);
        return Task.FromResult(entry);
    }

    public Task<IEnumerable<BookListEntry>> GetByListTypeAsync(BookListType listType)
    {
        var entries = _entries.Where(e => e.ListType == listType);
        return Task.FromResult(entries);
    }

    public Task<IEnumerable<BookListEntry>> GetAllAsync()
    {
        return Task.FromResult<IEnumerable<BookListEntry>>(_entries);
    }

    public Task<BookListEntry> AddAsync(BookListEntry entry)
    {
        if (entry.Id == Guid.Empty)
            entry.Id = Guid.NewGuid();
        
        entry.AddedAt = DateTime.UtcNow;
        _entries.Add(entry);
        return Task.FromResult(entry);
    }

    public Task<BookListEntry> UpdateAsync(BookListEntry entry)
    {
        var existing = _entries.FirstOrDefault(e => e.Id == entry.Id);
        if (existing != null)
        {
            _entries.Remove(existing);
            _entries.Add(entry);
        }
        return Task.FromResult(entry);
    }

    public Task DeleteAsync(Guid id)
    {
        var entry = _entries.FirstOrDefault(e => e.Id == id);
        if (entry != null)
            _entries.Remove(entry);
        return Task.CompletedTask;
    }

    public Task<bool> IsBookInListAsync(Guid bookId, BookListType listType)
    {
        var exists = _entries.Any(e => e.BookId == bookId && e.ListType == listType);
        return Task.FromResult(exists);
    }
}
