using BookScanner.Domain.Entities;

namespace BookScanner.Domain.Repositories;

public interface IBookRepository
{
    Task<Book?> GetByIdAsync(Guid id);
    Task<Book?> GetByIsbnAsync(string isbn);
    Task<IEnumerable<Book>> GetAllAsync();
    Task<IEnumerable<Book>> SearchAsync(string searchTerm);
    Task<Book> AddAsync(Book book);
    Task<Book> UpdateAsync(Book book);
    Task DeleteAsync(Guid id);
}
