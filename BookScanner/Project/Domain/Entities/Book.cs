using DataStores.Abstractions;

namespace BookScanner.Domain.Entities;

/// <summary>
/// Repräsentiert ein Buch mit bibliografischen Metadaten.
/// </summary>
/// <remarks>
/// Book erbt von EntityBase (DataStores) und erhält damit eine int-basierte Id.
/// Die fachliche Identität wird über Titel + Autor definiert (case-insensitive).
/// ISBN wird primär für externe API-Lookups verwendet, nicht für die Identität.
/// </remarks>
public class Book : EntityBase
{
    /// <summary>
    /// ISBN des Buches (optional).
    /// Wird primär für externe Buchsuchen (OpenLibrary, DNB, Google Books) verwendet.
    /// </summary>
    public string? ISBN { get; init; }

    /// <summary>
    /// Titel des Buches (erforderlich, Teil der fachlichen Identität).
    /// </summary>
    public required string Title { get; init; }

    /// <summary>
    /// Autor des Buches (optional, Teil der fachlichen Identität).
    /// </summary>
    public string? Author { get; init; }

    /// <summary>
    /// Verlag des Buches (optional).
    /// </summary>
    public string? Publisher { get; init; }

    /// <summary>
    /// Erscheinungsjahr (optional).
    /// </summary>
    public int? Year { get; init; }

    /// <summary>
    /// URL zum Buchcover (optional).
    /// </summary>
    public string? CoverUrl { get; init; }

    /// <summary>
    /// Frei definierbare Tags zur inhaltlichen Beschreibung (Genre, Thema, etc.).
    /// </summary>
    public List<string> Tags { get; init; } = new();

    /// <summary>
    /// Gibt den Titel des Buches zurück (für Debugging und UI-Anzeige).
    /// </summary>
    public override string ToString() => Title;

    /// <summary>
    /// Vergleicht dieses Buch mit einem anderen Objekt basierend auf fachlicher Identität (Titel + Autor).
    /// </summary>
    /// <remarks>
    /// Die fachliche Identität basiert auf Titel und Autor (case-insensitive), NICHT auf der Id.
    /// Dies ist wichtig, da die Id vor Persistierung 0 ist und sich nach DB-Insert ändert.
    /// Zwei Bücher mit gleichem Titel und Autor gelten als identisch, auch wenn andere Properties unterschiedlich sind.
    /// </remarks>
    public override bool Equals(object? obj)
    {
        if (obj is not Book other) return false;
        if (ReferenceEquals(this, other)) return true;

        return string.Equals(Title, other.Title, StringComparison.OrdinalIgnoreCase)
            && string.Equals(Author, other.Author, StringComparison.OrdinalIgnoreCase);
    }

    /// <summary>
    /// Gibt den HashCode basierend auf fachlicher Identität (Titel + Autor) zurück.
    /// </summary>
    /// <remarks>
    /// Der HashCode basiert auf Titel und Autor (case-insensitive), NICHT auf der Id.
    /// Dies stellt sicher, dass der HashCode stabil bleibt, auch vor Persistierung (Id = 0).
    /// </remarks>
    public override int GetHashCode()
    {
        return HashCode.Combine(
            Title?.ToUpperInvariant(),
            Author?.ToUpperInvariant()
        );
    }
}
