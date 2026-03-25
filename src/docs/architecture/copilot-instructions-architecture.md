# Copilot Instructions – Clean Architecture Guidelines

## Purpose

This document defines the Clean Architecture rules and patterns for this repository.

All contributors and AI assistants (including GitHub Copilot) must follow these guidelines when:
- Creating new features
- Organizing code files
- Designing system components
- Naming files and folders
- Structuring dependencies

---

## Clean Architecture Principles

### 4-Layer Architecture

The project follows **Clean Architecture** with 4 distinct layers:

```
┌─────────────────────────────────────────┐
│          WebUI (Blazor)                 │ ← Presentation Layer
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

### Core Rules

1. **Dependency Inversion**: Higher layers depend on abstractions in lower layers
2. **No Circular Dependencies**: Dependency graph must be a DAG (Directed Acyclic Graph)
3. **Framework Independence**: Application layer doesn't know about WebUI or Infrastructure
4. **Single Responsibility**: Each layer has one reason to change
5. **Testability**: All business logic is testable without external dependencies

---

## Project Structure

### Domain Layer (`Domain/`)

**Purpose**: Core business logic and entities

```
Domain/
├── Entities/
│   ├── Incident.cs
│   ├── Task.cs
│   ├── Change.cs
│   └── [other entities].cs
│
├── Enums/
│   ├── IncidentStatus.cs
│   ├── IncidentPriority.cs
│   └── [other enums].cs
│
├── ValueObjects/
│   └── [value objects if needed].cs
│
└── Exceptions/
    └── DomainException.cs
```

**Rules**:
- ✅ Contains business entities and rules
- ✅ No dependencies on any other layer
- ✅ Framework independent
- ❌ No service dependencies
- ❌ No database access
- ❌ No external API calls

---

### Application Layer (`Application/`)

**Purpose**: Use cases, abstractions, and DTOs

```
Application/
│
├── Abstractions/
│   └── IIvantiService.cs              ← Service contracts
│
├── Common/
│   ├── Result.cs                      ← Result wrapper
│   ├── PagedQuery.cs                  ← Query parameters
│   ├── PagedResult.cs                 ← Paged responses
│   └── DResponse.cs                   ← API wrapper
│
├── DTOs/
│   ├── Session/
│   │   ├── SessionDataDto.cs
│   │   └── UserDataDto.cs
│   │
│   ├── Incident/
│   │   ├── IncidentListItemDto.cs
│   │   ├── IncidentDto.cs
│   │   └── IncidentUpdateRequestDto.cs
│   │
│   ├── Workspace/
│   │   └── WorkspaceDto.cs
│   │
│   └── Form/
│       ├── FormViewDataDto.cs
│       ├── FormValidationListDataDto.cs
│       └── FormDefaultDataDto.cs
│
├── Exceptions/
│   ├── ApplicationException.cs
│   ├── ValidationException.cs
│   └── NotFoundException.cs
│
└── Services/                           ← DEPRECATED (see Abstractions/)
    └── [old files - to be removed]
```

**Rules**:
- ✅ Defines service abstractions (interfaces)
- ✅ Contains all DTOs organized by domain
- ✅ Has exception hierarchy
- ✅ Contains business logic and orchestration
- ✅ Framework independent
- ❌ No dependencies on Infrastructure specifics
- ❌ No dependencies on WebUI
- ❌ No database direct access
- ❌ No HTTP client dependencies

### DTO Organization

DTOs are organized by **domain/bounded context**, not by operation type:

```
✅ CORRECT:
Application/DTOs/Incident/
├── IncidentDto.cs
├── IncidentListItemDto.cs
└── IncidentUpdateRequestDto.cs

❌ INCORRECT:
Application/DTOs/Requests/
├── UpdateIncidentRequest.cs
Application/DTOs/Responses/
├── IncidentResponse.cs
```

**DTO Naming Convention**:
- Request DTOs: `[Entity]RequestDto` or `[Entity]CreateDto`, `[Entity]UpdateDto`
- Response DTOs: `[Entity]Dto` or `[Entity]ListDto`
- Query objects: `[Entity]QueryDto`

Examples:
```csharp
// Good
public class IncidentCreateRequestDto
public class IncidentUpdateRequestDto
public class IncidentListItemDto
public class IncidentDto

