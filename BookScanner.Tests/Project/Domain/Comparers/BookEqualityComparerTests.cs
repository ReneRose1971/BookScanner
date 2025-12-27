using BookScanner.Domain.Comparers;
using BookScanner.Domain.Entities;

namespace BookScanner.Tests.Domain.Comparers;

/// <summary>
/// Tests für den BookEqualityComparer.
/// </summary>
public class BookEqualityComparerTests
{
    private readonly BookEqualityComparer _comparer = new();

    [Fact]
    public void Equals_ReturnsTrueForSameReference()
    {
        // Arrange
        var book = new Book { Title = "Test Book", Author = "Test Author" };

        // Act
        var result = _comparer.Equals(book, book);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void Equals_ReturnsTrueForSameTitleAndAuthor()
    {
        // Arrange
        var book1 = new Book { Title = "Clean Code", Author = "Robert C. Martin" };
        var book2 = new Book { Title = "Clean Code", Author = "Robert C. Martin" };

        // Act
        var result = _comparer.Equals(book1, book2);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void Equals_IsCaseInsensitive()
    {
        // Arrange
        var book1 = new Book { Title = "Clean Code", Author = "Robert C. Martin" };
        var book2 = new Book { Title = "CLEAN CODE", Author = "ROBERT C. MARTIN" };

        // Act
        var result = _comparer.Equals(book1, book2);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void Equals_ReturnsFalseForDifferentTitle()
    {
        // Arrange
        var book1 = new Book { Title = "Clean Code", Author = "Robert C. Martin" };
        var book2 = new Book { Title = "The Clean Coder", Author = "Robert C. Martin" };

        // Act
        var result = _comparer.Equals(book1, book2);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void Equals_ReturnsFalseForDifferentAuthor()
    {
        // Arrange
        var book1 = new Book { Title = "Clean Code", Author = "Robert C. Martin" };
        var book2 = new Book { Title = "Clean Code", Author = "Martin Fowler" };

        // Act
        var result = _comparer.Equals(book1, book2);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void Equals_ReturnsTrueForBothNullAuthors()
    {
        // Arrange
        var book1 = new Book { Title = "Clean Code", Author = null };
        var book2 = new Book { Title = "Clean Code", Author = null };

        // Act
        var result = _comparer.Equals(book1, book2);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void Equals_ReturnsFalseForOneNullAuthor()
    {
        // Arrange
        var book1 = new Book { Title = "Clean Code", Author = "Robert C. Martin" };
        var book2 = new Book { Title = "Clean Code", Author = null };

        // Act
        var result = _comparer.Equals(book1, book2);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void Equals_ReturnsFalseForNullFirstArgument()
    {
        // Arrange
        var book = new Book { Title = "Test Book" };

        // Act
        var result = _comparer.Equals(null, book);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void Equals_ReturnsFalseForNullSecondArgument()
    {
        // Arrange
        var book = new Book { Title = "Test Book" };

        // Act
        var result = _comparer.Equals(book, null);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void Equals_ReturnsTrueForBothNull()
    {
        // Act
        var result = _comparer.Equals(null, null);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void Equals_IgnoresOtherProperties()
    {
        // Arrange
        var book1 = new Book
        {
            Title = "Clean Code",
            Author = "Robert C. Martin",
            ISBN = "978-0132350884",
            Publisher = "Prentice Hall",
            Year = 2008
        };
        var book2 = new Book
        {
            Title = "Clean Code",
            Author = "Robert C. Martin",
            ISBN = "different-isbn",
            Publisher = "Different Publisher",
            Year = 2020
        };

        // Act
        var result = _comparer.Equals(book1, book2);

        // Assert
        Assert.True(result); // Nur Titel + Autor zählen
    }

    [Fact]
    public void GetHashCode_IsSameForEqualBooks()
    {
        // Arrange
        var book1 = new Book { Title = "Clean Code", Author = "Robert C. Martin" };
        var book2 = new Book { Title = "Clean Code", Author = "Robert C. Martin" };

        // Act
        var hash1 = _comparer.GetHashCode(book1);
        var hash2 = _comparer.GetHashCode(book2);

        // Assert
        Assert.Equal(hash1, hash2);
    }

    [Fact]
    public void GetHashCode_IsCaseInsensitive()
    {
        // Arrange
        var book1 = new Book { Title = "Clean Code", Author = "Robert C. Martin" };
        var book2 = new Book { Title = "CLEAN CODE", Author = "ROBERT C. MARTIN" };

        // Act
        var hash1 = _comparer.GetHashCode(book1);
        var hash2 = _comparer.GetHashCode(book2);

        // Assert
        Assert.Equal(hash1, hash2);
    }

    [Fact]
    public void GetHashCode_ThrowsForNull()
    {
        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => _comparer.GetHashCode(null!));
    }

    [Fact]
    public void GetHashCode_HandlesNullAuthor()
    {
        // Arrange
        var book = new Book { Title = "Clean Code", Author = null };

        // Act
        var hash = _comparer.GetHashCode(book);

        // Assert
        Assert.NotEqual(0, hash); // HashCode sollte berechenbar sein
    }
}
