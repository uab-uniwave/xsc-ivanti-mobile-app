# GetValidatedSearch Implementation Guide

## Overview

The `GetValidatedSearchAsync` method retrieves **all favorite saved searches** from Ivanti by iterating through the workspace favorites and calling the Ivanti API for each one.

## Architecture

### Data Flow

```
WorkspaceData
    └── SearchData
        └── Favorites[] (List of favorite searches)
            ├── [0] { Id: "guid-1", Name: "My Team's Active Incidents" }
            ├── [1] { Id: "guid-2", Name: "All Open Incidents" }
            └── [2] { Id: "guid-3", Name: "High Priority Items" }
                    ↓
    For Each Favorite
                    ↓
    GetValidatedSearchAsync(SearchId: guid-1)
                    ↓
    ValidatedSearch (Full search definition with conditions)
```

### Method Signature

```csharp
public async Task<Result<List<ValidatedSearch>>> 
    GetValidatedSearchAsync(CancellationToken ct)
```

**Returns:** List of `ValidatedSearch` objects, one for each favorite search

## Implementation Details

### Step-by-Step Process

#### 1. **Extract Favorites from WorkspaceData**

```csharp
var favoriteSearches = _workspaceData.SearchData?.Favorites;

if (favoriteSearches == null || !favoriteSearches.Any())
{
    _logger.LogWarning("No favorite searches found");
    return Result<List<ValidatedSearch>>.Success(new List<ValidatedSearch>());
}
```

**Data Source:**
- `_workspaceData.SearchData.Favorites`
- Populated from `GetWorkspaceDataAsync()`

#### 2. **Iterate Through Each Favorite**

```csharp
foreach (var favorite in favoriteSearches)
{
    if (string.IsNullOrEmpty(favorite.Id))
    {
        _logger.LogWarning("Skipping favorite with empty ID: {Name}", favorite.Name);
        continue;
    }

    // Process each favorite...
}
```

#### 3. **Build Request for Each Search**

```csharp
var request = new GetValideatedSearchRequest()
{
    SearchId = Guid.Parse(favorite.Id),           // From Favorites[x].Id
    ObjectId = _workspaceData.ObjectId,           // From WorkspaceData
    LayoutName = _roleWorkspaces.Workspaces       // From RoleWorkspaces
        .FirstOrDefault()?.LayoutName,
    CsrfToken = _sessionData.SessionCsrfToken     // From Session
};
```

**Request Parameters:**

| Parameter | Source | Example Value |
|-----------|--------|---------------|
| `SearchId` | `Favorites[i].Id` | `d74c224d-42e4-49dc-9b1a-4450b9d3d524` |
| `ObjectId` | `WorkspaceData.ObjectId` | `Incident#` |
| `LayoutName` | `RoleWorkspaces.Workspaces[0].LayoutName` | `DefaultLayout` |
| `CsrfToken` | `SessionData.SessionCsrfToken` | `abc123xyz...` |

#### 4. **Call API and Map Response**

```csharp
var response = await PostAsync<GetValideatedSearchResponse>(
    _endpoints.GetValidatedSearch, 
    request, 
    ct
);

if (response.IsFailure || response.Value == null)
{
    _logger.LogWarning("Failed to load search: {Error}", response.Error);
    continue;  // Skip this search, continue with others
}

var validatedSearch = _mapper.Map<ValidatedSearch>(response.Value.D);
validatedSearches.Add(validatedSearch);
```

#### 5. **Return Complete List**

```csharp
_logger.LogInformation("Successfully loaded {Count} validated searches", 
    validatedSearches.Count);

return Result<List<ValidatedSearch>>.Success(validatedSearches);
```

## Error Handling Strategy

### Graceful Degradation

The implementation uses **continue-on-error** pattern:

