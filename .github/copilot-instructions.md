# Copilot Instructions for XSC Ivanti Mobile App

## Project Overview
This is a Blazor Server application for Ivanti ServiceDesk built using Clean Architecture principles with MVVM pattern for UI components.

## Architecture Principles

### Clean Architecture Layers

#### 1. Domain Layer (`src/Domain`)
- **Purpose**: Core business entities and domain logic
- **Dependencies**: None (innermost layer)
- **Contents**: 
  - Domain entities
  - Value objects
  - Domain events
  - Domain exceptions
- **Rule**: Should have zero dependencies on other layers

#### 2. Application Layer (`src/Application`)
- **Purpose**: Business logic, use cases, and contracts
- **Dependencies**: Domain only
- **Structure**:
  ```
  Application/
  ├── Common/              # Shared utilities (Result, PagedResult, etc.)
  ├── DTOs/                # Data Transfer Objects
  ├── Exceptions/          # Application-specific exceptions
  ├── Interfaces/          # Service contracts
  │   ├── Authentication/  # Auth service interfaces
  │   ├── Navigation/      # Navigation service interfaces
  │   └── Workspaces/      # Workspace service interfaces
  ├── Models/              # Application models
  │   ├── FormDefaultData/
  │   ├── FormValidationListData/
  │   ├── FormViewData/
  │   ├── Login/          # Authentication models
  │   ├── RoleWorkspaces/
  │   ├── SessonData/
  │   ├── UserData/
  │   ├── ValidatedSearch/
  │   └── WorkspaceData/
  ├── Requests/            # Request DTOs
  └── Responses/           # Response DTOs
  ```
- **Rules**:
  - Define interfaces for all services
  - All business logic goes here
  - No implementation details (database, HTTP, etc.)
  - Use Result<T> pattern for error handling

#### 3. Infrastructure Layer (`src/Infrastructure`)
- **Purpose**: External concerns and service implementations
- **Dependencies**: Application, Domain
- **Structure**:
  ```
  Infrastructure/
  ├── Authentication/      # Auth service implementations
  ├── Exceptions/          # Infrastructure-specific exceptions
  ├── Ivanti/              # Ivanti API client and related code
  │   ├── Configuration/   # Ivanti configuration options
  │   └── IvantiClient.cs  # Low-level API client
  ├── Mapping/             # Mapster configuration
  ├── Utilities/           # Infrastructure utilities
  ├── Workspaces/          # Workspace service implementations
  └── DependencyInjection.cs
  ```
- **Rules**:
  - Implement interfaces from Application layer
  - Handle external dependencies (HTTP, databases, etc.)
  - Register all services in `DependencyInjection.cs`

#### 4. WebUI/Presentation Layer (`src/WebUI`)
- **Purpose**: User interface and presentation logic
- **Dependencies**: Application, Infrastructure (for DI registration)
- **Structure**:
  ```
  WebUI/
  ├── Components/          # Reusable Blazor components
  ├── Features/            # Feature-organized pages
  │   ├── Incidents/
  │   │   ├── ViewModels/
  │   │   ├── Incidents.razor
  │   │   ├── Incidents.razor.cs
  │   │   ├── IncidentNew.razor
  │   │   ├── IncidentNew.razor.cs
  │   │   ├── IncidentEdit.razor
  │   │   └── IncidentEdit.razor.cs
  │   └── Login/
  │       ├── ViewModels/
  │       ├── Login.razor
  │       ├── Login.razor.cs
  │       ├── SelectRole.razor
  │       └── SelectRole.razor.cs
  ├── Services/            # UI-specific services (NavigationService)
  └── Program.cs
  ```

## MVVM Pattern for Blazor Pages

### File Structure for Each Page
Every page MUST follow this structure:

1. **`<PageName>.razor`** - Markup only
   ```razor
   @page "/page-route"
   @using MudBlazor

   <!-- UI markup using MudBlazor components -->
   <!-- Bind to ViewModel properties -->
   ```

2. **`<PageName>.razor.cs`** - Code-behind (minimal)
   ```csharp
   using Microsoft.AspNetCore.Components;
   using WebUI.Features.<Feature>.ViewModels;

   namespace WebUI.Features.<Feature>;

   public partial class <PageName> : ComponentBase
   {
       [Inject] private <PageName>ViewModel ViewModel { get; set; } = default!;

       protected override async Task OnInitializedAsync()
       {
           await ViewModel.InitializeAsync();
       }

       // Minimal event handlers that delegate to ViewModel
   }
   ```

