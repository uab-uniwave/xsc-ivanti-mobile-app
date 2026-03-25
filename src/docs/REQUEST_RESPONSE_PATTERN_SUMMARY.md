# Request/Response Pattern Implementation Summary

## Overview
This document summarizes the implementation of the Request/Response pattern for Ivanti API integrations following Clean Architecture principles.

## New Request/Response DTOs Created

### Session Domain
- `Application/DTOs/Session/session-initialize-request-dto.cs`
  - `SessionInitializeRequestDto`
- `Application/DTOs/Session/session-initialize-response-dto.cs`
  - `SessionInitializeResponseDto`
  - `AvailableLanguageDto`

- `Application/DTOs/Session/user-data-request-dto.cs`
  - `UserDataRequestDto`
- `Application/DTOs/Session/user-data-response-dto.cs`
  - `UserDataResponseDto`

### Workspace Domain
- `Application/DTOs/Workspace/role-workspaces-request-dto.cs`
  - `RoleWorkspacesRequestDto`
- `Application/DTOs/Workspace/role-workspaces-response-dto.cs`
  - `RoleWorkspacesResponseDto` (with all nested types)

- `Application/DTOs/Workspace/workspace-data-request-dto.cs`
  - `WorkspaceDataRequestDto`
- `Application/DTOs/Workspace/workspace-data-response-dto.cs`
  - `WorkspaceDataResponseDto` (with all nested types)

### Form Domain
- `Application/DTOs/Form/find-form-view-request-dto.cs`
  - `FindFormViewRequestDto`
- `Application/DTOs/Form/find-form-view-response-dto.cs`
  - `FindFormViewResponseDto`

- `Application/DTOs/Form/form-default-data-request-dto.cs`
  - `FormDefaultDataRequestDto`
- `Application/DTOs/Form/form-default-data-response-dto.cs`
  - `FormDefaultDataResponseDto` (with all nested types)

## Files to Remove/Consolidate

### Duplicate/Deprecated DTOs in Wrong Folders

1. **SessionData Folder DTOs** (consolidate into Session folder):
   - `Application/DTOs/SessionData/SessionDataDto.cs`
   - `Application/DTOs/SessionData/SessionDataRequest.cs`
   
   **Action**: These should be replaced by:
   - `Application/DTOs/Session/session-initialize-request-dto.cs`
   - `Application/DTOs/Session/session-initialize-response-dto.cs`
   - `Application/DTOs/Session/user-data-request-dto.cs`
   - `Application/DTOs/Session/user-data-response-dto.cs`

2. **UserData Folder DTOs** (consolidate into Session folder):
   - `Application/DTOs/UserData/UserDataDto.cs`
   
   **Action**: Replace with `Application/DTOs/Session/user-data-response-dto.cs`

3. **WorkspaceData Folder DTOs** (consolidate into Workspace folder):
   - `Application/DTOs/WorkspaceData/WorkspaceDataDto.cs`
   
   **Action**: Replace with `Application/DTOs/Workspace/workspace-data-response-dto.cs`

4. **RoleWorkspace Folder DTOs** (consolidate into Workspace folder):
   - `Application/DTOs/RoleWorkspace/RoleWorkspacesDto.cs`
   
   **Action**: Replace with `Application/DTOs/Workspace/role-workspaces-response-dto.cs`

5. **FormViewData Folder DTOs** (consolidate into Form folder):
   - `Application/DTOs/FormViewData/FindFormViewDataDto.cs`
   
   **Action**: Replace with `Application/DTOs/Form/find-form-view-response-dto.cs`

6. **FormDefaultData Folder DTOs** (consolidate into Form folder):
   - `Application/DTOs/FormDefaultData/FormDefaultDataDto.cs`
   
   **Action**: Replace with `Application/DTOs/Form/form-default-data-response-dto.cs`

7. **Duplicate DTOs in Form Folder**:
   - `Application/DTOs/Form/FormViewDataDto.cs` - Remove (redundant with response DTO)

