using BookScanner.Domain.Entities;
using BookScanner.Domain.Repositories;

namespace BookScanner.Infrastructure.Persistence;

public class InMemoryBookRepository : IBookRepository
{
    private readonly List<Book> _books = [];

    public Task<Book?> GetByIdAsync(Guid id)
    {
        var book = _books.FirstOrDefault(b => b.Id == id);
        return Task.FromResult(book);
    }

    public Task<Book?> GetByIsbnAsync(string isbn)
    {
        var book = _books.FirstOrDefault(b => b.Isbn == isbn);
        return Task.FromResult(book);
    }

    public Task<IEnumerable<Book>> GetAllAsync()
    {
        return Task.FromResult<IEnumerable<Book>>(_books);
    }

    public Task<IEnumerable<Book>> SearchAsync(string searchTerm)
    {
        var results = _books.Where(b =>
            b.Title.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
            b.Author.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
            b.Isbn.Contains(searchTerm, StringComparison.OrdinalIgnoreCase));
        return Task.FromResult(results);
    }

    public Task<Book> AddAsync(Book book)
    {
        if (book.Id == Guid.Empty)
            book.Id = Guid.NewGuid();
        
        book.CreatedAt = DateTime.UtcNow;
        book.UpdatedAt = DateTime.UtcNow;
        _books.Add(book);
        return Task.FromResult(book);
    }

    public Task<Book> UpdateAsync(Book book)
    {
        var existing = _books.FirstOrDefault(b => b.Id == book.Id);
        if (existing != null)
        {
            _books.Remove(existing);
            book.UpdatedAt = DateTime.UtcNow;
            _books.Add(book);
        }
        return Task.FromResult(book);
    }

    public Task DeleteAsync(Guid id)
    {
        var book = _books.FirstOrDefault(b => b.Id == id);
        if (book != null)
            _books.Remove(book);
        return Task.CompletedTask;
    }
}
