using Xunit;

namespace BookScanner.Tests.Bootstrap;

/// <summary>
/// Definition einer Test-Collection für BookScanner DataStore Tests.
/// Tests in derselben Collection werden SEQUENZIELL ausgeführt.
/// </summary>
[CollectionDefinition("BookScanner DataStore Collection")]
public class BookScannerDataStoreCollection : ICollectionFixture<BookScannerDataStoreFixture>
{
    // Diese Klasse hat keinen Code, dient nur als Marker für die Collection
}