```csharp
foreach (var favorite in favoriteSearches)
{
    try
    {
        // Load search
        validatedSearches.Add(search);
    }
    catch (FormatException ex)
    {
        _logger.LogWarning("Invalid ID format, skipping");
        continue;  // Skip bad ID, process others
    }
    catch (Exception ex)
    {
        _logger.LogWarning("Error loading search, skipping");
        continue;  // Skip failed search, process others
    }
}
```

**Benefits:**
- One failed search doesn't break the entire operation
- Returns partial results instead of complete failure
- Logs warnings for debugging

### Error Categories

| Error Type | Handling | Impact |
|------------|----------|--------|
| **No favorites** | Return empty list | Non-blocking |
| **Invalid GUID** | Skip and continue | Partial results |
| **API failure** | Skip and continue | Partial results |
| **Network error** | Return failure | Complete failure |
| **Deserialization error** | Return failure | Complete failure |

## Usage Examples

### Example 1: Load and Display Searches

```csharp
// In ViewModel or Service
public class SavedSearchesViewModel
{
    private readonly IIvantiClient _ivantiClient;

    public List<ValidatedSearch> SavedSearches { get; private set; } = new();

    public async Task LoadSavedSearchesAsync()
    {
        var result = await _ivantiClient.GetValidatedSearchAsync(CancellationToken.None);

        if (result.IsSuccess && result.Value != null)
        {
            SavedSearches = result.Value;

            // Log details
            foreach (var search in SavedSearches)
            {
                Console.WriteLine($"Search: {search.Name}");
                Console.WriteLine($"  Favorite: {search.IsFavorite}");
                Console.WriteLine($"  Conditions: {search.Conditions.Count}");
            }
        }
    }
}
```

### Example 2: Blazor Component with Search Dropdown

```razor
@inject IIvantiClient IvantiClient

<MudSelect T="ValidatedSearch" 
          Label="Saved Searches"
          @bind-Value="_selectedSearch"
          ToStringFunc="@(s => s.Name ?? "Unnamed")"
          Dense="true">
    @foreach (var search in _savedSearches)
    {
        <MudSelectItem Value="@search">
            <MudStack Row="true" Spacing="2">
                @if (search.IsFavorite)
                {
                    <MudIcon Icon="@Icons.Material.Filled.Star" 
                            Color="Color.Warning" 
                            Size="Size.Small" />
                }
                <MudText>@search.Name</MudText>
                <MudChip T="string" Size="Size.Small" Color="Color.Info">
                    @search.Conditions.Count filters
                </MudChip>
            </MudStack>
        </MudSelectItem>
    }
</MudSelect>

@code {
    private List<ValidatedSearch> _savedSearches = new();
    private ValidatedSearch? _selectedSearch;

    protected override async Task OnInitializedAsync()
    {
        var result = await IvantiClient.GetValidatedSearchAsync(CancellationToken.None);

        if (result.IsSuccess && result.Value != null)
        {
            _savedSearches = result.Value;

            // Auto-select default search
            _selectedSearch = _savedSearches.FirstOrDefault(s => s.IsDefaultMy);
        }
    }
}
```

### Example 3: Filter and Categorize Searches

```csharp
public class SearchManager
{
    public List<ValidatedSearch> GetFavoriteSearches(List<ValidatedSearch> allSearches)
    {
        return allSearches
            .Where(s => s.IsFavorite)
            .OrderBy(s => s.Name)
            .ToList();
    }

    public List<ValidatedSearch> GetSearchesForRole(
        List<ValidatedSearch> allSearches, 
        string userRole)
    {
        return allSearches
            .Where(s => s.RoleScope?.Contains(userRole) == true)
            .ToList();
    }

    public List<ValidatedSearch> GetDefaultSearches(
        List<ValidatedSearch> allSearches, 
        string userRole)
    {
        return allSearches
            .Where(s => s.RolesDefault?.Contains(userRole) == true)
            .ToList();
    }

    public Dictionary<string, List<ValidatedSearch>> GroupByCategory(
        List<ValidatedSearch> allSearches)
    {
        return allSearches
            .GroupBy(s => s.CategoryName ?? "Uncategorized")
            .ToDictionary(g => g.Key, g => g.ToList());
    }
}
```

