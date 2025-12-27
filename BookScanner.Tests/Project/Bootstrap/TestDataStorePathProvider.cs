using DataStores.Bootstrap;

namespace BookScanner.Tests.Bootstrap;

/// <summary>
/// Test-Implementierung von IDataStorePathProvider für isolierte Unit-Tests.
/// </summary>
/// <remarks>
/// <para>
/// Diese Implementierung verwendet ein temporäres Verzeichnis für jeden Test-Lauf:
/// - Verhindert Konflikte zwischen parallelen Tests
/// - Automatische Cleanup-Möglichkeit nach Test
/// - Konsistente Pfad-Generierung für JSON und LiteDB
/// </para>
/// <para>
/// <b>Best Practice:</b><br/>
/// Wird in Test-Fixtures registriert und über DI an Registrars übergeben.
/// </para>
/// </remarks>
public sealed class TestDataStorePathProvider : IDataStorePathProvider
{
    private readonly string _basePath;

    /// <summary>
    /// Initialisiert den TestDataStorePathProvider mit einem Base-Pfad.
    /// </summary>
    /// <param name="basePath">Basis-Verzeichnis für alle Test-Dateien (z.B. Temp-Ordner).</param>
    public TestDataStorePathProvider(string basePath)
    {
        if (string.IsNullOrWhiteSpace(basePath))
        {
            throw new ArgumentException("Base path cannot be null or whitespace.", nameof(basePath));
        }

        _basePath = basePath;

        // Sicherstellen, dass das Verzeichnis existiert
        Directory.CreateDirectory(_basePath);
    }

    /// <summary>
    /// Erstellt einen eindeutigen Test-PathProvider mit Temp-Verzeichnis.
    /// </summary>
    /// <returns>Neuer PathProvider mit eindeutigem Temp-Ordner.</returns>
    public static TestDataStorePathProvider CreateUnique()
    {
        var uniquePath = Path.Combine(
            Path.GetTempPath(),
            "BookScanner_Tests",
            Guid.NewGuid().ToString("N"));

        return new TestDataStorePathProvider(uniquePath);
    }

    /// <inheritdoc />
    public string GetApplicationPath() => _basePath;

    /// <inheritdoc />
    public string GetDataPath() => Path.Combine(_basePath, "Data");

    /// <inheritdoc />
    public string GetSettingsPath() => Path.Combine(_basePath, "Settings");

    /// <inheritdoc />
    public string GetLogPath() => Path.Combine(_basePath, "Logs");

    /// <inheritdoc />
    public string GetCachePath() => Path.Combine(_basePath, "Cache");

    /// <inheritdoc />
    public string GetTempPath() => Path.Combine(_basePath, "Temp");

    /// <inheritdoc />
    public string GetCustomPath(string subdirectory)
    {
        if (string.IsNullOrWhiteSpace(subdirectory))
        {
            throw new ArgumentException("Subdirectory cannot be null or whitespace.", nameof(subdirectory));
        }

        return Path.Combine(_basePath, subdirectory);
    }

    /// <inheritdoc />
    public string FormatJsonFileName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Name cannot be null or whitespace.", nameof(name));
        }

        return Path.Combine(GetDataPath(), $"{name}.json");
    }

    /// <inheritdoc />
    public string FormatLiteDbFileName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Name cannot be null or whitespace.", nameof(name));
        }

        return Path.Combine(GetDataPath(), $"{name}.db");
    }

    /// <inheritdoc />
    public string FormatSettingsFileName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Name cannot be null or whitespace.", nameof(name));
        }

        return Path.Combine(GetSettingsPath(), $"{name}.json");
    }

    /// <inheritdoc />
    public string FormatLogFileName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Name cannot be null or whitespace.", nameof(name));
        }

        return Path.Combine(GetLogPath(), $"{name}.log");
    }

    /// <inheritdoc />
    public void EnsureDirectoriesExist()
    {
        Directory.CreateDirectory(GetApplicationPath());
        Directory.CreateDirectory(GetDataPath());
        Directory.CreateDirectory(GetSettingsPath());
        Directory.CreateDirectory(GetLogPath());
        Directory.CreateDirectory(GetCachePath());
        Directory.CreateDirectory(GetTempPath());
    }

    /// <summary>
    /// Gibt den Basis-Pfad zurück.
    /// </summary>
    public string BasePath => _basePath;

    /// <summary>
    /// Bereinigt alle Dateien im Test-Verzeichnis.
    /// </summary>
    public void Cleanup()
    {
        try
        {
            if (Directory.Exists(_basePath))
            {
                Directory.Delete(_basePath, recursive: true);
            }
        }
        catch
        {
            // Best-effort cleanup in tests
        }
    }
}