// Avoid
public class CreateIncidentRequest
public class IncidentResponse
public class GetIncidentDto
```

---

### Infrastructure Layer (`Infrastructure/`)

**Purpose**: Implementation of abstractions and external integrations

```
Infrastructure/
│
├── Services/
│   └── IvantiServiceAdapter.cs        ← Adapts old to new interface
│
├── Ivanti/
│   ├── IvantiClient.cs                ← API client implementation
│   ├── IvantiEndpoints.cs             ← Endpoint URLs
│   ├── Configuration/
│   │   └── IvantiOptions.cs
│   └── Requests/
│       ├── FindFormViewDataRequest.cs
│       ├── GetWorkspaceDataRequest.cs
│       └── [other request types].cs
│
├── Utilities/
│   ├── JsonUnwrapHelper.cs            ← JSON unwrapping
│   ├── ODataQueryBuilder.cs           ← OData query construction
│   └── FormDataMapper.cs              ← Data mapping
│
├── Http/
│   └── HttpClientLoggingHandler.cs    ← HTTP logging
│
├── Mapping/
│   └── MapsterConfig.cs               ← DTO mappings
│
├── Exceptions/
│   ├── IvantiException.cs
│   └── InfrastructureException.cs
│
└── DependencyInjection.cs             ← Service registration
```

**Rules**:
- ✅ Implements abstractions from Application
- ✅ Handles external integrations
- ✅ Contains utility functions
- ✅ Configures mappings and services
- ❌ Does not contain business logic
- ❌ Does not define new abstractions
- ❌ Does not depend on WebUI

---

### WebUI Layer (`WebUI/`)

**Purpose**: User interface and presentation

```
WebUI/
│
├── Features/                          ← Organize by feature
│   │
│   ├── Incidents/
│   │   ├── Pages/
│   │   │   ├── Incidents.razor        ← List view
│   │   │   └── IncidentDetail.razor   ← Detail view
│   │   │
│   │   ├── Dialogs/
│   │   │   ├── EditIncidentDialog.razor
│   │   │   └── CreateIncidentDialog.razor
│   │   │
│   │   └── ViewModels/
│   │       ├── IncidentsViewModel.cs
│   │       └── IncidentDetailViewModel.cs
│   │
│   ├── [OtherFeature]/
│   │   ├── Pages/
│   │   ├── Dialogs/
│   │   └── ViewModels/
│   │
│   └── ...
│
├── Shared/
│   ├── Layouts/
│   │   └── MainLayout.razor
│   │
│   └── Components/
│       ├── Navigation.razor
│       └── ...
│
├── App.razor
├── Program.cs
└── appsettings.json
```

**Rules**:
- ✅ Organize by feature (vertical slicing)
- ✅ Contains Blazor components
- ✅ Contains ViewModels
- ✅ Depends on Application abstractions only
- ❌ No business logic
- ❌ No direct Infrastructure dependencies
- ❌ No direct database access

---

## Naming Conventions

### File Names

Use **lowercase kebab-case** for all files:

```
✅ CORRECT:
user-data-dto.cs
incident-service.cs
form-validation-helper.cs
ivanti-options.cs

❌ INCORRECT:
UserDataDto.cs
IncidentService.cs
FormValidationHelper.cs
IvantiOptions.cs
```

### Class Names

Use **PascalCase** for class names (standard C# convention):

```csharp
✅ CORRECT:
public class UserDataDto
public class IncidentService
public class FormValidationHelper

❌ INCORRECT:
public class user_data_dto
public class incident_service
```

### Folder Names

Use **PascalCase** for folder names (matching namespace convention):

```
✅ CORRECT:
Application/DTOs/
Application/Exceptions/
Infrastructure/Utilities/
WebUI/Features/

