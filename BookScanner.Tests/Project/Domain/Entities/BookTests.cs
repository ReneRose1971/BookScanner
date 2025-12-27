using BookScanner.Domain.Entities;

namespace BookScanner.Tests.Domain.Entities;

/// <summary>
/// Tests für die Book Entity.
/// </summary>
public class BookTests
{
    [Fact]
    public void Book_CanBeCreated_WithRequiredProperties()
    {
        // Arrange & Act
        var book = new Book
        {
            Title = "Clean Code"
        };

        // Assert
        Assert.NotNull(book);
        Assert.Equal("Clean Code", book.Title);
    }

    [Fact]
    public void Book_CanBeCreated_WithAllProperties()
    {
        // Arrange & Act
        var book = new Book
        {
            ISBN = "978-0132350884",
            Title = "Clean Code",
            Author = "Robert C. Martin",
            Publisher = "Prentice Hall",
            Year = 2008,
            CoverUrl = "https://example.com/cover.jpg",
            Tags = new List<string> { "Programming", "Best Practices" }
        };

        // Assert
        Assert.Equal("978-0132350884", book.ISBN);
        Assert.Equal("Clean Code", book.Title);
        Assert.Equal("Robert C. Martin", book.Author);
        Assert.Equal("Prentice Hall", book.Publisher);
        Assert.Equal(2008, book.Year);
        Assert.Equal("https://example.com/cover.jpg", book.CoverUrl);
        Assert.Contains("Programming", book.Tags);
        Assert.Contains("Best Practices", book.Tags);
    }

    [Fact]
    public void Book_CanBeCreated_WithoutOptionalProperties()
    {
        // Arrange & Act
        var book = new Book
        {
            Title = "Some Book"
        };

        // Assert
        Assert.Null(book.ISBN);
        Assert.Null(book.Author);
        Assert.Null(book.Publisher);
        Assert.Null(book.Year);
        Assert.Null(book.CoverUrl);
        Assert.Empty(book.Tags);
    }

    [Fact]
    public void Book_ToString_ReturnsTitle()
    {
        // Arrange
        var book = new Book
        {
            Title = "The Pragmatic Programmer"
        };

        // Act
        var result = book.ToString();

        // Assert
        Assert.Equal("The Pragmatic Programmer", result);
    }

    [Fact]
    public void Book_Tags_DefaultsToEmptyList()
    {
        // Arrange & Act
        var book = new Book
        {
            Title = "Test Book"
        };

        // Assert
        Assert.NotNull(book.Tags);
        Assert.Empty(book.Tags);
    }

    [Fact]
    public void Book_InheritsFromEntityBase()
    {
        // Arrange & Act
        var book = new Book
        {
            Title = "Test Book"
        };

        // Assert
        Assert.IsAssignableFrom<DataStores.Abstractions.EntityBase>(book);
    }

    [Fact]
    public void Book_HasIdProperty_FromEntityBase()
    {
        // Arrange
        var book = new Book
        {
            Title = "Test Book",
            Id = 42
        };

        // Act & Assert
        Assert.Equal(42, book.Id);
    }

    [Fact]
    public void Book_Equals_ReturnsTrueForSameTitleAndAuthor()
    {
        // Arrange
        var book1 = new Book { Title = "Clean Code", Author = "Robert C. Martin", Id = 0 };
        var book2 = new Book { Title = "Clean Code", Author = "Robert C. Martin", Id = 0 };

        // Act & Assert
        Assert.True(book1.Equals(book2));
    }

    [Fact]
    public void Book_Equals_IsCaseInsensitive()
    {
        // Arrange
        var book1 = new Book { Title = "Clean Code", Author = "Robert C. Martin" };
        var book2 = new Book { Title = "CLEAN CODE", Author = "ROBERT C. MARTIN" };

        // Act & Assert
        Assert.True(book1.Equals(book2));
    }

    [Fact]
    public void Book_Equals_IgnoresId()
    {
        // Arrange
        var book1 = new Book { Title = "Clean Code", Author = "Robert C. Martin", Id = 1 };
        var book2 = new Book { Title = "Clean Code", Author = "Robert C. Martin", Id = 999 };

        // Act & Assert
        Assert.True(book1.Equals(book2)); // Gleicher Titel+Autor ? gleich, trotz verschiedener Ids
    }

    [Fact]
    public void Book_Equals_WorksBeforePersistence()
    {
        // Arrange - beide Bücher haben Id = 0 (vor DB-Insert)
        var book1 = new Book { Title = "Clean Code", Author = "Robert C. Martin" };
        var book2 = new Book { Title = "Clean Code", Author = "Robert C. Martin" };

        // Act & Assert
        Assert.Equal(0, book1.Id); // Vor Persistierung
        Assert.Equal(0, book2.Id);
        Assert.True(book1.Equals(book2)); // Trotzdem gleich (fachliche Identität)
    }

    [Fact]
    public void Book_GetHashCode_IsStableBeforePersistence()
    {
        // Arrange
        var book = new Book { Title = "Clean Code", Author = "Robert C. Martin" };
        var hashBeforePersist = book.GetHashCode();

        // Act - Simuliere DB-Insert
        book.Id = 42;
        var hashAfterPersist = book.GetHashCode();

        // Assert - HashCode bleibt gleich (wichtig für HashSets/Dictionaries!)
        Assert.Equal(hashBeforePersist, hashAfterPersist);
    }

    [Fact]
    public void Book_GetHashCode_IsSameForEqualBooks()
    {
        // Arrange
        var book1 = new Book { Title = "Clean Code", Author = "Robert C. Martin" };
        var book2 = new Book { Title = "CLEAN CODE", Author = "ROBERT C. MARTIN" };

        // Act & Assert
        Assert.Equal(book1.GetHashCode(), book2.GetHashCode());
    }
}
