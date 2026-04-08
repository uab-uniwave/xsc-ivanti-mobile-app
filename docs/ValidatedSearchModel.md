# ValidatedSearch Model Documentation

## Overview

The `ValidatedSearch` namespace contains model classes for handling Ivanti saved search definitions and search conditions. These models map to the Ivanti API's search and query functionality.

## File Location

```
src/Application/Models/ValidatedSearch/ValidatedSearch.cs
```

## Class Hierarchy

```
ValidatedSearchData (Root)
├── SearchCondition[] (Conditions)
│   └── SearchRelatedObject[] (RelatedObjects)
└── SearchRights (Permissions)
```

## Classes

### 1. ValidatedSearchData

The main class representing a complete saved search definition from Ivanti.

#### Properties

| Property | Type | JSON Name | Description |
|----------|------|-----------|-------------|
| `Type` | `string?` | `__type` | The metadata type identifier |
| `ExtType` | `string?` | `ExtType` | Extension type (e.g., "search-save") |
| `IsValid` | `bool` | `IsValid` | Indicates if the search definition is valid |
| `Id` | `string?` | `Id` | Unique identifier (GUID) |
| `CategoryName` | `string?` | `CategoryName` | Category of the search |
| `Name` | `string?` | `Name` | Display name of the saved search |
| `IsFavorite` | `bool` | `isFavorite` | Whether this is a favorite search |
| `RolesDefault` | `List<string>` | `RolesDefault` | Roles that have this as default search |
| `IsDefaultMy` | `bool` | `isDefaultMy` | Whether this is the user's default search |
| `ObjectId` | `string?` | `ObjectId` | Target object type (e.g., "Incident#") |
| `Description` | `string?` | `Description` | Search description |
| `OwnerId` | `string?` | `OwnerId` | Owner user ID |
| `Query` | `string?` | `Query` | **Serialized JSON string** containing search query |
| `Conditions` | `List<SearchCondition>` | `Conditions` | Parsed search conditions |
| `RoleScope` | `List<string>` | `RoleScope` | Roles with access to this search |
| `TeamScopes` | `object?` | `TeamScopes` | Team-based access scopes |
| `TeamDefaults` | `object?` | `TeamDefaults` | Team default settings |
| `RoleScopeLabel` | `string?` | `RoleScopelabel` | Display label for role scope |
| `PureName` | `string?` | `pureName` | Pure/clean name of the search |
| `SearchRights` | `SearchRights?` | `SearchRights` | User permissions for this search |
| `CanExport` | `bool` | `CanExport` | Whether results can be exported |
| `CanEmail` | `bool` | `CanEmail` | Whether results can be emailed |
| `SortInfo` | `string?` | `SortInfo` | Serialized sort configuration |
| `SortFields` | `List<object>` | `SortFields` | Sort field definitions |
| `LastModified` | `string?` | `LastModified` | Last modification date (JSON date format) |
| `CreatedBy` | `string?` | `CreatedBy` | Creator username |
| `LastModBy` | `string?` | `LastModBy` | Last modifier username |
| `HiddenExpression` | `string?` | `HiddenExpression` | Expression for conditional visibility |
| `Permissions` | `int` | `Permissions` | Numeric permission flags |

#### Usage Example

```csharp
var result = await _ivantiClient.GetValidatedSearchAsync(cancellationToken);

if (result.IsSuccess && result.Value != null)
{
    var search = result.Value;
    Console.WriteLine($"Search: {search.Name}");
    Console.WriteLine($"Object: {search.ObjectId}");
    Console.WriteLine($"Conditions: {search.Conditions.Count}");

    // Check permissions
    if (search.SearchRights?.Edit == true)
    {
        // User can edit this search
    }
}
```

### 2. SearchCondition

Represents an individual search filter/condition within a saved search.

#### Properties