❌ INCORRECT:
Application/dtos/
Application/exceptions/
Infrastructure/utilities/
WebUI/features/
```

### Namespace Naming

Namespaces should reflect folder structure:

```csharp
// Folder: Application/DTOs/Incident/
// File: incident-dto.cs
namespace Application.DTOs.Incident;
public class IncidentDto { }

// Folder: Infrastructure/Utilities/
// File: odata-query-builder.cs
namespace Infrastructure.Utilities;
public class ODataQueryBuilder { }

// Folder: WebUI/Features/Incidents/ViewModels/
// File: incidents-view-model.cs
namespace WebUI.Features.Incidents.ViewModels;
public class IncidentsViewModel { }
```

---

## Dependency Injection Pattern

### Service Registration

All services should be registered in `Infrastructure/DependencyInjection.cs`:

```csharp
public static IServiceCollection AddInfrastructure(
    this IServiceCollection services,
    IConfiguration config)
{
    // Options
    services.AddOptions<IvantiOptions>()
        .Bind(config.GetSection("Ivanti"))
        .ValidateDataAnnotations();

    // Logging
    // ... setup Serilog

    // Mapping
    MapsterConfig.RegisterMappings();
    services.AddSingleton(TypeAdapterConfig.GlobalSettings);
    services.AddScoped<IMapper, ServiceMapper>();

    // Services
    services.AddHttpClient<IvantiClient>(...)
    services.AddScoped<IIvantiService, IvantiServiceAdapter>();

    return services;
}
```

**Rules**:
- ✅ Register abstractions, not implementations
- ✅ Use dependency injection, not service locator
- ✅ Group related registrations
- ✅ Keep registrations readable and organized

---

## Exception Handling

### Exception Hierarchy

```
Exception
├── DomainException                    (Domain layer)
│   └── [Domain-specific exceptions]
│
├── ApplicationException               (Application layer)
│   ├── ValidationException
│   ├── NotFoundException
│   └── [Application-specific exceptions]
│
└── InfrastructureException            (Infrastructure layer)
    ├── IvantiException
    └── [Infrastructure-specific exceptions]
```

### Exception Usage Rules

```csharp
// ✅ CORRECT: Throw specific exceptions
public class IncidentService
{
    public async Task<IncidentDto> GetIncidentAsync(string id)
    {
        if (string.IsNullOrEmpty(id))
            throw new ValidationException("Incident ID is required");

        var incident = await _repository.GetAsync(id);
        if (incident == null)
            throw new NotFoundException(nameof(Incident), id);

        return _mapper.Map<IncidentDto>(incident);
    }
}

// ❌ AVOID: Generic exceptions
throw new Exception("Something went wrong");
throw new ApplicationException("Invalid state");
```

---

## DTO and Entity Mapping

### Mapster Configuration

All mappings should be configured in `Infrastructure/Mapping/MapsterConfig.cs`:

```csharp
public static void RegisterMappings()
{
    var config = TypeAdapterConfig.GlobalSettings;

    // Domain Entity → DTO
    config.NewConfig<Incident, IncidentDto>()
        .Map(dest => dest.Status, src => src.Status.ToString());

    config.NewConfig<Incident, IncidentListItemDto>();

    // DTO → Domain Entity
    config.NewConfig<IncidentCreateRequestDto, Incident>();
}
```

**Rules**:
- ✅ All mappings in one place
- ✅ Use expression trees (safe for all contexts)
- ✅ Handle enum conversions explicitly
- ✅ Comment complex mappings

---

## Feature Organization

### Feature Folder Structure

Each feature should follow this structure:

```
WebUI/Features/[FeatureName]/
├── Pages/                    ← Razor components
│   ├── [Feature]List.razor
│   ├── [Feature]Detail.razor
│   └── [Feature]Dialog.razor
│
├── ViewModels/              ← State management
│   ├── [Feature]ViewModel.cs
│   └── [Feature]DetailViewModel.cs
│
├── Models/                  ← Feature-specific models (optional)
│   └── [Feature]ViewModel.cs
│
└── Services/                ← Feature-specific services (optional, prefer Application)
    └── [Feature]Service.cs
