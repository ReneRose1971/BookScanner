using BookScanner.Domain.Comparers;
using BookScanner.Domain.Entities;
using DataStores.Abstractions;
using DataStores.Bootstrap;
using DataStores.Registration;

namespace BookScanner.Bootstrap;

/// <summary>
/// Registriert alle DataStores für die BookScanner-Anwendung.
/// </summary>
/// <remarks>
/// <para>
/// Dieser Registrar verwendet das Builder-Pattern aus DataStores.Registration:
/// - LiteDbDataStoreBuilder für persistente Stores (Books)
/// - InMemoryDataStoreBuilder für temporäre Stores (falls benötigt)
/// </para>
/// <para>
/// <b>Verwendung:</b><br/>
/// Der Registrar wird automatisch vom DataStoresBootstrapDecorator erkannt und ausgeführt.
/// Erfordert einen <b>parameterlosen öffentlichen Konstruktor</b> für automatisches Scanning.
/// </para>
/// <para>
/// <b>Best Practice:</b><br/>
/// Der Datenbankpfad wird über den IDataStorePathProvider bezogen, nicht über Konstruktor-Parameter.
/// </para>
/// </remarks>
public class BookScannerDataStoreRegistrar : DataStoreRegistrarBase
{
    /// <summary>
    /// Initialisiert eine neue Instanz des BookScannerDataStoreRegistrar.
    /// </summary>
    /// <remarks>
    /// Parameterloser Konstruktor ist erforderlich für automatisches Scanning durch DataStoresBootstrapDecorator.
    /// </remarks>
    public BookScannerDataStoreRegistrar()
    {
    }

    /// <summary>
    /// Konfiguriert alle DataStores für die BookScanner-Anwendung.
    /// </summary>
    /// <param name="serviceProvider">Service Provider für Dependency Resolution.</param>
    /// <param name="pathProvider">Path Provider für standardisierte Dateipfade.</param>
    protected override void ConfigureStores(
        IServiceProvider serviceProvider,
        IDataStorePathProvider pathProvider)
    {
        // Book Store: Persistent mit LiteDB
        // Datenbankpfad wird über pathProvider generiert
        AddStore(new LiteDbDataStoreBuilder<Book>(
            databasePath: pathProvider.FormatLiteDbFileName("bookscanner"),
            comparer: new BookEqualityComparer()));
    }
}
