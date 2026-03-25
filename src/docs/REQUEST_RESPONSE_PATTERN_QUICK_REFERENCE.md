# Request/Response Pattern Architecture Guidelines

## Quick Reference

### Pattern Overview
```
Request DTO  → [External API Call] → Response DTO
```

### Folder Organization
```
Application/DTOs/[Domain]/
├── [operation]-request-dto.cs
└── [operation]-response-dto.cs
```

### Naming Convention
| Purpose | Pattern | Example |
|---------|---------|---------|
| Sent to API | `[Operation]RequestDto` | `SessionInitializeRequestDto` |
| Received from API | `[Operation]ResponseDto` | `SessionInitializeResponseDto` |
| Domain Entity | `[Entity]Dto` | `IncidentDto` |
| List Item | `[Entity]ListItemDto` | `IncidentListItemDto` |

### Code Template

```csharp
// 1. Request DTO - What we send
namespace Application.DTOs.Session;

public class SessionInitializeRequestDto
{
    [JsonPropertyName("_csrfToken")]
    public string? CsrfToken { get; init; }
}

// 2. Response DTO - What we receive
namespace Application.DTOs.Session;

public class SessionInitializeResponseDto
{
    [JsonPropertyName("SessionCsrfToken")]
    public string? SessionCsrfToken { get; init; }

    [JsonPropertyName("UserName")]
    public string? UserName { get; init; }
}

// 3. Service Method - Infrastructure Layer
public async Task<Result<SessionInitializeResponseDto>> InitializeSessionAsync(
    SessionInitializeRequestDto request,
    CancellationToken ct)
{
    var response = await PostAsync<JsonElement>(_endpoints.SessionData, request, ct);
    if (response.IsFailure)
        return Result<SessionInitializeResponseDto>.Failure(response.Error!);

    var data = JsonUnwrapHelper.Deserialize<SessionInitializeResponseDto>(
        response.Value?.ToString() ?? "{}");

    return data == null
        ? Result<SessionInitializeResponseDto>.Failure("Invalid response")
        : Result<SessionInitializeResponseDto>.Success(data);
}
```

### Key Rules

✅ **DO**:
- Create Request and Response DTOs as a pair
- Store both in same folder: `Application/DTOs/[Domain]/`
- Make properties immutable (`init` accessors)
- Include `[JsonPropertyName]` attributes
- Use Result<ResponseDto> return type
- Mirror external API response structure in Response DTO
- Accept Request DTO as method parameter

❌ **DON'T**:
- Mix request/response in single DTO
- Expose raw API response directly
- Share DTOs between different operations
- Put business logic in DTOs
- Have Response DTO inherit from Request DTO
- Use `set` accessors on API DTOs

### Response DTO Properties

Response DTOs use `init` accessor (immutable after construction):
```csharp
public class SessionInitializeResponseDto
{
    // ✅ Correct
    public string? SessionCsrfToken { get; init; }
    
    // ❌ Wrong
    public string? SessionCsrfToken { get; set; }
}
```

### Nested Types in Response

For complex responses with nested objects, define them in the same file:
```csharp
public class RoleWorkspacesResponseDto
{
    public List<RoleWorkspaceItemDto> Workspaces { get; init; }
}

public class RoleWorkspaceItemDto
{
    public string Id { get; init; }
    public string Name { get; init; }
}
```

### Default Values for Collections

Initialize collection properties to prevent null:
```csharp
public class SessionInitializeResponseDto
{
    // ✅ Correct
    [JsonPropertyName("AvailableLanguages")]
    public List<AvailableLanguageDto> AvailableLanguages { get; init; } = new();
    
    // ❌ Wrong
    public List<AvailableLanguageDto>? AvailableLanguages { get; init; }
}
```

### Service Integration Example

```csharp
// Before (without Request/Response)
var payload = new { _csrfToken = (string?)null };
var response = await PostAsync<JsonElement>(_endpoints.SessionData, payload, ct);

// After (with Request/Response)
var request = new SessionInitializeRequestDto { CsrfToken = null };
var response = await PostAsync<JsonElement>(_endpoints.SessionData, request, ct);

// Convert to Response DTO
var sessionResponse = JsonUnwrapHelper.Deserialize<SessionInitializeResponseDto>(
    response.Value?.ToString() ?? "{}");

return sessionResponse == null
    ? Result<SessionInitializeResponseDto>.Failure("Invalid response")
    : Result<SessionInitializeResponseDto>.Success(sessionResponse);
```

### Current API Operations

| Operation | Request DTO | Response DTO |
|-----------|-------------|--------------|
| Initialize Session | `SessionInitializeRequestDto` | `SessionInitializeResponseDto` |
| Get User Data | `UserDataRequestDto` | `UserDataResponseDto` |
| Get Role Workspaces | `RoleWorkspacesRequestDto` | `RoleWorkspacesResponseDto` |
| Get Workspace Data | `WorkspaceDataRequestDto` | `WorkspaceDataResponseDto` |
| Find Form View | `FindFormViewRequestDto` | `FindFormViewResponseDto` |
| Get Form Default Data | `FormDefaultDataRequestDto` | `FormDefaultDataResponseDto` |

---

For detailed folder structure and file organization, see `REQUEST_RESPONSE_PATTERN_SUMMARY.md`