| Property | Type | JSON Name | Description |
|----------|------|-----------|-------------|
| `Type` | `string?` | `__type` | Metadata type |
| `ObjectId` | `string?` | `ObjectId` | Object being queried (e.g., "Incident#") |
| `ObjectDisplay` | `string?` | `ObjectDisplay` | Display name of the object |
| `JoinRule` | `string?` | `JoinRule` | Logical operator ("AND", "OR") |
| `Condition` | `string?` | `Condition` | Comparison operator ("=", "!=", "<", etc.) |
| `ConditionType` | `int` | `ConditionType` | Type of condition (0 = ByField) |
| `FieldName` | `string?` | `FieldName` | Field being filtered |
| `FieldDisplay` | `string?` | `FieldDisplay` | Display name of field |
| `FieldAlias` | `string?` | `FieldAlias` | Field alias if any |
| `FieldType` | `string?` | `FieldType` | Data type ("list", "text", "date", etc.) |
| `FieldValue` | `string?` | `FieldValue` | Filter value or expression |
| `FieldValueDisplay` | `string?` | `FieldValueDisplay` | Display value |
| `FieldValueBehavior` | `string?` | `FieldValueBehavior` | "single" or "list" |
| `FieldStartValue` | `string?` | `FieldStartValue` | Range start (for between queries) |
| `FieldEndValue` | `string?` | `FieldEndValue` | Range end (for between queries) |
| `BracketLevel` | `int` | `BracketLevel` | Grouping parenthesis level |
| `IsClosingBracket` | `bool` | `IsClosingBracket` | Whether this closes a bracket group |
| `IsRelatedObjectQuery` | `bool` | `IsRelatedObjectQuery` | Whether this queries related objects |
| `RelatedObjectId` | `string?` | `RelatedObjectId` | Related object identifier |
| `RelatedObjectDisplay` | `string?` | `RelatedObjectDisplay` | Related object display name |
| `RelatedObjectOp` | `string?` | `RelatedObjectOp` | Related object operation |
| `RelatedObjectCount` | `int` | `RelatedObjectCount` | Count of related objects |
| `RelatedObjects` | `List<SearchRelatedObject>` | `RelatedObjects` | Available related objects |
| `RelatedRelatedObjects` | `object?` | `RelatedRelatedObjects` | Nested related objects |
| `MasterObjectId` | `string?` | `MasterObjectId` | Master object reference |
| `IsRelatedObjectCondition` | `bool` | `IsRelatedObjectCondition` | Whether condition is on related object |
| `SubQuery` | `object?` | `SubQuery` | Nested sub-query |

#### Example Condition

**Simple Field Filter:**
```json
{
  "FieldName": "Status",
  "Condition": "=",
  "FieldValue": "Active",
  "JoinRule": "AND"
}
```

**Related Object Query:**
```json
{
  "FieldName": "OwnerTeam",
  "FieldValue": "$(CurrentUserTeamNames())",
  "FieldValueBehavior": "list",
  "IsRelatedObjectQuery": false
}
```

### 3. SearchRelatedObject

Represents a relationship available for searching (e.g., Incident → Task, Incident → Problem).

#### Properties

| Property | Type | JSON Name | Description |
|----------|------|-----------|-------------|
| `Id` | `string?` | `ID` | Unique identifier for the relationship |
| `ObjectId` | `string?` | `ObjectId` | Target object type |
| `Name` | `string?` | `Name` | Relationship display name |
| `Style` | `string?` | `Style` | Relationship style ("master", "related") |
| `ThereCardinality` | `string?` | `ThereCardinality` | Cardinality ("One", "Many", "") |

#### Relationship Examples

| Relationship | ID | Cardinality |
|-------------|-----|-------------|
| Incident (Master) | `Incident#` | `""` (empty) |
| Activity History | `Journal#.` | `Many` (0...N) |
| Task Group | `Task#.` | `Many` (0...N) |
| Employee (Owner) | `Employee#.owner` | `One` (0...1) |
| Service | `CI#Service.IncidentAssociatesService` | `One` (0...1) |
| Problem | `Problem#.` | `One` (0...1) |
| Change | `Change#.` | `Many` (0...M) |

### 4. SearchRights

User permissions for a saved search.

#### Properties

| Property | Type | JSON Name | Description |
|----------|------|-----------|-------------|
| `Create` | `bool` | `Create` | Can create new searches |
| `Edit` | `bool` | `Edit` | Can edit this search |
| `Delete` | `bool` | `Delete` | Can delete this search |
| `GlobalEdit` | `bool` | `GlobalEdit` | Can edit globally shared searches |