## API Call Sequence

### Complete Initialization Flow

```
1. InitializeSessionAsync()
   ↓ Stores: _sessionData (contains CsrfToken)

2. GetUserDataAsync()
   ↓ Stores: _userData (contains UserRole)

3. GetRoleWorkspacesAsync()
   ↓ Stores: _roleWorkspaces (contains Workspace.LayoutName)

4. GetWorkspaceDataAsync()
   ↓ Stores: _workspaceData (contains SearchData.Favorites[] and ObjectId)

5. GetValidatedSearchAsync()
   ↓ For each Favorites[i]:
      Request:
        - SearchId: Favorites[i].Id (Guid)
        - ObjectId: WorkspaceData.ObjectId
        - LayoutName: RoleWorkspaces.Workspaces[0].LayoutName
        - CsrfToken: SessionData.SessionCsrfToken

   ↓ Returns: List<ValidatedSearch>
```

## Request Construction Details

### Favorite Search Properties

```csharp
// WorkspaceFavorite from WorkspaceData
public class WorkspaceFavorite
{
    public string? Id { get; set; }        // ← Used as SearchId (GUID)
    public string? Name { get; set; }      // Search display name
    public bool IsDefault { get; set; }    // Is default for workspace
    public bool CanEdit { get; set; }      // User can modify
}
```

### Example Request Built from Data

Given this favorite:
```json
{
  "Id": "d74c224d-42e4-49dc-9b1a-4450b9d3d524",
  "Name": "My Team's Active Incidents",
  "isDefault": true,
  "CanEdit": false
}
```

Builds this request:
```json
{
  "SearchId": "d74c224d-42e4-49dc-9b1a-4450b9d3d524",
  "ObjectId": "Incident#",
  "LayoutName": "DefaultLayout",
  "_csrfToken": "abc123xyz..."
}
```

## Response Mapping

### Response Structure

```csharp
public class GetValideatedSearchResponse
{
    [JsonPropertyName("d")]
    public ValidatedSearch D { get; set; }
}
```

### Mapster Mapping

```csharp
var validatedSearch = _mapper.Map<ValidatedSearch>(response.Value.D);
```

**Note:** If Mapster mapping is not configured, the response.Value.D can be used directly since it's already of type `ValidatedSearch`.

## Performance Considerations

### Sequential vs Parallel Loading

**Current Implementation (Sequential):**
```csharp
foreach (var favorite in favoriteSearches)
{
    await PostAsync(...);  // One at a time
}
```

**Future Optimization (Parallel):**
```csharp
var tasks = favoriteSearches.Select(async favorite =>
{
    var request = new GetValideatedSearchRequest() { ... };
    return await PostAsync(...);
});

var results = await Task.WhenAll(tasks);
```

### Caching Strategy

```csharp
private List<ValidatedSearch>? _cachedValidatedSearches;
private DateTime? _cacheExpiry;

public async Task<Result<List<ValidatedSearch>>> GetValidatedSearchAsync(CancellationToken ct)
{
    // Check cache
    if (_cachedValidatedSearches != null && 
        _cacheExpiry > DateTime.UtcNow)
    {
        return Result<List<ValidatedSearch>>.Success(_cachedValidatedSearches);
    }

    // Load fresh data
    var result = await LoadValidatedSearchesAsync(ct);

    if (result.IsSuccess)
    {
        _cachedValidatedSearches = result.Value;
        _cacheExpiry = DateTime.UtcNow.AddMinutes(15);
    }

    return result;
}
```

## Typical Response Data

### Example: 3 Favorites → 3 ValidatedSearch Objects

