# xsc-ivanti-mobile-app

> Ivanti mobile application built with .NET 10 and Blazor, following Clean Architecture principles

---

## 📚 Documentation Index

### Architecture & Standards
- [Clean Architecture Guidelines](docs/architecture/copilot-instructions-architecture.md)  
  Rules, patterns, and best practices for Clean Architecture implementation including 4-layer structure, naming conventions, and dependency injection patterns.

- [Documentation Management](docs/architecture/copilot-instructions-documents-management.md)  
  Rules for organizing and maintaining documentation in the repository.

### Refactoring Implementation (In Progress)
These documents are stored in `/docs-archive` as they track the ongoing refactoring process:

- [Clean Architecture Implementation Summary](docs-archive/clean-architecture-implementation-summary.md)  
  Complete summary of refactoring progress, files created, structure changes, and metrics.

- [Clean Architecture Manual Cleanup Guide](docs-archive/clean-architecture-manual-cleanup.md)  
  Step-by-step implementation instructions with code templates and troubleshooting.

- [Quick Reference Migration Guide](docs-archive/quick-reference-migration.md)  
  Fast reference guide with type mappings, common issues, and solutions during implementation.

- [Clean Architecture Progress Tracking](docs-archive/clean-architecture-progress.md)  
  Detailed progress report with file-by-file checklist and phase tracking.

- [Clean Architecture Refactoring Plan](docs-archive/clean-architecture-refactoring-plan.md)  
  Original comprehensive refactoring plan and design decisions.

### Quick Start Guides (In Progress)
- [Project Status Overview](docs-archive/project-status.md)  
  Current refactoring status, what's complete, remaining work, and timeline.

- [Start Here Guide](docs-archive/start-here.md)  
  Introduction guide with reading paths for developers, architects, and project managers.

---

## Project Structure

```
xsc-ivanti-mobile-app/
├── src/
│   ├── Domain/                    ← Core business logic and entities
│   ├── Application/               ← Use cases, DTOs, and abstractions
│   ├── Infrastructure/            ← External integrations and utilities
│   └── WebUI/                     ← Blazor UI (features-based organization)
│
├── docs/                          ← Permanent documentation
│   └── architecture/
│       ├── copilot-instructions-architecture.md
│       └── copilot-instructions-documents-management.md
│
└── docs-archive/                  ← Temporary/historical documentation
    └── [refactoring guides and implementation plans]
```

---

## Architecture Overview

The project follows **Clean Architecture** principles with 4 distinct layers:

```
┌─────────────────────────────────────────┐
│          WebUI (Blazor)                 │ ← Presentation
│     Features, ViewModels, Pages         │   (Framework Dependent)
└────────────────┬────────────────────────┘
                 │ depends only on
┌────────────────▼────────────────────────┐
│      Application Layer                  │ ← Business Logic
│  DTOs, Abstractions, Exceptions, Logic  │   (Framework Independent)
└────────────────┬────────────────────────┘
                 │ depends only on
┌────────────────▼────────────────────────┐
│      Infrastructure Layer               │ ← Implementation Details
│  Services, Http, Mapping, Utilities     │   (Framework Dependent)
└────────────────┬────────────────────────┘
                 │ depends only on
┌────────────────▼────────────────────────┐
│           Domain Layer                  │ ← Core Business
│    Entities, Enums, Exceptions          │   (Framework Independent)
└─────────────────────────────────────────┘
```

**Key Principles**:
- ✅ Dependency Inversion - Higher layers depend on abstractions
- ✅ No Circular Dependencies - Strict DAG
- ✅ Framework Independence - Application layer is decoupled
- ✅ Single Responsibility - Each layer has one reason to change
- ✅ Testability - Business logic is testable without frameworks

For detailed guidelines, see [Clean Architecture Guidelines](docs/architecture/copilot-instructions-architecture.md).

---

## Technology Stack

- **.NET 10** - Latest LTS framework
- **Blazor Server** - Real-time web UI
- **Mapster** - DTO mapping
- **Serilog** - Structured logging
- **MudBlazor** - UI component library

---

## Current Status

**Clean Architecture Refactoring**: 80% Complete ✅

### Completed
- ✅ 15 new files created
- ✅ DTOs organized by domain (Session, Incident, Workspace, Form)
- ✅ Service abstractions defined
- ✅ Exception hierarchy established
- ✅ Utilities properly located
- ✅ ViewModels reorganized
- ✅ Comprehensive documentation (52 pages)

### Remaining (20%)
- ⏳ Create adapter (15 min)
- ⏳ Update DTO mappings (10 min)
- ⏳ Fix dependency injection (5 min)
- ⏳ Compilation fixes (15 min)

**Estimated Time to Complete**: 45-90 minutes

See [Project Status](docs-archive/project-status.md) for details.

---

## Getting Started

### For Developers
1. Read [Clean Architecture Guidelines](docs/architecture/copilot-instructions-architecture.md)
2. Follow [Manual Cleanup Guide](docs-archive/clean-architecture-manual-cleanup.md) for implementation
3. Reference [Quick Guide](docs-archive/quick-reference-migration.md) while coding

### For Architects
1. Read [Implementation Summary](docs-archive/clean-architecture-implementation-summary.md)
2. Review [Architecture Guidelines](docs/architecture/copilot-instructions-architecture.md)
3. Check [Progress Tracking](docs-archive/clean-architecture-progress.md)

### For Project Managers
1. Check [Project Status](docs-archive/project-status.md) - 5 minute overview
2. Share status with stakeholders

---

## Contributing Guidelines

When contributing code, follow:
1. **[Clean Architecture Guidelines](docs/architecture/copilot-instructions-architecture.md)**
   - Maintain 4-layer structure
   - Organize by feature (WebUI)
   - Use dependency injection
   - Apply SOLID principles

2. **[Documentation Management](docs/architecture/copilot-instructions-documents-management.md)**
   - Architecture docs in `/docs`
   - Temporary docs in `/docs-archive`
   - Update README.md index
   - Use kebab-case filenames

3. **Code Standards**
   - PascalCase for class names
   - kebab-case for file names
   - Constructor injection for dependencies
   - Result pattern for operations
   - Async/await for I/O operations

---

## Resources

- [Clean Architecture by Robert C. Martin](https://blog.cleancoder.com/uncle-bob/2012/08/13/the-clean-architecture.html)
- [SOLID Principles](https://en.wikipedia.org/wiki/SOLID)
- [.NET Documentation](https://docs.microsoft.com/en-us/dotnet/)
- [Blazor Documentation](https://docs.microsoft.com/en-us/aspnet/core/blazor/)
- [Mapster Guide](https://github.com/MapsterMapper/Mapster/wiki)

---

## Project Links

- **GitHub**: https://github.com/uab-uniwave/xsc-ivanti-mobile-app
- **Issues**: [GitHub Issues](https://github.com/uab-uniwave/xsc-ivanti-mobile-app/issues)
- **Discussions**: [GitHub Discussions](https://github.com/uab-uniwave/xsc-ivanti-mobile-app/discussions)

---

**Last Updated**: March 24, 2026  
**Status**: Active Development  
**Target Framework**: .NET 10