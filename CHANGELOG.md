# Changelog

Alle bedeutsamen Änderungen an diesem Projekt werden in dieser Datei dokumentiert.

Das Format basiert auf [Keep a Changelog](https://keepachangelog.com/de/1.0.0/),
und dieses Projekt folgt [Semantic Versioning](https://semver.org/lang/de/).

---

## [Unreleased]

### Added
- TASKS.md zur Verfolgung des Implementierungsstatus
- CHANGELOG.md zur Dokumentation von Änderungen
- **Book Entity** (BookScanner/Project/Domain/Entities/Book.cs)
  - Erbt von EntityBase (DataStores.Abstractions)
  - Properties: ISBN, Title (required), Author, Publisher, Year, CoverUrl, Tags
  - **Equals/GetHashCode basierend auf fachlicher Identität (Titel + Autor), NICHT auf Id**
  - ToString() gibt Titel zurück
- **BookEqualityComparer** (BookScanner/Project/Domain/Comparers/BookEqualityComparer.cs)
  - Delegiert an Book.Equals/GetHashCode (Konsistenz)
  - Für Verwendung mit InMemoryDataStore<Book>
- **Unit Tests für Book** (BookScanner.Tests/Domain/Entities/BookTests.cs)
  - 13 Tests für Book-Erstellung, Properties, Equals/GetHashCode
  - Tests für Id-Unabhängigkeit (wichtig vor Persistierung)
  - Tests für HashCode-Stabilität (bleibt gleich nach Id-Zuweisung)
- **Unit Tests für BookEqualityComparer** (BookScanner.Tests/Domain/Comparers/BookEqualityComparerTests.cs)
  - 15 Tests für Delegations-Logik
- **WPF Application Entry Point**
  - App.xaml + App.xaml.cs (Application-Klasse)
  - MainWindow.xaml + MainWindow.xaml.cs (Dummy-Fenster)
  - Beseitigt Build-Fehler CS5001 (fehlender Main-Einstiegspunkt)

### Changed
- **BookScanner.Wpf.csproj**: PropertyGroup an den Anfang verschoben (korrekte Struktur)

### Fixed
- Build-Fehler in BookScanner.Wpf behoben (fehlender WPF-Einstiegspunkt)
- Alle 4 Projekte kompilieren jetzt erfolgreich ?

### Technical Details
- **Wichtige Design-Entscheidung:** Equals/GetHashCode basieren auf Titel + Autor (fachliche Identität)
  - **Problem gelöst:** Id ist vor Persistierung 0 ? Id-basierte Gleichheit wäre instabil
  - **Vorteil:** HashCode bleibt stabil, auch nach DB-Insert (wichtig für HashSets/Dictionaries)
- ISBN ist optional (nicht alle Bücher haben ISBN)
- 28 Unit Tests, alle bestanden ?
- WPF-App startet mit Dummy-Fenster (bereit für MVVM-Implementierung)

---

## [2025-01-20] - Initial Setup

### Added
- Projekt-Struktur erstellt:
  - BookScanner (Core Domain)
  - BookScanner.Wpf (WPF UI Layer)
  - BookScanner.Tests (Unit Tests für Core)
  - BookScanner.Wpf.Tests (UI/ViewModel Tests)
- DLL-Referenzen konfiguriert:
  - Common.BootStrap (.NET 8)
  - DataStores (.NET 8)
  - CustomWPFControls (.NET 8-windows)
  - TestHelper (.NET 8)
  - TestHelper.DataStores (.NET 8)
- .pdb-Dateien für alle Referenzen verlinkt (Debug-Support)
- Instructions.md mit vollständiger Project Overview:
  - Flexible Listen-Konzept (beliebig viele Listen)
  - Kategorien (Database, Wishlist)
  - Tags (frei definierbar)
  - Use Cases (UC-01 bis UC-04)
  - Externe Services (OpenLibrary, DNB, Google Books, OpenAI Vision)
  - Such- und Fallback-Strategie
  - UI-Prinzipien (MVVM)
  - Architektur-Leitlinien

### Changed
- Testprojekte erweitert:
  - Alle DLL-Referenzen in BookScanner.Tests
  - Alle DLL-Referenzen in BookScanner.Wpf.Tests

### Technical Details
- Target Framework: .NET 8
- Test Framework: xUnit
- DI Pattern: IServiceModule (Common.BootStrap)
- Data Pattern: InMemoryDataStore (DataStores)
- UI Pattern: ViewModelBase, CollectionViewModel (CustomWPFControls)

---

## Template für zukünftige Einträge

```markdown
## [YYYY-MM-DD] - Phase X, Step X.X

### Added
- Neue Dateien/Features

### Changed
- Geänderte Dateien/Logik

### Fixed
- Bugfixes

### Removed
- Entfernte Dateien/Features