#### Permission Patterns

```csharp
// Check if user can modify search
if (searchData.SearchRights?.Edit == true)
{
    // Show edit button
}

// Check if search is read-only
bool isReadOnly = searchData.SearchRights is null 
    || !searchData.SearchRights.Edit;
```

## JSON Date Format

Ivanti uses Microsoft JSON date format:

```
"/Date(1641801395000)/"
```

This represents milliseconds since Unix epoch (January 1, 1970).

### Parsing Example

```csharp
public static DateTimeOffset? ParseIvantiDate(string? dateString)
{
    if (string.IsNullOrEmpty(dateString))
        return null;

    // Extract milliseconds from "/Date(1641801395000)/"
    var match = Regex.Match(dateString, @"/Date\((\d+)\)/");
    if (match.Success && long.TryParse(match.Groups[1].Value, out long ms))
    {
        return DateTimeOffset.FromUnixTimeMilliseconds(ms);
    }

    return null;
}
```

## Query String Format

The `Query` property contains a **serialized JSON string** (not an object). To use it:

```csharp
// Deserialize the query
if (!string.IsNullOrEmpty(searchData.Query))
{
    var queryConditions = JsonSerializer.Deserialize<List<SearchCondition>>(
        searchData.Query
    );

    // Process query conditions
    foreach (var condition in queryConditions)
    {
        Console.WriteLine($"{condition.FieldName} {condition.Condition} {condition.FieldValue}");
    }
}
```

## Integration Example

### Using in a Blazor Component

```razor
@inject IIvantiClient IvantiClient

<MudSelect T="ValidatedSearchData" 
          Label="Saved Searches"
          @bind-Value="_selectedSearch"
          ToStringFunc="@(s => s.Name)">
    @foreach (var search in _savedSearches)
    {
        <MudSelectItem Value="@search">
            @if (search.IsFavorite)
            {
                <MudIcon Icon="@Icons.Material.Filled.Star" Size="Size.Small" Class="mr-2" />
            }
            @search.Name
        </MudSelectItem>
    }
</MudSelect>

@code {
    private List<ValidatedSearchData> _savedSearches = new();
    private ValidatedSearchData? _selectedSearch;

    protected override async Task OnInitializedAsync()
    {
        var result = await IvantiClient.GetValidatedSearchAsync(CancellationToken.None);

        if (result.IsSuccess && result.Value != null)
        {
            _savedSearches.Add(result.Value);
        }
    }
}
```

### Building Search UI

```csharp
public class SearchFilterViewModel
{
    private readonly ValidatedSearchData _searchData;

    public SearchFilterViewModel(ValidatedSearchData searchData)
    {
        _searchData = searchData;
    }

    public List<SearchConditionViewModel> GetActiveFilters()
    {
        return _searchData.Conditions
            .Where(c => !string.IsNullOrEmpty(c.FieldValue))
            .Select(c => new SearchConditionViewModel
            {
                FieldName = c.FieldDisplay ?? c.FieldName,
                Operator = GetOperatorDisplay(c.Condition),
                Value = c.FieldValueDisplay ?? c.FieldValue
            })
            .ToList();
    }

    private string GetOperatorDisplay(string? op)
    {
        return op switch
        {
            "=" => "equals",
            "!=" => "not equals",
            ">" => "greater than",
            "<" => "less than",
            "()" => "in list",
            _ => op ?? "="
        };
    }
}
```

## Condition Type Mapping

| ConditionType | Value | Description |
|---------------|-------|-------------|
| ByField | 0 | Direct field comparison |
| ByRelatedObject | 1 | Comparison through relationship |
| ByExpression | 2 | Custom expression |

## Field Type Mapping

| FieldType | Description | Example Values |
|-----------|-------------|----------------|
| `"list"` | Dropdown/Select | "Active", "Closed" |
| `"text"` | Free text | "Server Error" |
| `"date"` | Date/DateTime | "2025-01-20" |
| `"number"` | Numeric | "123", "45.67" |
| `"boolean"` | True/False | "true", "false" |

## Special Values and Expressions

The `FieldValue` property can contain special expressions:

