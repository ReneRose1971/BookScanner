# BookScanner - Implementation Tasks

**Last Updated:** 2025-01-20

---

## ? Completed

- [x] Projekt-Setup (Solution mit 4 Projekten)
- [x] DLL-Referenzen konfiguriert (DataStores, CustomWPFControls, Common.BootStrap, TestHelper)
- [x] Instructions.md erstellt (Project Overview)
- [x] TASKS.md und CHANGELOG.md angelegt
- [x] **Phase 1, Step 1.1:** Book Entity implementiert
  - Book erbt von EntityBase (DataStores)
  - Properties: ISBN, Title, Author, Publisher, Year, CoverUrl, Tags
  - Equals/GetHashCode basierend auf Id (EntityBase)
  - BookEqualityComparer für fachliche Identität (Titel + Autor)
  - Unit Tests (22 Tests, alle grün)

---

## ?? In Progress

*Noch keine Tasks in Bearbeitung*

---

## ?? Planned

### **Phase 1: Domain Foundation**
- [x] **Step 1.1:** Book Entity ?
- [ ] **Step 1.2:** BookList Entity
  - Properties: Id (int, von EntityBase), Name, Category
  - Equals/GetHashCode (Id-basiert)
  - Unit Tests
- [ ] **Step 1.3:** BookListCategory Enum
  - Values: Database, Wishlist (erweiterbar)
  - Unit Tests für Validierung

### **Phase 2: Repository Abstraction**
- [ ] **Step 2.1:** IBookRepository Interface
  - GetByList(Guid listId)
  - Add(Book book, Guid listId)
  - Remove(Book book)
  - FindByIsbn(string isbn)
- [ ] **Step 2.2:** IBookListRepository Interface
  - GetAll()
  - GetByCategory(BookListCategory category)
  - Add(BookList list)
  - Remove(BookList list)

### **Phase 3: In-Memory Implementation**
- [ ] **Step 3.1:** InMemoryBookRepository
  - Nutzt InMemoryDataStore\<Book\>
  - Dictionary für List-Zuordnung
  - Unit Tests mit Fixtures
- [ ] **Step 3.2:** InMemoryBookListRepository
  - Nutzt InMemoryDataStore\<BookList\>
  - Unit Tests

### **Phase 4: External API Abstraction**
- [ ] **Step 4.1:** BookMetadata DTO
  - ISBN, Title, Author, Publisher, Year, CoverUrl, Source
- [ ] **Step 4.2:** IBookLookupService Interface
  - SearchByTitleAsync(string title, string? author)
  - LookupByIsbnAsync(string isbn)
- [ ] **Step 4.3:** BookLookupResult (Aggregation Model)
  - Sammelt Treffer aus mehreren Quellen
  - Deduplizierung-Logik
  - Confidence-Score
- [ ] **Step 4.4:** NullBookLookupService (Test-Implementierung)
  - Gibt leere Ergebnisse zurück
  - Für Tests ohne externe APIs

### **Phase 5: OpenLibrary Client**
- [ ] **Step 5.1:** OpenLibraryClient Implementation
  - HttpClient-basiert
  - ISBN-Lookup
  - Title/Author-Search
- [ ] **Step 5.2:** Integration Tests
  - Mit Mock-Daten (bevorzugt)
  - Optional: Mit echten API-Aufrufen
- [ ] **Step 5.3:** OpenLibraryServiceModule (DI)
  - IServiceModule für Common.BootStrap
  - Registriert OpenLibraryClient

### **Phase 6: Search Aggregation**
- [ ] **Step 6.1:** BookSearchAggregator
  - Nimmt List\<IBookLookupService\>
  - Ruft parallel/sequenziell ab
  - Dedupliziert nach ISBN
- [ ] **Step 6.2:** Unit Tests
  - Mit NullBookLookupService
  - Mit Mock-Daten

### **Phase 7: WPF Foundation**
- [ ] **Step 7.1:** App.xaml + MainWindow.xaml
  - Leeres Fenster
  - DI-Container-Setup
- [ ] **Step 7.2:** MainViewModel
  - Nutzt IBookListRepository
  - ObservableCollection\<BookListViewModel\>
- [ ] **Step 7.3:** BookListViewModel
  - Wrapped BookList (ViewModelBase\<BookList\>)

### **Phase 8: Book Display**
- [ ] **Step 8.1:** BookViewModel
  - Wrapped Book
  - Properties für Binding
- [ ] **Step 8.2:** MainWindow UI
  - ListBox für BookLists (gruppiert nach Category)
  - DataGrid für Books

### **Phase 9: Add Book Dialog (UC-01)**
- [ ] **Step 9.1:** AddBookDialog.xaml + ViewModel
  - Textfelder: Title, Author
  - ComboBox: Zielliste
  - Search-Button
- [ ] **Step 9.2:** Search Logic
  - Ruft BookSearchAggregator auf
  - Zeigt Treffer an
- [ ] **Step 9.3:** Integration in MainWindow
  - Command "Add Book"

### **Phase 10: DNB Client** (geplant)
- [ ] DNB SRU API-Client
- [ ] Integration in Aggregator

### **Phase 11: Google Books Client** (optional)
- [ ] Google Books API-Client
- [ ] Feature-Flag-Integration

### **Phase 12: OpenAI Vision** (optional)
- [ ] OpenAI Vision API-Client
- [ ] Bild-Upload UI
- [ ] Feature-Flag-Integration

### **Phase 13: Persistenz**
- [ ] SQLite/JSON-Repository-Implementierung
- [ ] Datenmigration

### **Phase 14: Tags UI**
- [ ] Tag-Verwaltung im AddBookDialog
- [ ] Tag-Anzeige in BookViewModel

### **Phase 15: List Management UI**
- [ ] Dialog für Listenverwaltung (UC-04)
- [ ] Listen erstellen/umbenennen/löschen

---

## ?? Current Focus

**Next Step:** Phase 1, Step 1.2 - BookList Entity implementieren

---

## ?? Notes

- Alle Phasen folgen Test-First-Prinzip (Unit Tests vor/mit Implementation)
- External APIs sind austauschbar durch Interfaces
- Feature-Flags für kostenpflichtige Dienste (OpenAI Vision, Google Books)
- UI folgt MVVM-Pattern mit CustomWPFControls (ViewModelBase)
