# Contributing to BookScanner

Thank you for your interest in contributing to BookScanner! This document provides guidelines and instructions for contributing.

## Getting Started

1. Fork the repository
2. Clone your fork locally
3. Create a new branch for your feature or bugfix
4. Make your changes
5. Test your changes
6. Submit a pull request

## Development Setup

1. Install .NET 8 SDK
2. Install Visual Studio 2022 or your preferred IDE
3. Clone the repository
4. Open `BookScanner.sln`
5. Build the solution: `dotnet build`
6. Run tests: `dotnet test`

## Coding Standards

- Follow the existing code style
- Use meaningful variable and method names
- Add XML documentation comments for public APIs
- Keep methods focused and concise
- Write unit tests for new functionality

## Architecture Guidelines

- **Domain Layer**: Pure business logic, no dependencies on infrastructure
- **Application Layer**: Use cases that orchestrate domain objects
- **Infrastructure Layer**: Implementations of interfaces defined in Domain
- **Presentation Layer**: UI code only, no business logic

## Pull Request Process

1. Ensure all tests pass
2. Update documentation if needed
3. Describe your changes clearly in the PR description
4. Reference any related issues
5. Wait for code review

## Reporting Issues

When reporting issues, please include:
- A clear description of the problem
- Steps to reproduce
- Expected vs actual behavior
- Your environment (OS, .NET version, etc.)

## Questions?

Feel free to open an issue for questions or discussions.