| Expression | Description |
|------------|-------------|
| `$(CurrentUserTeamNames())` | Current user's team names |
| `$(CurrentUser())` | Current user ID |
| `$(CurrentDate())` | Today's date |
| `$(CurrentDateTime())` | Current date and time |

## Search Condition Examples

### Example 1: Simple Status Filter

```json
{
  "ObjectId": "Incident#",
  "JoinRule": "AND",
  "Condition": "=",
  "FieldName": "Status",
  "FieldType": "list",
  "FieldValue": "Active",
  "FieldValueBehavior": "single"
}
```

Translates to SQL:
```sql
WHERE Status = 'Active'
```

### Example 2: Team Filter with Expression

```json
{
  "ObjectId": "Incident#",
  "JoinRule": "AND",
  "Condition": "()",
  "FieldName": "OwnerTeam",
  "FieldType": "list",
  "FieldValue": "$(CurrentUserTeamNames())",
  "FieldValueBehavior": "list"
}
```

Translates to SQL:
```sql
AND OwnerTeam IN (SELECT TeamName FROM CurrentUserTeams)
```

### Example 3: Complex Query with Brackets

```csharp
// Bracket level indicates grouping:
// (Status = 'Active' OR Status = 'Pending') AND Priority = '1'

Conditions:
[
    { BracketLevel: 1, Condition: "=", FieldName: "Status", FieldValue: "Active", JoinRule: "OR" },
    { BracketLevel: 1, Condition: "=", FieldName: "Status", FieldValue: "Pending", JoinRule: "AND", IsClosingBracket: true },
    { BracketLevel: 0, Condition: "=", FieldName: "Priority", FieldValue: "1" }
]
```

## Related Objects Hierarchy

The `RelatedObjects` array defines available relationships for query expansion:

### Master Object
```json
{
  "ID": "Incident#",
  "ObjectId": "Incident#",
  "Name": "Incident",
  "Style": "master",
  "ThereCardinality": ""
}
```

### One-to-Many Relationships
```json
{
  "ID": "Journal#.",
  "ObjectId": "Journal#",
  "Name": "Activity History via IncidentContainsJournal (0...1 : 0...N)",
  "Style": "related",
  "ThereCardinality": "Many"
}
```

### Many-to-One Relationships
```json
{
  "ID": "Employee#.owner",
  "ObjectId": "Employee#",
  "Name": "Employee via IncidentOwnerEmployee (0...N : 0...1)",
  "Style": "related",
  "ThereCardinality": "One"
}
```

### Many-to-Many Relationships
```json
{
  "ID": "Change#.",
  "ObjectId": "Change#",
  "Name": "Change via IncidentAssociatesChange (0...N : 0...M)",
  "Style": "related",
  "ThereCardinality": "Many"
}
```

## Reusable Patterns

### Filtering by Search Conditions

```csharp
public IQueryable<Incident> ApplySearchConditions(
    IQueryable<Incident> query, 
    ValidatedSearchData searchData)
{
    foreach (var condition in searchData.Conditions)
    {
        query = condition.FieldName switch
        {
            "Status" => query.Where(i => i.Status == condition.FieldValue),
            "Priority" => query.Where(i => i.Priority == condition.FieldValue),
            "OwnerTeam" => ApplyTeamFilter(query, condition),
            _ => query
        };
    }

    return query;
}

private IQueryable<Incident> ApplyTeamFilter(
    IQueryable<Incident> query, 
    SearchCondition condition)
{
    // Handle special expressions like $(CurrentUserTeamNames())
    if (condition.FieldValue?.StartsWith("$(") == true)
    {
        // Evaluate expression
        var teams = GetCurrentUserTeams();
        return query.Where(i => teams.Contains(i.OwnerTeam));
    }

    return query.Where(i => i.OwnerTeam == condition.FieldValue);
}
```

### Building Dynamic Search UI

```razor
@foreach (var condition in SearchData.Conditions)
{
    <MudStack Row="true" Class="mb-2">
        <MudText>@condition.FieldDisplay</MudText>
        <MudText Color="Color.Primary">@condition.Condition</MudText>
        <MudChip T="string" Color="Color.Info">
            @condition.FieldValueDisplay
        </MudChip>

        @if (condition != SearchData.Conditions.Last())
        {
            <MudText Color="Color.Secondary">@condition.JoinRule</MudText>
        }
    </MudStack>
}
```