3. **`<PageName>ViewModel.cs`** - All page logic
   ```csharp
   using Application.Interfaces.*;

   namespace WebUI.Features.<Feature>.ViewModels;

   public class <PageName>ViewModel
   {
       // Injected services
       private readonly IServiceName _service;
       private readonly ILogger<PageName>ViewModel> _logger;

       public <PageName>ViewModel(
           IServiceName service,
           ILogger<<PageName>ViewModel> logger)
       {
           _service = service;
           _logger = logger;
       }

       // Properties for data binding
       public bool IsLoading { get; private set; }
       public string ErrorMessage { get; private set; } = string.Empty;

       // Initialization
       public async Task InitializeAsync() { }

       // Action methods
       public async Task HandleActionAsync() { }
   }
   ```

### ViewModel Registration
All ViewModels MUST be registered in `Program.cs`:
```csharp
builder.Services.AddScoped<<PageName>ViewModel>();
```

## Service Layer Guidelines

### Service Interfaces
- Place in `Application/Interfaces/<Category>/`
- Use `I` prefix (e.g., `IAuthenticationService`)
- Document with XML comments
- Return `Result<T>` or `Task<Result<T>>`

### Service Implementations
- Place in `Infrastructure/<Category>/`
- Same name without `I` prefix (e.g., `AuthenticationService`)
- Inject required dependencies
- Use ILogger for logging
- Handle exceptions and return Result.Failure()

### Service Registration Pattern
In `Infrastructure/DependencyInjection.cs`:
```csharp
// For services needing HttpClient
services.AddHttpClient<IServiceInterface, ServiceImplementation>((sp, client) =>
{
    // Configure HttpClient
});

// For regular services
services.AddScoped<IServiceInterface, ServiceImplementation>();
```

## Authentication Flow

1. **Get Verification Token** (`GET /`)
   - Extract `__RequestVerificationToken` from HTML
   - Store cookies for session

2. **Login** (`POST /`)
   - Submit username, password, verification token
   - Receive authentication cookies
   - Initialize Ivanti session
   - Get user data

3. **Select Role/Workspace**
   - Load available workspaces
   - Navigate to selected workspace

4. **Initialize Workspace**
   - Load workspace data
   - Load form view data
   - Load form defaults
   - Load validation lists
   - Load saved searches

## Error Handling Pattern

Use the `Result<T>` pattern for all service methods:

```csharp
public async Task<Result<DataType>> MethodAsync()
{
    try
    {
        _logger.LogInformation("Starting operation...");

        // Perform operation
        var data = await _someService.GetDataAsync();

        if (data == null)
        {
            _logger.LogWarning("Data not found");
            return Result<DataType>.Failure("Data not found");
        }

        return Result<DataType>.Success(data);
    }
    catch (Exception ex)
    {
        _logger.LogError(ex, "Error performing operation");
        return Result<DataType>.Failure($"Error: {ex.Message}");
    }
}
```

## Naming Conventions

### C# Code
- **Classes**: PascalCase (e.g., `AuthenticationService`)
- **Interfaces**: `I` + PascalCase (e.g., `IAuthenticationService`)
- **Methods**: PascalCase (e.g., `LoginAsync()`)
- **Properties**: PascalCase (e.g., `IsLoading`)
- **Private fields**: _camelCase (e.g., `_logger`)
- **Parameters**: camelCase (e.g., `username`)
- **Local variables**: camelCase (e.g., `result`)

### Files and Folders
- **Folders**: PascalCase (e.g., `Authentication`, `ViewModels`)
- **Files**: Match class name (e.g., `LoginViewModel.cs`)
- **Razor files**: PascalCase (e.g., `Login.razor`)

### Routes
- Lowercase with hyphens (e.g., `/login`, `/select-role`, `/incidents`)

## MudBlazor Component Guidelines

### Standard Components
- Use MudBlazor components throughout the UI
- Consistent elevation: `Elevation="4"` for main containers
- Spacing: Use `MudStack` with `Spacing="4"` for consistent spacing
- Forms: Use `MudTextField` with `Variant.Outlined`
- Buttons: Use `MudButton` with appropriate `Variant` and `Color`
- Feedback: Use `MudAlert` for errors, `MudProgressCircular` for loading

