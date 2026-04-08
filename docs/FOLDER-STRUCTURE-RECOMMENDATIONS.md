# Folder Structure Recommendations for XSC Ivanti Mobile App

## 📋 Executive Summary

Based on **Microsoft Learn** official documentation and **Clean Architecture** best practices, here are comprehensive recommendations for your project structure. This document consolidates research from Microsoft's official guidance on:

- Clean Architecture for ASP.NET Core applications
- Feature-based organization (Vertical Slices)
- Blazor project structure best practices
- Enterprise application architecture patterns

## 🎯 Current Structure Analysis

### ✅ What You're Doing Right

1. **Clean Architecture Layers** - Proper separation of Domain, Application, Infrastructure, WebUI
2. **MVVM Pattern** - Correct implementation with ViewModel separation
3. **Feature Folders in WebUI** - Good start with `Features/Login`, `Features/Incidents`
4. **Service Interfaces in Application Layer** - Proper abstraction with `Interfaces/`

### ⚠️ Areas for Improvement

Based on Microsoft best practices, here are recommended enhancements:

---

## 🏗️ Recommended Final Structure

```
xsc-ivanti-mobile-app/
├── docs/                                    # 📚 Documentation
│   ├── architecture/                        # Architecture diagrams & decisions
│   ├── api/                                 # API documentation
│   ├── copilot-instructions.md              # ✅ Already created
│   └── ARCHITECTURE.md                      # ✅ Already created
│
├── src/
│   ├── Domain/                              # 🎯 CORE - No dependencies
│   │   ├── Common/                          # Shared domain concepts
│   │   │   ├── BaseEntity.cs
│   │   │   ├── IAggregateRoot.cs
│   │   │   └── ValueObject.cs
│   │   ├── Entities/                        # Domain entities (if needed)
│   │   │   └── [Currently minimal for this app]
│   │   ├── Enums/                           # Domain enumerations
│   │   ├── Events/                          # Domain events (future)
│   │   └── Exceptions/                      # Domain-specific exceptions
│   │
│   ├── Application/                         # 🎯 BUSINESS LOGIC & CONTRACTS
│   │   ├── Common/                          # ✅ Shared application utilities
│   │   │   ├── Behaviours/                  # [Future] Validation, logging behaviors
│   │   │   ├── Mappings/                    # Mapping configurations
│   │   │   ├── Models/                      # Common models (Result, PagedResult)
│   │   │   └── Extensions/                  # Extension methods
│   │   │
│   │   ├── Interfaces/                      # ✅ Service contracts
│   │   │   ├── Authentication/              # ✅ IAuthenticationService
│   │   │   ├── Infrastructure/              # Infrastructure abstractions
│   │   │   │   ├── IDateTime.cs
│   │   │   │   └── IFileSystem.cs
│   │   │   ├── Navigation/                  # ✅ INavigationService
│   │   │   ├── Persistence/                 # [Future] IRepository<T>
│   │   │   └── Workspaces/                  # ✅ IWorkspaceService
│   │   │
│   │   ├── Features/                        # 📁 FEATURE-BASED ORGANIZATION
│   │   │   ├── Authentication/              # Authentication feature
│   │   │   │   ├── Commands/                # [If using CQRS later]
│   │   │   │   ├── DTOs/                    # Feature-specific DTOs
│   │   │   │   ├── Queries/                 # [If using CQRS later]
│   │   │   │   └── Models/                  # Feature models
│   │   │   │
│   │   │   ├── Incidents/                   # Incidents feature
│   │   │   │   ├── DTOs/
│   │   │   │   │   ├── IncidentDto.cs
│   │   │   │   │   ├── IncidentListItemDto.cs
│   │   │   │   │   └── IncidentUpdateRequestDto.cs
│   │   │   │   └── Models/                  # Incident-specific models
│   │   │   │
│   │   │   └── Workspaces/                  # Workspaces feature
│   │   │       ├── DTOs/
│   │   │       └── Models/
│   │   │           ├── FormDefaultData/
│   │   │           ├── FormValidationListData/
│   │   │           ├── FormViewData/
│   │   │           ├── RoleWorkspaces/
│   │   │           ├── ValidatedSearch/
│   │   │           └── WorkspaceData/
│   │   │
│   │   ├── DTOs/                            # ⚠️ MIGRATE TO Features/
│   │   │   └── [Move to respective features]
│   │   │
│   │   ├── Models/                          # ⚠️ REORGANIZE
│   │   │   ├── Common/                      # Truly shared models only
│   │   │   │   ├── SessionData/
│   │   │   │   └── UserData/
│   │   │   └── [Move feature-specific to Features/]
│   │   │
│   │   ├── Requests/                        # API Request DTOs
│   │   │   └── [Organized by feature/endpoint]
│   │   │
│   │   ├── Responses/                       # API Response DTOs
│   │   │   └── [Organized by feature/endpoint]
│   │   │
│   │   └── Exceptions/                      # ✅ Application exceptions
│   │
│   ├── Infrastructure/                      # 🔧 EXTERNAL CONCERNS
│   │   ├── Authentication/                  # ✅ AuthenticationService
│   │   │   ├── AuthenticationService.cs
│   │   │   └── [Auth-related implementations]
│   │   │
│   │   ├── Persistence/                     # [Future] Database access
│   │   │   ├── Configurations/
│   │   │   ├── Repositories/
│   │   │   └── ApplicationDbContext.cs
│   │   │
│   │   ├── ExternalServices/                # Third-party integrations
│   │   │   └── Ivanti/                      # ✅ Ivanti API client
│   │   │       ├── Configuration/
│   │   │       │   └── IvantiOptions.cs
│   │   │       ├── IvantiClient.cs
│   │   │       ├── IvantiEndpoints.cs
│   │   │       ├── HttpClientLoggingHandler.cs
│   │   │       └── ODataQueryBuilder.cs
│   │   │
│   │   ├── Identity/                        # [Future] Identity services
│   │   │
│   │   ├── Services/                        # Infrastructure services
│   │   │   ├── DateTimeService.cs
│   │   │   └── FileService.cs
│   │   │
│   │   ├── Workspaces/                      # ✅ WorkspaceService
│   │   │   └── WorkspaceService.cs
│   │   │
│   │   ├── Mapping/                         # ✅ Mapster configuration
│   │   │   └── MapsterConfig.cs
│   │   │
│   │   ├── Utilities/                       # Infrastructure utilities
│   │   │   └── ODataQueryBuilder.cs
│   │   │
│   │   ├── Exceptions/                      # ✅ Infrastructure exceptions
│   │   │
│   │   └── DependencyInjection.cs           # ✅ Service registration
│   │
│   └── WebUI/                               # 🎨 PRESENTATION LAYER
│       ├── Components/                      # Shared Blazor components
│       │   ├── Common/                      # Reusable UI components
│       │   │   ├── LoadingSpinner.razor
│       │   │   ├── ErrorDisplay.razor
│       │   │   └── ConfirmDialog.razor
│       │   │
│       │   ├── Layout/                      # Layout components
│       │   │   ├── MainLayout.razor
│       │   │   ├── MainLayout.razor.cs
│       │   │   ├── NavMenu.razor
│       │   │   └── NavMenu.razor.cs
│       │   │
│       │   └── Shared/                      # Shared across features
│       │       ├── UserAvatar.razor
│       │       └── WorkspaceSelector.razor
│       │
│       ├── Features/                        # 📁 FEATURE-BASED PAGES
│       │   ├── Authentication/              # ♻️ RENAME from Login
│       │   │   ├── Pages/
│       │   │   │   ├── Login.razor
│       │   │   │   ├── Login.razor.cs
│       │   │   │   ├── SelectRole.razor
│       │   │   │   └── SelectRole.razor.cs
│       │   │   └── ViewModels/
│       │   │       ├── LoginViewModel.cs
│       │   │       └── SelectRoleViewModel.cs
│       │   │
│       │   ├── Incidents/                   # ✅ Good structure
│       │   │   ├── Pages/
│       │   │   │   ├── Incidents.razor      # List view
│       │   │   │   ├── Incidents.razor.cs
│       │   │   │   ├── IncidentDetails.razor # Details view
│       │   │   │   ├── IncidentDetails.razor.cs
│       │   │   │   ├── IncidentNew.razor
│       │   │   │   ├── IncidentNew.razor.cs
│       │   │   │   ├── IncidentEdit.razor
│       │   │   │   └── IncidentEdit.razor.cs
│       │   │   ├── Components/              # Feature-specific components
│       │   │   │   ├── IncidentCard.razor
│       │   │   │   ├── IncidentFilter.razor
│       │   │   │   └── IncidentStatusBadge.razor
│       │   │   └── ViewModels/
│       │   │       ├── IncidentsViewModel.cs
│       │   │       ├── IncidentDetailsViewModel.cs
│       │   │       ├── IncidentNewViewModel.cs
│       │   │       └── IncidentEditViewModel.cs
│       │   │
│       │   ├── Dashboard/                   # [Future] Dashboard feature
│       │   │   ├── Pages/
│       │   │   ├── Components/
│       │   │   └── ViewModels/
│       │   │
│       │   └── Workspaces/                  # [Future] Workspace management
│       │       ├── Pages/
│       │       ├── Components/
│       │       └── ViewModels/
│       │
│       ├── Services/                        # UI-specific services
│       │   ├── NavigationService.cs         # ✅ Already created
│       │   ├── IvantiNavigationService.cs   # [Legacy - keep for now]
│       │   ├── StateContainer.cs            # [Future] Shared state
│       │   └── ToastService.cs              # [Future] Notifications
│       │
│       ├── wwwroot/                         # Static files
│       │   ├── css/
│       │   │   ├── app.css
│       │   │   └── site.css
│       │   ├── js/
│       │   │   └── app.js
│       │   ├── images/
│       │   │   └── logo.png
│       │   └── favicon.ico
│       │
│       ├── _Imports.razor                   # Global using directives
│       ├── App.razor                        # Root component
│       ├── Routes.razor                     # Routing configuration
│       └── Program.cs                       # Entry point
│
├── tests/                                   # 🧪 TEST PROJECTS
│   ├── Application.UnitTests/
│   │   ├── Features/                        # Test by feature
│   │   │   ├── Authentication/
│   │   │   └── Incidents/
│   │   └── Common/
│   │
│   ├── Infrastructure.IntegrationTests/
│   │   ├── Authentication/
│   │   ├── ExternalServices/
│   │   │   └── Ivanti/
│   │   └── Workspaces/
│   │
│   └── WebUI.Tests/
│       ├── Features/
│       │   ├── Authentication/
│       │   │   └── LoginViewModelTests.cs
│       │   └── Incidents/
│       │       └── IncidentsViewModelTests.cs
│       └── Components/
│
└── .github/                                 # GitHub-specific files
    └── workflows/                           # CI/CD pipelines
        ├── build.yml
        └── deploy.yml
```

