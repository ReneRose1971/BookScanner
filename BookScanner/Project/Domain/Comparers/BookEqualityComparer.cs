using BookScanner.Domain.Entities;

namespace BookScanner.Domain.Comparers;

/// <summary>
/// Vergleicht Bücher basierend auf ihrer fachlichen Identität (Titel + Autor).
/// </summary>
/// <remarks>
/// <para>
/// Dieser Comparer delegiert an die Equals/GetHashCode-Implementierung der Book-Klasse.
/// Die fachliche Identität wird durch Titel und Autor definiert (case-insensitive).
/// </para>
/// <para>
/// Verwendung mit InMemoryDataStore:
/// <code>
/// var dataStore = new InMemoryDataStore&lt;Book&gt;(new BookEqualityComparer());
/// </code>
/// </para>
/// <para>
/// <b>Warum delegieren?</b><br/>
/// - Book.Equals/GetHashCode enthalten die primäre Logik<br/>
/// - Comparer ist für DataStore-Integration (IEqualityComparer&lt;T&gt; benötigt)<br/>
/// - Vermeidet Code-Duplizierung<br/>
/// - Konsistenz: Gleiche Logik überall
/// </para>
/// </remarks>
public class BookEqualityComparer : IEqualityComparer<Book>
{
    /// <summary>
    /// Vergleicht zwei Bücher auf fachliche Gleichheit (delegiert an Book.Equals).
    /// </summary>
    public bool Equals(Book? x, Book? y)
    {
        if (ReferenceEquals(x, y)) return true;
        if (x is null || y is null) return false;

        return x.Equals(y);
    }

    /// <summary>
    /// Berechnet den HashCode (delegiert an Book.GetHashCode).
    /// </summary>
    public int GetHashCode(Book obj)
    {
        if (obj is null) throw new ArgumentNullException(nameof(obj));

        return obj.GetHashCode();
    }
}