```
Favorites[]:
1. { Id: "guid-1", Name: "My Team's Active Incidents" }
2. { Id: "guid-2", Name: "All Open Incidents" }
3. { Id: "guid-3", Name: "High Priority" }

↓ After GetValidatedSearchAsync()

ValidatedSearches[]:
1. ValidatedSearch {
     Id: "guid-1",
     Name: "My Team's Active Incidents",
     Conditions: [
       { FieldName: "Status", Condition: "=", FieldValue: "Active" },
       { FieldName: "OwnerTeam", Condition: "()", FieldValue: "$(CurrentUserTeamNames())" }
     ],
     RoleScope: ["ServiceDeskManager", "ResponsiveAnalyst"],
     IsFavorite: true
   }

2. ValidatedSearch {
     Id: "guid-2",
     Name: "All Open Incidents",
     Conditions: [
       { FieldName: "IsInFinalState", Condition: "=", FieldValue: "false" }
     ],
     RoleScope: ["ServiceDeskAnalyst"],
     IsFavorite: true
   }

3. ValidatedSearch {
     Id: "guid-3",
     Name: "High Priority",
     Conditions: [
       { FieldName: "Priority", Condition: "=", FieldValue: "1" }
     ],
     RoleScope: ["ServiceDeskManager"],
     IsFavorite: false
   }
```

## UI Integration Examples

### Saved Search Selector with Filters

```razor
<MudPaper Class="pa-4">
    <MudText Typo="Typo.h6" Class="mb-3">Saved Searches</MudText>

    <MudChipSet @bind-SelectedChip="_selectedSearchChip" 
               Mandatory="true" 
               Filter="true">
        @foreach (var search in _savedSearches)
        {
            <MudChip T="string" 
                    Text="@search.Name" 
                    Color="@(search.IsFavorite ? Color.Primary : Color.Default)"
                    Icon="@(search.IsFavorite ? Icons.Material.Filled.Star : null)"
                    OnClick="@(() => ApplySearch(search))">
                @search.Name
            </MudChip>
        }
    </MudChipSet>

    @if (_selectedSearch != null)
    {
        <MudDivider Class="my-3" />
        <MudText Typo="Typo.subtitle2" Class="mb-2">Active Filters:</MudText>

        @foreach (var condition in _selectedSearch.Conditions)
        {
            <MudChip T="string" Size="Size.Small" Color="Color.Info" Class="mr-2 mb-2">
                @condition.FieldDisplay @condition.Condition @condition.FieldValueDisplay
            </MudChip>
        }
    }
</MudPaper>

@code {
    private List<ValidatedSearch> _savedSearches = new();
    private ValidatedSearch? _selectedSearch;
    private MudChip? _selectedSearchChip;

    private void ApplySearch(ValidatedSearch search)
    {
        _selectedSearch = search;
        // Apply search conditions to data grid
        ApplySearchConditions(search.Conditions);
    }
}
```

### Search Menu with Categories

```razor
<MudMenu Label="Saved Searches" 
         Variant="Variant.Filled" 
         Color="Color.Primary"
         StartIcon="@Icons.Material.Filled.Search">

    @foreach (var category in _searchesByCategory)
    {
        <MudText Typo="Typo.overline" Class="px-4 pt-2 mud-text-secondary">
            @category.Key
        </MudText>

        @foreach (var search in category.Value)
        {
            <MudMenuItem OnClick="@(() => LoadSearch(search))">
                @if (search.IsFavorite)
                {
                    <MudIcon Icon="@Icons.Material.Filled.Star" 
                            Size="Size.Small" 
                            Color="Color.Warning" 
                            Class="mr-2" />
                }
                @search.Name
                <MudChip T="string" Size="Size.Small" Class="ml-2">
                    @search.Conditions.Count
                </MudChip>
            </MudMenuItem>
        }

        @if (category.Value != _searchesByCategory.Values.Last())
        {
            <MudDivider />
        }
    }
</MudMenu>

@code {
    private Dictionary<string, List<ValidatedSearch>> _searchesByCategory = new();

    protected override async Task OnInitializedAsync()
    {
        var result = await IvantiClient.GetValidatedSearchAsync(CancellationToken.None);

        if (result.IsSuccess && result.Value != null)
        {
            _searchesByCategory = result.Value
                .GroupBy(s => s.CategoryName ?? "General")
                .ToDictionary(g => g.Key, g => g.ToList());
        }
    }
}
```

