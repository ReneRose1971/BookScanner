using BookScanner.Bootstrap;
using Common.Bootstrap;
using DataStores.Abstractions;
using DataStores.Bootstrap;
using Microsoft.Extensions.DependencyInjection;

namespace BookScanner.Tests.Bootstrap;

/// <summary>
/// Test-Fixture für BookScannerDataStoreRegistrar-Tests.
/// </summary>
/// <remarks>
/// <para>
/// Diese Fixture richtet eine vollständige Test-Umgebung gemäß Best Practices ein:
/// - TestDataStorePathProvider für isolierte Dateipfade
/// - DefaultBootstrapWrapper für automatisches IServiceModule-Scanning
/// - DataStoresBootstrapDecorator für automatisches IDataStoreRegistrar-Scanning
/// - Automatische Cleanup beim Dispose
/// </para>
/// <para>
/// <b>Best Practice (DataStores.md):</b><br/>
/// Folgt dem 6-Schritt Bootstrap-Prozess:
/// 1. PathProvider registrieren<br/>
/// 2. DefaultBootstrapWrapper instanziieren<br/>
/// 3. DataStoresBootstrapDecorator instanziieren<br/>
/// 4. RegisterServices aufrufen<br/>
/// 5. ServiceProvider bauen<br/>
/// 6. DataStoreBootstrap.RunAsync() ausführen
/// </para>
/// <para>
/// <b>Verwendung:</b><br/>
/// <code>
/// public class MyTests : IClassFixture&lt;BookScannerDataStoreFixture&gt;
/// {
///     private readonly BookScannerDataStoreFixture _fixture;
///     
///     public MyTests(BookScannerDataStoreFixture fixture)
///     {
///         _fixture = fixture;
///     }
///     
///     [Fact]
///     public void Test_BookStore()
///     {
///         var store = _fixture.DataStores.GetGlobal&lt;Book&gt;();
///         // ... Test-Code
///     }
/// }
/// </code>
/// </para>
/// </remarks>
public sealed class BookScannerDataStoreFixture : IDisposable
{
    /// <summary>
    /// ServiceProvider für DI-Tests.
    /// </summary>
    public IServiceProvider ServiceProvider { get; }

    /// <summary>
    /// Test-PathProvider mit eindeutigem Temp-Verzeichnis.
    /// </summary>
    public TestDataStorePathProvider PathProvider { get; }

    /// <summary>
    /// DataStores Facade für Zugriff auf alle Stores.
    /// /// </summary>
    public IDataStores DataStores { get; }

    /// <summary>
    /// Pfad zur Test-Datenbank (für explizite Zugriffe falls nötig).
    /// </summary>
    public string DatabasePath => PathProvider.FormatLiteDbFileName("bookscanner");

    /// <summary>
    /// Temp-Verzeichnis für die Test-Datenbank.
    /// </summary>
    public string TempDirectory => PathProvider.BasePath;

    public BookScannerDataStoreFixture()
    {
        // Schritt 1: PathProvider registrieren
        PathProvider = TestDataStorePathProvider.CreateUnique();
        
        // Sicherstellen, dass alle Verzeichnisse existieren
        PathProvider.EnsureDirectoriesExist();

        // ServiceCollection aufbauen
        var services = new ServiceCollection();

        // PathProvider als Singleton registrieren
        services.AddSingleton<IDataStorePathProvider>(PathProvider);

        // Schritt 2: DefaultBootstrapWrapper instanziieren
        var defaultWrapper = new DefaultBootstrapWrapper();

        // Schritt 3: DataStoresBootstrapDecorator instanziieren
        var bootstrap = new DataStoresBootstrapDecorator(defaultWrapper);

        // Schritt 4: RegisterServices aufrufen
        // Scannt automatisch:
        // - IServiceModule (DataStoresServiceModule)
        // - IEqualityComparer<T> (BookEqualityComparer)
        // - IDataStoreRegistrar (BookScannerDataStoreRegistrar)
        bootstrap.RegisterServices(
            services,
            typeof(DefaultBootstrapWrapper).Assembly,      // Common.Bootstrap
            typeof(DataStoresBootstrapDecorator).Assembly, // DataStores
            typeof(BookScannerDataStoreRegistrar).Assembly // BookScanner
        );

        // Schritt 5: ServiceProvider bauen
        ServiceProvider = services.BuildServiceProvider();

        // Schritt 6: DataStoreBootstrap ausführen
        // WICHTIG: Muss synchron ausgeführt werden in Test-Fixtures
        // (xUnit unterstützt kein async in Konstruktoren)
        DataStoreBootstrap.RunAsync(ServiceProvider).GetAwaiter().GetResult();

        // DataStores Facade bereitstellen
        DataStores = ServiceProvider.GetRequiredService<IDataStores>();
    }

    /// <summary>
    /// Bereinigt die Test-Datenbank und das Temp-Verzeichnis.
    /// </summary>
    public void Dispose()
    {
        // ServiceProvider disposen
        if (ServiceProvider is IDisposable disposable)
        {
            disposable.Dispose();
        }

        // PathProvider cleanup
        PathProvider.Cleanup();
    }
}