### Loading States
```razor
@if (ViewModel.IsLoading)
{
    <MudProgressCircular Indeterminate="true" />
}
```

### Error Display
```razor
@if (ViewModel.HasError)
{
    <MudAlert Severity="Severity.Error" Dense="true">
        @ViewModel.ErrorMessage
    </MudAlert>
}
```

## Dependency Injection Rules

1. **Constructor Injection Only**
   - No property injection
   - All dependencies in constructor

2. **Scoped Services for User State**
   - ViewModels: Scoped
   - User-specific services: Scoped

3. **Singleton for Configuration**
   - Options classes
   - Mapster configuration

## Logging Guidelines

### Log Levels
- **Information**: Normal flow (user actions, service calls)
- **Warning**: Unexpected but handled situations
- **Error**: Exceptions and failures
- **Debug**: Detailed diagnostic information

### Log Messages
```csharp
_logger.LogInformation("Starting operation for user: {Username}", username);
_logger.LogWarning("Resource not found: {ResourceId}", resourceId);
_logger.LogError(ex, "Error processing request for {RequestType}", requestType);
```

## Cookie Management

- Ivanti uses cookie-based authentication
- Cookies are managed by HttpClient
- Store cookies from login page
- Include in all subsequent requests
- Clear cookies on logout

## Testing Approach (Future)

When implementing tests:
- Unit tests in `tests/Application.UnitTests`
- Integration tests in `tests/Infrastructure.IntegrationTests`
- Use xUnit framework
- Mock external dependencies
- Test ViewModels independently

## Common Patterns to Follow

### 1. Async Everywhere
- Always use async/await for I/O operations
- Suffix async methods with `Async`
- Accept `CancellationToken` parameter

### 2. Null Safety
- Enable nullable reference types
- Use null-forgiving operator (`!`) only when certain
- Check for null before use
- Use null-conditional operators (`?.`)

### 3. LINQ Usage
- Prefer LINQ for collection operations
- Use meaningful variable names in lambdas
- Consider performance for large collections

### 4. String Handling
- Use string interpolation for simple cases
- Use StringBuilder for multiple concatenations
- Use `string.IsNullOrEmpty()` and `string.IsNullOrWhiteSpace()`

## What NOT to Do

❌ **Don't** put business logic in `.razor` files
❌ **Don't** put business logic in `.razor.cs` files
❌ **Don't** reference Infrastructure from Application
❌ **Don't** create services without interfaces
❌ **Don't** use static methods for business logic
❌ **Don't** swallow exceptions without logging
❌ **Don't** use magic strings (create constants)
❌ **Don't** mix concerns (separate UI, business, data)
❌ **Don't** skip XML documentation for public APIs

## Best Practices Summary

✅ **Do** follow Clean Architecture layer boundaries
✅ **Do** use MVVM pattern for all pages
✅ **Do** implement interfaces in Application layer
✅ **Do** use Result<T> for error handling
✅ **Do** log important operations
✅ **Do** use dependency injection
✅ **Do** write XML documentation
✅ **Do** handle errors gracefully
✅ **Do** use MudBlazor components consistently
✅ **Do** keep ViewModels testable

## Quick Reference

### Creating a New Page

1. Create folder in `Features/<FeatureName>`
2. Create `<PageName>ViewModel.cs` with business logic
3. Create `<PageName>.razor` with UI markup
4. Create `<PageName>.razor.cs` with minimal code-behind
5. Register ViewModel in `Program.cs`
6. Use MudBlazor components for UI

### Creating a New Service

1. Define interface in `Application/Interfaces/<Category>/`
2. Implement in `Infrastructure/<Category>/`
3. Register in `Infrastructure/DependencyInjection.cs`
4. Inject into ViewModels via constructor

### Handling Errors

1. Wrap operations in try-catch
2. Log errors with context
3. Return `Result.Failure()` with message
4. Display in UI using MudAlert

## Version Information

- **.NET**: 10.0
- **C# Language Version**: 14.0
- **MudBlazor**: 9.2.0
- **Mapster**: 7.4.0
- **Framework**: Blazor Server

---

**Remember**: Keep it clean, keep it simple, keep it maintainable! 🚀