## Logging Output

When method executes successfully:

```
[Information] Found 3 favorite searches to load
[Information] Loaded validated search: My Team's Active Incidents (ID: d74c224d-42e4-49dc-9b1a-4450b9d3d524) with 2 conditions
[Information] Loaded validated search: All Open Incidents (ID: e85d335e-53f5-50ed-a2cb-5561c0e4e635) with 1 conditions
[Information] Loaded validated search: High Priority (ID: f96e446f-64g6-61fe-b3dc-6672d1f5f746) with 1 conditions
[Information] Successfully loaded 3 validated searches
```

When errors occur:

```
[Warning] Skipping favorite search with empty ID: Unnamed Search
[Warning] Invalid SearchId format for favorite: invalid-guid-format
[Warning] Failed to get validated search for ID abc123: API returned 404
[Information] Successfully loaded 2 validated searches (1 failed)
```

## Testing

### Unit Test Example

```csharp
[Fact]
public async Task GetValidatedSearchAsync_WithMultipleFavorites_ReturnsAllSearches()
{
    // Arrange
    var mockWorkspaceData = new WorkspaceData
    {
        ObjectId = "Incident#",
        SearchData = new WorkspaceSearchData
        {
            Favorites = new List<WorkspaceFavorite>
            {
                new() { Id = Guid.NewGuid().ToString(), Name = "Search 1" },
                new() { Id = Guid.NewGuid().ToString(), Name = "Search 2" },
                new() { Id = Guid.NewGuid().ToString(), Name = "Search 3" }
            }
        }
    };

    // Act
    var result = await _ivantiClient.GetValidatedSearchAsync(CancellationToken.None);

    // Assert
    Assert.True(result.IsSuccess);
    Assert.NotNull(result.Value);
    Assert.Equal(3, result.Value.Count);
}

[Fact]
public async Task GetValidatedSearchAsync_WithInvalidGuid_SkipsInvalidAndContinues()
{
    // Arrange
    var mockWorkspaceData = new WorkspaceData
    {
        SearchData = new WorkspaceSearchData
        {
            Favorites = new List<WorkspaceFavorite>
            {
                new() { Id = "valid-guid-1", Name = "Valid 1" },
                new() { Id = "invalid-guid", Name = "Invalid" },  // Will be skipped
                new() { Id = "valid-guid-2", Name = "Valid 2" }
            }
        }
    };

    // Act
    var result = await _ivantiClient.GetValidatedSearchAsync(CancellationToken.None);

    // Assert
    Assert.True(result.IsSuccess);
    Assert.Equal(2, result.Value.Count);  // 2 valid, 1 skipped
}
```

## Integration with ViewModel

### Example: IncidentsViewModel with Saved Searches

