# BookScanner

A WPF application for managing personal book collections, built with .NET 8 and Clean Architecture.

## Features

- **Book Management**: Manage two separate lists:
  - **My Books**: Books you own
  - **Wishlist**: Books you want to acquire
- **Search & Add**: Add books by ISBN or search term
- **Clean Architecture**: Separation of concerns with Domain, Application, and Infrastructure layers
- **MVVM Pattern**: Modern WPF development using CommunityToolkit.Mvvm
- **Dependency Injection**: Fully configured DI container for testability

## Technology Stack

- **.NET 8**
- **WPF** (Windows Presentation Foundation)
- **CommunityToolkit.Mvvm** for MVVM implementation
- **Microsoft.Extensions.DependencyInjection** for DI
- **xUnit** for unit testing

## Project Structure

```
BookScanner/
??? BookScanner/                    # Core library (Domain, Application, Infrastructure)
?   ??? Domain/
?   ?   ??? Entities/              # Domain entities (Book, BookListEntry)
?   ?   ??? Enums/                 # Enumerations (BookListType)
?   ?   ??? Repositories/          # Repository interfaces
?   ??? Application/
?   ?   ??? UseCases/              # Use cases (AddBookToList, GetBooksInList)
?   ??? Infrastructure/
?       ??? Persistence/           # Repository implementations
?       ??? DependencyInjection/   # DI configuration
??? BookScanner.Wpf/               # WPF Application
?   ??? ViewModels/                # ViewModels
?   ??? Views/                     # XAML Views
??? BookScanner.Tests/             # Core unit tests
??? BookScanner.Wpf.Tests/         # WPF unit tests
```

## Getting Started

### Prerequisites

- .NET 8 SDK
- Visual Studio 2022 or later (recommended) or VS Code
- Windows OS (for WPF)

### Building

```bash
dotnet build BookScanner.sln
```

### Running

```bash
dotnet run --project BookScanner.Wpf
```

### Testing

```bash
dotnet test BookScanner.sln
```

## Usage

1. Launch the application
2. Enter a book title or ISBN in the search box
3. Click "Add to Database" to add to your collection or "Add to Wishlist" to add to your wishlist
4. View your books in the respective lists

## Architecture

The application follows Clean Architecture principles:

- **Domain Layer**: Contains business entities and repository interfaces
- **Application Layer**: Contains use cases (business logic)
- **Infrastructure Layer**: Contains concrete implementations (repositories, DI setup)
- **Presentation Layer**: WPF UI with MVVM pattern

All layers follow the Dependency Rule: dependencies point inward toward the domain.

## Future Enhancements

- ISBN barcode scanning
- Integration with book APIs (Google Books, Open Library)
- Book cover images
- Advanced search and filtering
- Export/Import functionality
- Database persistence (currently in-memory)

## License

This project is open source and available under the MIT License.

## Contributing

Contributions are welcome! Please feel free to submit a Pull Request.