---

## 📊 Key Architectural Decisions

### 1. Feature-Based Organization (Vertical Slices)

**Recommendation**: Organize by **FEATURE** rather than technical concerns

#### ✅ Benefits (from Microsoft Learn):
- All related files in one place
- Easier to navigate large applications
- Better for team collaboration (feature teams)
- Reduced coupling between features
- Easier to add/remove features

#### 🎯 Implementation:

**Application Layer**: Group related DTOs, Models, and business logic by feature
```
Application/Features/
├── Authentication/
│   ├── DTOs/
│   ├── Models/
│   └── [Business logic]
│
└── Incidents/
    ├── DTOs/
    ├── Models/
    └── [Business logic]
```

**WebUI Layer**: Group pages, components, and ViewModels by feature
```
WebUI/Features/
├── Authentication/
│   ├── Pages/
│   ├── Components/
│   └── ViewModels/
│
└── Incidents/
    ├── Pages/
    ├── Components/
    └── ViewModels/
```

### 2. Clean Architecture Layer Responsibilities

#### Domain Layer (Currently Minimal - OK for this app)
```
Domain/
├── Common/           # Base classes, interfaces
├── Entities/         # [Future if needed]
├── Enums/            # Domain enumerations
└── Exceptions/       # Domain exceptions
```