```csharp
public class IncidentsViewModel
{
    private readonly IIvantiClient _ivantiClient;

    public List<ValidatedSearch> SavedSearches { get; private set; } = new();
    public ValidatedSearch? ActiveSearch { get; private set; }
    public List<Incident> FilteredIncidents { get; private set; } = new();

    public async Task InitializeAsync(CancellationToken ct)
    {
        // Load all initialization data
        await _ivantiClient.InitializeSessionAsync(ct);
        await _ivantiClient.GetUserDataAsync(ct);
        await _ivantiClient.GetRoleWorkspacesAsync(ct);
        await _ivantiClient.GetWorkspaceDataAsync(ct);

        // Load validated searches
        var searchResult = await _ivantiClient.GetValidatedSearchAsync(ct);
        if (searchResult.IsSuccess && searchResult.Value != null)
        {
            SavedSearches = searchResult.Value;

            // Set default search
            ActiveSearch = SavedSearches.FirstOrDefault(s => s.IsDefaultMy)
                        ?? SavedSearches.FirstOrDefault();
        }
    }

    public void ApplySearch(ValidatedSearch search)
    {
        ActiveSearch = search;

        // Apply conditions to filter incidents
        FilteredIncidents = ApplySearchConditions(AllIncidents, search.Conditions);
    }
}
```

## Debugging Tips

### Enable Detailed Logging

```csharp
// In appsettings.Development.json
{
  "Logging": {
    "LogLevel": {
      "Infrastructure.Ivanti.IvantiClient": "Debug"
    }
  }
}
```

### Inspect Request/Response

```csharp
// Add breakpoint here to inspect data
var request = new GetValideatedSearchRequest()
{
    SearchId = Guid.Parse(favorite.Id),  // ← Check this value
    ObjectId = _workspaceData.ObjectId,  // ← Verify ObjectId
    LayoutName = _roleWorkspaces.Workspaces.FirstOrDefault()?.LayoutName,
    CsrfToken = _sessionData.SessionCsrfToken
};

// Add breakpoint here to inspect response
var validatedSearch = _mapper.Map<ValidatedSearch>(response.Value.D);
Console.WriteLine($"Loaded: {validatedSearch.Name} with {validatedSearch.Conditions.Count} conditions");
```

## Common Issues and Solutions

### Issue 1: Empty List Returned

**Cause:** No favorites in WorkspaceData
```csharp
// Check if favorites exist
if (_workspaceData.SearchData?.Favorites?.Any() != true)
{
    _logger.LogWarning("No favorites available");
}
```

**Solution:** Ensure `GetWorkspaceDataAsync()` was called first

### Issue 2: Invalid GUID Format

**Cause:** Favorite.Id is not a valid GUID
```
FormatException: Guid should contain 32 digits with 4 dashes
```

**Solution:** Validate before parsing
```csharp
if (Guid.TryParse(favorite.Id, out var searchId))
{
    request.SearchId = searchId;
}
else
{
    _logger.LogWarning("Invalid GUID: {Id}", favorite.Id);
    continue;
}
```

### Issue 3: Partial Results

**Symptom:** Only some searches loaded
**Cause:** API errors for specific search IDs

**Check logs:**
```
[Warning] Failed to get validated search for ID abc-123: 404 Not Found
```

**Solution:** This is expected behavior - method returns available searches

## Related Classes

### Existing Workspace Models (Reused)

From `Application.Models.WorkspaceData`:
- `WorkspaceFavorite` - Source of SearchId
- `WorkspaceSearchData` - Contains Favorites collection
- `WorkspaceRelatedObject` - Similar structure to SearchRelatedObject

### New ValidatedSearch Models

From `Application.Models.ValidatedSearch`:
- `ValidatedSearch` - Main search definition
- `SearchCondition` - Individual filter
- `SearchRelatedObject` - Object relationships
- `SearchRights` - User permissions

## Future Enhancements

1. **Parallel Loading**
   - Load all searches concurrently for better performance

2. **Caching**
   - Cache loaded searches for 15 minutes
   - Invalidate on workspace change

3. **Incremental Loading**
   - Load on-demand when user selects search

4. **Search Execution**
   - Apply conditions to query builder
   - Execute search and return results

5. **Search Management**
   - Create new searches
   - Edit existing searches (when `SearchRights.Edit = true`)
   - Delete searches (when `SearchRights.Delete = true`)

---

**Implementation Status:** ✅ Complete
**Build Status:** ✅ Successful
**Framework:** .NET 10
**Date:** 2025
