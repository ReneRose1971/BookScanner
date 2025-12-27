using BookScanner.Domain.Entities;
using DataStores.Abstractions;

namespace BookScanner.Tests.Bootstrap;

/// <summary>
/// Tests für BookScannerDataStoreRegistrar.
/// </summary>
/// <remarks>
/// Diese Tests verwenden eine gemeinsame Fixture (BookScannerDataStoreFixture).
/// Durch [Collection] werden alle Tests SEQUENZIELL ausgeführt, um Race Conditions zu vermeiden.
/// Jeder Test räumt am Ende seinen Zustand auf (bookStore.Clear()).
/// </remarks>
[Collection("BookScanner DataStore Collection")]
public class BookScannerDataStoreRegistrarTests
{
    private readonly BookScannerDataStoreFixture _fixture;

    public BookScannerDataStoreRegistrarTests(BookScannerDataStoreFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task Registrar_CreatesDatabase_AtSpecifiedPath()
    {
        // Arrange - Fixture hat bereits den Registrar initialisiert
        var bookStore = _fixture.DataStores.GetGlobal<Book>();

        try
        {
            // Act - Zugriff auf Book-Store triggert DB-Erstellung
            bookStore.Add(new Book { Title = "Test Book", Author = "Test Author" });
            
            // Warte kurz, damit AutoSave die DB schreiben kann
            await Task.Delay(100);

            // Assert - Datenbankdatei sollte existieren
            Assert.True(File.Exists(_fixture.DatabasePath), 
                $"Database file should exist at {_fixture.DatabasePath}");
        }
        finally
        {
            // Cleanup - Store für nächsten Test leeren
            bookStore.Clear();
        }
    }

    [Fact]
    public void Registrar_RegistersBookStore_InGlobalRegistry()
    {
        // Act
        var bookStore = _fixture.DataStores.GetGlobal<Book>();

        // Assert
        Assert.NotNull(bookStore);
        Assert.IsAssignableFrom<IDataStore<Book>>(bookStore);
    }

    [Fact]
    public void BookStore_CanAddAndRetrieveBooks()
    {
        // Arrange
        var bookStore = _fixture.DataStores.GetGlobal<Book>();
        
        try
        {
            var testBook = new Book 
            { 
                Title = "Clean Code", 
                Author = "Robert C. Martin",
                ISBN = "978-0132350884",
                Publisher = "Prentice Hall",
                Year = 2008
            };

            // Act
            bookStore.Add(testBook);
            var allBooks = bookStore.Items;

            // Assert
            Assert.Single(allBooks);
            Assert.Equal("Clean Code", allBooks[0].Title);
            Assert.Equal("Robert C. Martin", allBooks[0].Author);
        }
        finally
        {
            bookStore.Clear();
        }
    }

    [Fact]
    public void BookStore_AllowsDuplicates_DataStoreIsCollection()
    {
        // Arrange
        var bookStore = _fixture.DataStores.GetGlobal<Book>();
        
        try
        {
            var book1 = new Book { Title = "Clean Code", Author = "Robert C. Martin" };
            var book2 = new Book { Title = "Clean Code", Author = "Robert C. Martin" }; // Gleiches Buch

            // Act
            bookStore.Add(book1);
            bookStore.Add(book2); // DataStore ist eine Collection, kein Set - Duplikate erlaubt

            var allBooks = bookStore.Items;

            // Assert
            Assert.Equal(2, allBooks.Count); // Beide Bücher sind im Store (Duplikate erlaubt)
        }
        finally
        {
            bookStore.Clear();
        }
    }

    [Fact]
    public void BookStore_Contains_UsesFachlicheIdentitaet()
    {
        // Arrange
        var bookStore = _fixture.DataStores.GetGlobal<Book>();
        
        try
        {
            var book1 = new Book { Title = "Clean Code", Author = "Robert C. Martin" };
            var book2 = new Book { Title = "Clean Code", Author = "Robert C. Martin" }; // Gleiches Buch, andere Instanz

            // Act
            bookStore.Add(book1);

            // Assert - BookEqualityComparer wird für Contains() verwendet
            Assert.True(bookStore.Contains(book2)); // book2 wird als gleich erkannt (Titel + Autor)
        }
        finally
        {
            bookStore.Clear();
        }
    }

    [Fact]
    public void BookStore_Contains_IsCaseInsensitive()
    {
        // Arrange
        var bookStore = _fixture.DataStores.GetGlobal<Book>();
        
        try
        {
            var book1 = new Book { Title = "Clean Code", Author = "Robert C. Martin" };
            var book2 = new Book { Title = "CLEAN CODE", Author = "ROBERT C. MARTIN" }; // Gleich, nur andere Schreibweise

            // Act
            bookStore.Add(book1);

            // Assert - BookEqualityComparer ist case-insensitive
            Assert.True(bookStore.Contains(book2)); // Case-insensitive Vergleich
        }
        finally
        {
            bookStore.Clear();
        }
    }

    [Fact]
    public async Task BookStore_PersistsData_ToLiteDB()
    {
        // Arrange
        var bookStore = _fixture.DataStores.GetGlobal<Book>();
        
        try
        {
            var testBook = new Book 
            { 
                Title = "The Pragmatic Programmer", 
                Author = "Andy Hunt",
                Tags = new List<string> { "Programming", "Career" }
            };

            // Act
            bookStore.Add(testBook);
            
            // Warte kurz, damit AutoSave die DB schreiben kann
            await Task.Delay(100);

            // Assert - Datei sollte Daten enthalten (Größe > 0)
            Assert.True(File.Exists(_fixture.DatabasePath), "Database file should exist");
            var fileInfo = new FileInfo(_fixture.DatabasePath);
            Assert.True(fileInfo.Length > 0, "Database file should contain data");
        }
        finally
        {
            bookStore.Clear();
        }
    }

    [Fact]
    public void BookStore_CanRemoveBooks()
    {
        // Arrange
        var bookStore = _fixture.DataStores.GetGlobal<Book>();
        
        try
        {
            var testBook = new Book { Title = "Test Book", Author = "Test Author" };
            bookStore.Add(testBook);

            // Act
            var removed = bookStore.Remove(testBook);

            // Assert
            Assert.True(removed);
            Assert.Empty(bookStore.Items);
        }
        finally
        {
            bookStore.Clear();
        }
    }

    [Fact]
    public void BookStore_Remove_UsesFachlicheIdentitaet()
    {
        // Arrange
        var bookStore = _fixture.DataStores.GetGlobal<Book>();
        
        try
        {
            var book1 = new Book { Title = "Clean Code", Author = "Robert C. Martin" };
            var book2 = new Book { Title = "Clean Code", Author = "Robert C. Martin" }; // Gleiche fachliche Identität
            
            bookStore.Add(book1);

            // Act - Remove mit anderer Instanz, aber gleicher fachlicher Identität
            var removed = bookStore.Remove(book2);

            // Assert - BookEqualityComparer erkennt fachliche Gleichheit
            Assert.True(removed);
            Assert.Empty(bookStore.Items);
        }
        finally
        {
            bookStore.Clear();
        }
    }

    [Fact]
    public void BookStore_CanClearAllBooks()
    {
        // Arrange
        var bookStore = _fixture.DataStores.GetGlobal<Book>();
        
        try
        {
            bookStore.Add(new Book { Title = "Book 1", Author = "Author 1" });
            bookStore.Add(new Book { Title = "Book 2", Author = "Author 2" });
            bookStore.Add(new Book { Title = "Book 3", Author = "Author 3" });

            // Act
            bookStore.Clear();

            // Assert
            Assert.Empty(bookStore.Items);
        }
        finally
        {
            // Bereits gecleart, aber sicherheitshalber
            bookStore.Clear();
        }
    }
}