**Rules**:
- ❌ No dependencies on any other layer
- ✅ Pure business rules only
- ✅ POCO classes

#### Application Layer
```
Application/
├── Common/           # Shared utilities, Result<T>
├── Interfaces/       # Service contracts grouped by concern
├── Features/         # ⭐ FEATURE-BASED organization
├── Requests/         # API request DTOs
├── Responses/        # API response DTOs
└── Exceptions/       # Application exceptions
```

**Rules**:
- ✅ Depends on Domain only
- ✅ Defines all interfaces
- ❌ No infrastructure concerns
- ✅ Business logic and orchestration

#### Infrastructure Layer
```
Infrastructure/
├── Authentication/       # Auth implementations
├── ExternalServices/     # Third-party APIs (Ivanti)
├── Persistence/          # [Future] Database access
├── Services/             # Infrastructure services
├── Mapping/              # Mapster config
└── DependencyInjection.cs
```

**Rules**:
- ✅ Implements Application interfaces
- ✅ Depends on Application layer
- ✅ Contains all external dependencies
- ❌ Should not be referenced by Application

#### WebUI Layer
```
WebUI/
├── Components/       # Shared components
│   ├── Common/      # Generic reusable
│   ├── Layout/      # Layouts
│   └── Shared/      # Shared across features
│
├── Features/         # ⭐ FEATURE-BASED pages
│   └── [Feature]/
│       ├── Pages/
│       ├── Components/  # Feature-specific
│       └── ViewModels/
│
├── Services/         # UI services (Navigation, State)
├── wwwroot/          # Static assets
└── Program.cs
```