```

**Rules**:
- ✅ Group related components together
- ✅ Keep feature self-contained
- ✅ Prefer composition over inheritance
- ✅ One responsibility per ViewModel

---

## Best Practices

### 1. Abstraction First

Always define abstractions before implementations:

```csharp
// Step 1: Define abstraction in Application layer
namespace Application.Abstractions;
public interface IIncidentService
{
    Task<IncidentDto> GetAsync(string id);
}

// Step 2: Implement in Infrastructure layer
namespace Infrastructure.Services;
public class IncidentService : IIncidentService
{
    public async Task<IncidentDto> GetAsync(string id) { }
}

// Step 3: Register in DependencyInjection.cs
services.AddScoped<IIncidentService, IncidentService>();
```

### 2. Result Pattern

Always return `Result<T>` for operations that might fail:

```csharp
public async Task<Result<IncidentDto>> GetIncidentAsync(string id)
{
    if (string.IsNullOrEmpty(id))
        return Result<IncidentDto>.Failure("ID required");

    try
    {
        var incident = await _service.GetAsync(id);
        return Result<IncidentDto>.Success(incident);
    }
    catch (Exception ex)
    {
        _logger.LogError(ex, "Error getting incident");
        return Result<IncidentDto>.Failure(ex.Message);
    }
}
```

### 3. Constructor Injection

Use constructor injection for all dependencies:

```csharp
✅ CORRECT:
public class IncidentViewModel
{
    private readonly IIncidentService _service;
    private readonly ILogger<IncidentViewModel> _logger;

    public IncidentViewModel(IIncidentService service, ILogger<IncidentViewModel> logger)
    {
        _service = service;
        _logger = logger;
    }
}

❌ INCORRECT:
public class IncidentViewModel
{
    public IncidentViewModel()
    {
        _service = ServiceLocator.Get<IIncidentService>();
    }
}
```

### 4. Immutability Where Possible

Keep DTOs immutable:

```csharp
✅ CORRECT:
public class IncidentDto
{
    [JsonPropertyName("recordId")]
    public string? RecordId { get; init; }

    [JsonPropertyName("subject")]
    public string? Subject { get; init; }
}

❌ INCORRECT:
public class IncidentDto
{
    public string? RecordId { get; set; }
    public string? Subject { get; set; }
}
```

### 5. Async/Await

Always use async/await for I/O operations:

```csharp
✅ CORRECT:
public async Task<Result<IncidentDto>> GetAsync(string id, CancellationToken ct)
{
    return await _service.GetIncidentAsync(id, ct);
}

❌ INCORRECT:
public IncidentDto Get(string id)
{
    return _service.GetIncident(id).Result;  // Blocks thread!
}
```

### 6. Logging

Log important operations and errors:

```csharp
✅ CORRECT:
_logger.LogInformation("Fetching incident {IncidentId}", id);
try
{
    var result = await _service.GetAsync(id);
    _logger.LogInformation("Successfully fetched incident {IncidentId}", id);
    return result;
}
catch (Exception ex)
{
    _logger.LogError(ex, "Error fetching incident {IncidentId}", id);
    throw;
}
```

---

## Code Review Checklist

When reviewing code, ensure:

- [ ] Correct layer placement
- [ ] No circular dependencies
- [ ] Proper abstraction usage
- [ ] Naming conventions followed
- [ ] DTOs organized by domain
- [ ] Result pattern used for operations
- [ ] Constructor injection used
- [ ] Proper exception handling
- [ ] Logging implemented
- [ ] Async/await used for I/O
- [ ] Unit testable code
- [ ] No hardcoded values

---

## References

- [Clean Architecture by Robert C. Martin](https://blog.cleancoder.com/uncle-bob/2012/08/13/the-clean-architecture.html)
- [SOLID Principles](https://en.wikipedia.org/wiki/SOLID)
- [Dependency Injection in .NET](https://docs.microsoft.com/en-us/dotnet/core/extensions/dependency-injection)
- [Mapster Documentation](https://github.com/MapsterMapper/Mapster/wiki)

---

**Last Updated**: March 24, 2026
**Version**: 1.0
**Status**: Active