8. **Duplicate DTOs in Workspace Folder**:
   - `Application/DTOs/Workspace/RoleWorkspacesDto.cs` - Remove (replaced by response DTO)
   - `Application/DTOs/Workspace/WorkspaceDataDto.cs` - Remove (replaced by response DTO)

### Duplicate Session DTOs
- `Application/DTOs/Session/SessionDataDto.cs` - Consolidate with response DTO
- `Application/DTOs/Session/UserDataDto.cs` - Consolidate with response DTO

## Clean Folder Structure After Consolidation

```
Application/DTOs/
├── Session/
│   ├── session-initialize-request-dto.cs
│   ├── session-initialize-response-dto.cs
│   ├── user-data-request-dto.cs
│   └── user-data-response-dto.cs
│
├── Workspace/
│   ├── role-workspaces-request-dto.cs
│   ├── role-workspaces-response-dto.cs
│   ├── workspace-data-request-dto.cs
│   └── workspace-data-response-dto.cs
│
├── Form/
│   ├── find-form-view-request-dto.cs
│   ├── find-form-view-response-dto.cs
│   ├── form-default-data-request-dto.cs
│   └── form-default-data-response-dto.cs
│
├── Incident/
│   ├── incident-dto.cs
│   ├── incident-list-item-dto.cs
│   ├── incident-create-request-dto.cs
│   └── incident-update-request-dto.cs
│
└── Common/
    └── [shared DTOs across domains]
```

## Migration Path for IvantiClient

### Before (Current):
```csharp
public async Task<Result<SessionData>> InitializeSessionAsync(CancellationToken ct)
{
    var response = await PostAsync<JsonElement>(_endpoints.SessionData,
        payload: new { _csrfToken = (string?)null },
        ct);
    // ... processing
}
```

### After (Using Request/Response):
```csharp
public async Task<Result<SessionInitializeResponseDto>> InitializeSessionAsync(
    SessionInitializeRequestDto request,
    CancellationToken ct)
{
    var response = await PostAsync<JsonElement>(_endpoints.SessionData, request, ct);
    if (response.IsFailure)
        return Result<SessionInitializeResponseDto>.Failure(response.Error!);
    
    var sessionData = JsonUnwrapHelper.Deserialize<SessionInitializeResponseDto>(
        response.Value?.ToString() ?? "{}");
    
    return sessionData == null 
        ? Result<SessionInitializeResponseDto>.Failure("Invalid response")
        : Result<SessionInitializeResponseDto>.Success(sessionData);
}
```

## Next Steps

1. **Update IvantiClient.cs**:
   - Change method signatures to use Request/Response DTOs
   - Update all method calls with proper request objects
   - Change return types from generic models to response DTOs

2. **Update IIvantiService.cs interface**:
   - Update method signatures to match IvantiClient
   - Use Request/Response DTOs

3. **Update MapsterConfig.cs**:
   - Add mappings for new Request/Response DTOs
   - Remove mappings for deprecated DTOs
   - Update DTO references to use new consolidated locations

4. **Remove deprecated DTO files**:
   - Delete files listed in "Files to Remove/Consolidate" section
   - Remove old folder structures (SessionData, UserData, WorkspaceData, etc.)

5. **Update all service consumers**:
   - Update web UI ViewModels to use new service signatures
   - Update any other code that depends on old DTOs

## Benefits of This Pattern

✅ **Clear Separation**: Request and Response DTOs are explicit and separated
✅ **Scalability**: Easy to add new operations with their own Request/Response pair
✅ **Maintainability**: DTOs organized by domain context, not by operation type
✅ **Consistency**: All external API integrations follow same pattern
✅ **Type Safety**: Strong typing with immutable properties
✅ **API Independence**: Response DTOs can be evolved without affecting requests

---

**Implementation Status**: Request/Response DTOs created
**Next Action**: Update IvantiClient to use these DTOs