**Rules**:
- ✅ Depends on Application layer only (for interfaces)
- ✅ Infrastructure registered in Program.cs
- ✅ MVVM pattern for all pages
- ❌ No business logic in .razor files

---

## 🔄 Migration Plan

### Phase 1: Feature-Based Reorganization (Current Priority)

#### Step 1: Create Feature Folders in Application Layer
```bash
# Create feature folders
mkdir src/Application/Features/Authentication
mkdir src/Application/Features/Incidents
mkdir src/Application/Features/Workspaces

# Create subfolders
mkdir src/Application/Features/Authentication/{DTOs,Models}
mkdir src/Application/Features/Incidents/{DTOs,Models}
mkdir src/Application/Features/Workspaces/{DTOs,Models}
```

#### Step 2: Move DTOs to Feature Folders
```bash
# Move Authentication DTOs
Move-Item src/Application/Requests/LoginRequest.cs → 
    src/Application/Features/Authentication/DTOs/
Move-Item src/Application/Requests/GetVerificationTokenRequest.cs → 
    src/Application/Features/Authentication/DTOs/
Move-Item src/Application/Responses/LoginResponse.cs → 
    src/Application/Features/Authentication/DTOs/
Move-Item src/Application/Models/Login/ → 
    src/Application/Features/Authentication/Models/

# Move Incident DTOs
Move-Item src/Application/DTOs/Incident/* → 
    src/Application/Features/Incidents/DTOs/

# Move Workspace Models
Move-Item src/Application/Models/FormDefaultData/ → 
    src/Application/Features/Workspaces/Models/
Move-Item src/Application/Models/FormValidationListData/ → 
    src/Application/Features/Workspaces/Models/
# ... etc for all workspace-related models
```

#### Step 3: Reorganize WebUI Features
```bash
# Rename Login to Authentication
Rename-Item src/WebUI/Features/Login → 
    src/WebUI/Features/Authentication

# Create Pages subfolder
mkdir src/WebUI/Features/Authentication/Pages
Move-Item src/WebUI/Features/Authentication/*.razor → 
    src/WebUI/Features/Authentication/Pages/
Move-Item src/WebUI/Features/Authentication/*.razor.cs → 
    src/WebUI/Features/Authentication/Pages/

# Do same for Incidents
mkdir src/WebUI/Features/Incidents/Pages
mkdir src/WebUI/Features/Incidents/Components
```

#### Step 4: Update Namespaces
Update all namespaces to match new structure:
```csharp
// Old
namespace Application.Requests;

// New
namespace Application.Features.Authentication.DTOs;
```

### Phase 2: Shared Components Organization

Create proper component hierarchy:
```bash
mkdir src/WebUI/Components/Common
mkdir src/WebUI/Components/Layout
mkdir src/WebUI/Components/Shared
```

Move components accordingly:
- Generic reusable → `Components/Common/`
- Layouts → `Components/Layout/`
- Shared across features → `Components/Shared/`

### Phase 3: Infrastructure Cleanup

Reorganize Ivanti client:
```bash
mkdir src/Infrastructure/ExternalServices/Ivanti
Move-Item src/Infrastructure/Ivanti/* → 
    src/Infrastructure/ExternalServices/Ivanti/
```

---

## 📁 Naming Conventions