## Role-Based Access

```csharp
public bool IsSearchAvailableForUser(ValidatedSearchData search, string userRole)
{
    return search.RoleScope?.Contains(userRole) == true;
}

public bool IsDefaultSearchForUser(ValidatedSearchData search, string userRole)
{
    return search.RolesDefault?.Contains(userRole) == true;
}
```

## Export/Email Capabilities

```csharp
// Check if search supports export
if (searchData.CanExport)
{
    // Show export button
    <MudIconButton Icon="@Icons.Material.Filled.Download" 
                  OnClick="ExportResults" />
}

// Check if search supports email
if (searchData.CanEmail)
{
    // Show email button
    <MudIconButton Icon="@Icons.Material.Filled.Email" 
                  OnClick="EmailResults" />
}
```

## Mapping to Domain Entities

When the search returns data, map it to domain entities:

```csharp
// After executing search query
var incidents = searchResults
    .Select(r => new Incident
    {
        RecId = r.RecId,
        Status = r.Status,
        Priority = r.Priority,
        // ... map other properties
    })
    .ToList();
```

## Testing

### Unit Test Example

```csharp
[Fact]
public void ValidatedSearchData_Deserializes_Correctly()
{
    // Arrange
    var json = File.ReadAllText("8.ValidatedSearch.json");
    var options = new JsonSerializerOptions
    {
        PropertyNameCaseInsensitive = true
    };

    // Act
    var wrapper = JsonSerializer.Deserialize<ValidatedSearchWrapper>(json, options);
    var searchData = wrapper.D;

    // Assert
    Assert.NotNull(searchData);
    Assert.Equal("My Team's Active Incidents", searchData.Name);
    Assert.True(searchData.IsValid);
    Assert.True(searchData.IsFavorite);
    Assert.Equal(2, searchData.Conditions.Count);
    Assert.Contains("ServiceDeskManager", searchData.RolesDefault);
}

public class ValidatedSearchWrapper
{
    [JsonPropertyName("d")]
    public ValidatedSearchData? D { get; set; }
}
```

## Performance Considerations

1. **Query String**: The `Query` property is a large serialized JSON string - avoid deserializing it unless necessary
2. **Related Objects**: Can contain 30+ relationships - lazy load when needed
3. **Conditions**: Typically 2-10 conditions per search
4. **Role Scope**: Can contain 10+ roles - use `Contains()` for lookups

## Future Enhancements

1. **Query Builder**: Visual query builder based on Conditions
2. **Expression Evaluator**: Evaluate special expressions like `$(CurrentUserTeamNames())`
3. **Search Templates**: Pre-defined search templates
4. **Advanced Filters**: Support for nested conditions and sub-queries
5. **Export Formats**: Excel, PDF, CSV export based on `CanExport`

## Common Related Object Types

Based on the JSON, Incident has relationships with:

- **Journal** - Activity History
- **Task** - Associated Tasks (multiple types)
- **Employee** - Owner
- **Change** - Related Changes
- **Problem** - Parent Problem
- **CI** - Configuration Items
- **Service** - Associated Service
- **Attachment** - File Attachments
- **Knowledge** - Knowledge Articles
- **Approval** - Approval Workflows

## API Integration

### Request/Response (TODO)

Currently, the `GetValidatedSearchAsync` method needs request/response classes:

```csharp
// TODO: Create these classes
public class GetValidatedSearchRequest
{
    [JsonPropertyName("searchId")]
    public string? SearchId { get; set; }

    [JsonPropertyName("_csrfToken")]
    public string? CsrfToken { get; set; }
}

public class GetValidatedSearchResponse
{
    [JsonPropertyName("d")]
    public ValidatedSearchData? D { get; set; }
}
```

## References

- Related to: `Application.Models.WorkspaceData.WorkspaceSearchQueryItem`
- Related to: `Application.Models.WorkspaceData.WorkspaceRelatedObject`
- Used by: Saved searches, Quick filters, Advanced search

---

**Created:** 2025
**Version:** 1.0
**Framework:** .NET 10
**JSON Source:** `8.ValidatedSearch.json`
