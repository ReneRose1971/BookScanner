# BookScanner - Requirements

## App-Ziel
BookScanner ist eine WPF-Anwendung zum Verwalten von Büchern in zwei Listen:
- **Datenbank**: Bücher, die der Benutzer besitzt
- **Wunschliste**: Bücher, die der Benutzer haben möchte

## Funktionen
- **Import**: Bücher können per Scan (Barcode/ISBN) oder Suche hinzugefügt werden
- **Verwaltung**: Einfache Verwaltung beider Listen
- **Erweiterbarkeit**: Architektur ermöglicht spätere Erweiterungen

## Technische Basis
- .NET 8
- WPF für UI
- Clean Architecture (Core + UI-Layer)
- Dependency Injection
- xUnit für Tests