### Folders
```
✅ PascalCase for all folders
✅ Plural for collections: Components, Services, ViewModels
✅ Singular for specific items: Authentication, Incident
```

### Files
```
✅ Match class name exactly
✅ .razor for components
✅ .razor.cs for code-behind
✅ ViewModel suffix for ViewModels
✅ Service suffix for services
✅ Tests suffix for test files
```

### Routes
```
✅ lowercase-with-hyphens: /login, /select-role, /incidents
✅ RESTful patterns: /incidents/{id}, /incidents/new
```

---

## 🎯 Best Practices Summary

### ✅ DO

1. **Organize by Feature** not by technical layer (in Application and WebUI)
2. **Keep related files together** (Page, ViewModel, Components)
3. **Use Clear Folder Names** (Pages, Components, ViewModels, DTOs, Models)
4. **Separate Concerns** (UI → Application → Infrastructure → Domain)
5. **Follow MVVM** strictly (all logic in ViewModel)
6. **Use Interfaces** for all services
7. **Keep Infrastructure isolated** (don't reference from Application)
8. **Group tests by feature** matching source structure

### ❌ DON'T

1. **Mix technical and feature organization** (be consistent)
2. **Create deep nested folders** (max 3-4 levels)
3. **Use generic names** (Utils, Helpers, Misc)
4. **Put business logic in UI layer**
5. **Reference Infrastructure from Application**
6. **Create God folders** (everything in one place)
7. **Mix different concerns in same folder**

---

## 📈 Future Considerations

### When to Add New Folders

#### Application Layer:
```
Behaviors/          # When implementing MediatR pipeline behaviors
Commands/           # If switching to CQRS pattern
Queries/            # If switching to CQRS pattern
Validators/         # When adding FluentValidation
Specifications/     # For complex query specifications
```

#### Infrastructure Layer:
```
Persistence/        # When adding database
Identity/           # When adding ASP.NET Core Identity
Caching/            # When implementing caching
Messaging/          # When adding message bus
Files/              # For file storage services
```

#### WebUI Layer:
```
Middleware/         # Custom Blazor middleware
Filters/            # Custom filters
Handlers/           # Event handlers
StateManagement/    # If using Fluxor or similar
```

---

## 🔍 References

### Microsoft Official Documentation
1. [Clean Architecture](https://learn.microsoft.com/en-us/dotnet/architecture/modern-web-apps-azure/common-web-application-architectures#clean-architecture)
2. [Feature Slices for ASP.NET Core](https://learn.microsoft.com/en-us/archive/msdn-magazine/2016/september/asp-net-core-feature-slices-for-asp-net-core-mvc)
3. [Blazor Project Structure](https://learn.microsoft.com/en-us/aspnet/core/blazor/project-structure)
4. [ASP.NET Core Architecture](https://learn.microsoft.com/en-us/dotnet/architecture/modern-web-apps-azure/develop-asp-net-core-mvc-apps#structuring-the-application)
5. [Vertical Slices](https://deviq.com/vertical-slices/)

### Sample Projects
1. [eShopOnWeb](https://github.com/dotnet-architecture/eShopOnWeb) - Microsoft's Clean Architecture sample
2. [Ardalis CleanArchitecture Template](https://github.com/ardalis/cleanarchitecture) - Industry standard template

---

## 🚀 Action Items

### Immediate (This Week)
- [ ] Create `Features/` folders in Application layer
- [ ] Move DTOs to feature-specific folders
- [ ] Rename `Login` to `Authentication` in WebUI
- [ ] Create `Pages/` subfolders in WebUI Features
- [ ] Update namespaces across the project

### Short Term (Next Sprint)
- [ ] Create `Components/Common`, `Components/Layout`, `Components/Shared`
- [ ] Move Ivanti client to `ExternalServices/Ivanti`
- [ ] Create test project structure
- [ ] Update documentation

### Long Term (Future)
- [ ] Implement test projects following feature structure
- [ ] Add CI/CD pipeline
- [ ] Consider CQRS if complexity increases

---

## 💡 Conclusion

The recommended structure balances:
- **Microsoft Best Practices** - Following official guidance
- **Clean Architecture** - Clear separation of concerns
- **Feature Organization** - Easier navigation and maintenance
- **Scalability** - Room to grow without major refactoring
- **Team Productivity** - Related files together

This structure will serve you well as the application grows and won't require revisiting later. ✨
