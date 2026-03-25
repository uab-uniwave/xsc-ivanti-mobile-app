# Copilot Instruction: Blazor Component Code Separation

When modifying or creating Blazor components in this repository, follow these strict separation rules:

## File Structure

Every Blazor component consists of THREE files with clear responsibilities:

### 1. `Component.razor` - Markup Only
**Purpose:** Display UI and capture user input events

**ALLOWED:**
- HTML/MudBlazor markup
- `@bind` directives for data binding
- `@onclick`, `@onchange` event handlers that call code-behind methods
- `@if`, `@foreach` for simple conditional rendering
- `@parameter` declarations
- `@cascadingparameter` consumption
- Component-scoped CSS

**FORBIDDEN:**
- Method definitions (except inline lambdas delegating to code-behind)
- Business logic or calculations
- API calls
- Nested complex conditionals
- Data aggregations (Sum, Count, Where in markup)
- Dialog/modal orchestration code
- Event handler implementations

**Pattern:**
```razor
<!-- ✅ GOOD: Call handler, let code-behind delegate to ViewModel -->
<MudButton OnClick="@HandleDeleteAsync">Delete</MudButton>

<!-- ❌ BAD: Complex logic in markup -->
<MudButton OnClick="@(async () => { 
    var confirmed = await DialogService.ShowMessageBoxAsync(...);
    if (confirmed) await API.DeleteAsync(item.Id);
})">Delete</MudButton>
```

---

### 2. `Component.razor.cs` - Component Orchestration
**Purpose:** Bridge UI to business logic; manage component lifecycle and UI state

**ALLOWED:**
- `[Inject]` dependency resolution
- `[Parameter]` and `[CascadingParameter]` declarations
- Lifecycle methods (`OnInitializedAsync`, `OnAfterRenderAsync`, `Dispose`)
- Event handlers that **delegate** to ViewModel methods
- **Pure UI state** (form collapsed/expanded, selected tabs, input references)
- `StateHasChanged()` calls
- Dialog opening/closing operations

**FORBIDDEN:**
- Data models or business properties
- Calculations or aggregations
- API calls directly
- Business logic implementation
- Validation logic
- File operations
- Data transformations

**Pattern:**
```csharp
public partial class Orders
{
    [Inject] private OrderApiClient Api { get; set; } = null!;
    [Inject] private OrdersViewModel ViewModel { get; set; } = null!;

    // ✅ GOOD: Delegate to ViewModel
    private async Task HandleDeleteAsync(GetOrderDto order)
        => await ViewModel.DeleteOrderAsync(order, ReloadGridDataAsync);

    // ✅ GOOD: Pure UI state
    private bool _isFormCollapsed = false;

    // ❌ BAD: Business logic
    private async Task HandleDeleteAsync(GetOrderDto order)
    {
        await Api.DeleteOrderAsync(order.Id);  // ❌ API call
        Orders.Remove(order);  // ❌ Business logic
    }
}
```

---

### 3. `ComponentViewModel.cs` - Business Logic & State
**Purpose:** All business logic, data management, and API interactions

**ALLOWED:**
- Public properties for UI binding
- Calculated/derived properties
- API call methods
- Business operations (CRUD, filtering, validation)
- Data transformation methods
- Search/filter implementations
- File operations logic
- Dialog parameter preparation
- Event/state notifications

**FORBIDDEN:**
- UI element references (`InputFile`, `MudForm`, `MudDataGrid`)
- MudBlazor service calls (except creating dialog parameters)
- Component lifecycle methods
- `StateHasChanged()` calls
- Pure UI state (collapsed/expanded, visual toggles)
- Cascading parameters

**Pattern:**
```csharp
public class OrdersViewModel
{
    // ✅ GOOD: Bindable properties
    public List<GetOrderDto> Orders { get; set; } = new();
    public int CurrentPage { get; set; }

    // ✅ GOOD: Calculated properties
    public decimal TotalAmount => Orders.Sum(o => o.Amount);
    public int CompletedCount => Orders.Count(o => o.State == 6);

    // ✅ GOOD: Business logic
    public async Task<GridData<GetOrderDto>> LoadServerDataAsync(GridState<GetOrderDto> state)
    {
        var result = await _api.GetAllOrderAsync(...);
        Orders = result.Items.ToList();
        return new GridData<GetOrderDto> { Items = Orders };
    }

    // ❌ BAD: UI element reference
    public InputFile? FileInput { get; set; }

    // ❌ BAD: Pure UI state
    public bool IsFormCollapsed { get; set; }
}
```

---

## Decision Rules

**Use this tree to determine file placement:**

1. **Is it HTML/MudBlazor markup?**
   → **Component.razor**

2. **Does it inject a service, manage component lifecycle, or contain pure UI state?**
   → **Component.razor.cs**
   - Example: `InputFile` references, visual toggles, component lifecycle methods

3. **Does it need to be displayed in the UI or used in calculations?**
   → **ComponentViewModel.cs** (as property)
   - Example: `Orders`, `CurrentPage`, `IsLoading`

4. **Is it a calculation, transformation, or aggregation?**
   → **ComponentViewModel.cs** (as property or method)
   - Example: `TotalAmount`, `AverageOrderAmount`, `BuildFilterString()`

5. **Does it involve an API call or network operation?**
   → **ComponentViewModel.cs** (as method)

6. **Is it business logic (validate, filter, sort, delete)?**
   → **ComponentViewModel.cs** (as method)

7. **Does it need to show a dialog or manage dialog content?**
   → **ComponentViewModel.cs** (prepares parameters and logic)
   → **Component.razor.cs** (shows the dialog)

---

## Common Anti-Patterns (Avoid These)

### ❌ Anti-Pattern 1: Data Aggregation in Markup
```razor
<!-- WRONG -->
<MudText>Total: @(Orders.Sum(o => o.Amount).ToString("C2"))</MudText>
<MudText>Completed: @Orders.Count(o => o.State == 6)</MudText>
```

**Fix:** Add properties to ViewModel
```csharp
// ViewModel
public decimal TotalAmount => Orders.Sum(o => o.Amount);
public int CompletedCount => Orders.Count(o => o.State == 6);
```

Then use in markup:
```razor
<MudText>Total: @ViewModel.TotalAmount.ToString("C2")</MudText>
<MudText>Completed: @ViewModel.CompletedCount</MudText>
```

---

### ❌ Anti-Pattern 2: Business Logic in Code-Behind
```csharp
// WRONG - in Component.razor.cs
private async Task HandleDeleteAsync(GetOrderDto order)
{
    var confirmed = await DialogService.ShowMessageBoxAsync("Delete?", "Are you sure?");
    if (confirmed == true)
    {
        await _api.DeleteOrderAsync(order.Id);  // ❌ API call here
        Orders.Remove(order);  // ❌ State mutation here
        Snackbar.Add("Deleted!", Severity.Success);  // ❌ Notification here
    }
}
```

**Fix:** Move all logic to ViewModel
```csharp
// ViewModel
public async Task DeleteOrderAsync(GetOrderDto order)
{
    var confirmed = await _dialogService.ShowMessageBoxAsync("Delete?", "Are you sure?");
    if (confirmed == true)
    {
        await _api.DeleteOrderAsync(order.Id);
        Orders.Remove(order);
        _snackbar.Add("Deleted!", Severity.Success);
    }
}

// Component.razor.cs
private async Task HandleDeleteAsync(GetOrderDto order)
    => await ViewModel.DeleteOrderAsync(order);
```

---

### ❌ Anti-Pattern 3: UI State in ViewModel
```csharp
// WRONG - in ViewModel
public bool IsFormCollapsed { get; set; }
public InputFile? FileInput { get; set; }
public bool ShowAdvancedOptions { get; set; }
```

**Fix:** Keep pure UI state in code-behind
```csharp
// Component.razor.cs
private bool _isFormCollapsed = false;
private InputFile? _fileInput;
private bool _showAdvancedOptions = false;
```

---

### ❌ Anti-Pattern 4: Search Logic Split Across Files
```csharp
// WRONG - in Component.razor.cs
private Dictionary<string, Customer> _customerCache = new();

private async Task<IEnumerable<string>> SearchAsync(string value)
{
    var customers = await Api.GetCustomersAsync(value);
    _customerCache.Clear();
    // ... caching logic ...
}
```

**Fix:** Move search logic to ViewModel
```csharp
// ViewModel
private Dictionary<string, Customer> _customerCache = new();

public async Task<IEnumerable<string>> SearchCustomersAsync(string value)
{
    if (string.IsNullOrWhiteSpace(value))
        return Enumerable.Empty<string>();

    var customers = await _api.GetCustomersAsync(value);
    _customerCache.Clear();
    // ... caching logic ...
    return displayStrings;
}

// Component.razor.cs - just delegate
private async Task<IEnumerable<string>> SearchAsync(string value)
    => await ViewModel.SearchCustomersAsync(value);
```

---

### ❌ Anti-Pattern 5: Complex Conditionals in Markup
```razor
<!-- WRONG -->
@if (Orders != null && Orders.Count > 0 
     && Orders.Any(o => o.State == 6)
     && Orders.Where(o => o.Amount > 1000).Count() > 0)
{
    <MudText>Complex condition met</MudText>
}
```

**Fix:** Create a ViewModel property
```csharp
// ViewModel
public bool HasHighValueCompletedOrders =>
    Orders.Count > 0 &&
    Orders.Any(o => o.State == 6) &&
    Orders.Any(o => o.Amount > 1000);
```

Then use simply:
```razor
@if (ViewModel.HasHighValueCompletedOrders)
{
    <MudText>Complex condition met</MudText>
}
```

---

## Copilot Behavioral Rules

When you see or create Blazor component code:

1. **Always verify the 3-file structure** exists before implementing features
   - If a file is missing, create it with the appropriate separation

2. **When adding a feature, determine which file(s) it affects:**
   - Ask: "Is this UI, orchestration, or business logic?"
   - Place code in the correct file first time

3. **When refactoring, follow dependency flow:**
   - Business logic should always be in ViewModel
   - Code-behind should be thin orchestration layer
   - Markup should be pure display

4. **When you see anti-patterns:**
   - Flag them with explanations
   - Offer to fix them using the separation rules
   - Update the code if user approves

5. **When reviewing existing code:**
   - Look for calculations in markup or code-behind
   - Look for API calls outside ViewModel
   - Look for UI state in ViewModel
   - Suggest fixes that improve separation

6. **When generating new code:**
   - Generate all 3 files with clear separation
   - Add XML documentation explaining responsibilities
   - Follow the patterns shown in this file

---

## Reference: Where Each Responsibility Goes

| Responsibility | `.razor` | `.razor.cs` | `ViewModel.cs` |
|---|:---:|:---:|:---:|
| HTML/Markup | ✅ | ❌ | ❌ |
| Data binding | ✅ | ❌ | ✅ |
| Event handlers | ✅ | ✅ (delegate) | ❌ |
| Component lifecycle | ❌ | ✅ | ❌ |
| Dependency injection | ❌ | ✅ | ✅ |
| Calculations | ❌ | ❌ | ✅ |
| API calls | ❌ | ❌ | ✅ |
| Business logic | ❌ | ❌ | ✅ |
| Validation logic | ❌ | ❌ | ✅ |
| Dialog orchestration | ❌ | ✅ (show) | ✅ (params) |
| UI state (collapsed) | ❌ | ✅ | ❌ |
| File operations | ❌ | ❌ | ✅ |
| Search/filter logic | ❌ | ❌ | ✅ |
| StateHasChanged() | ❌ | ✅ | ❌ |

---

## Examples from This Repository

### ✅ Good Example: OrdersViewModel Calculated Properties

```csharp
// File: OrdersViewModel.cs
public int TotalOrderCount => Orders.Count;
public decimal TotalAmount => Orders.Sum(o => o.Amount);
public decimal AverageOrderAmount => Orders.Count > 0 ? TotalAmount / Orders.Count : 0;
public int CompletedOrderCount => Orders.Count(o => o.State == 6);
```

Then used in markup:
```razor
<InfoCard Header="Total Amount" Value="@ViewModel.TotalAmount.ToString("C2")" />
<InfoCard Header="Avg. Order" Value="@ViewModel.AverageOrderAmount.ToString("C2")" />
```

✅ **Why this is correct:** Calculations stay in ViewModel, markup stays clean

---

### ❌ Bad Example: Calculations in Orders.razor

```razor
<!-- File: Orders.razor - lines 55-62 -->
@if (_viewModel?.Orders != null && _viewModel.Orders.Count > 0)
{
    var totalOrders = _viewModel.Orders.Count;
    var totalAmount = _viewModel.Orders.Sum(o => o.Amount);
    var avgOrderAmount = totalOrders > 0 ? totalAmount / totalOrders : 0;
    var totalMaterials = _viewModel.Orders.Sum(o => o.Materials.Count);
    // ... more calculations ...
}
```

❌ **Why this is wrong:** Calculations in markup make it hard to test and reuse

---

### ✅ Good Example: Delegating from Code-Behind

```csharp
// File: Orders.razor.cs
private async Task HandleOpenOrderEditDialogAsync(GetOrderDto? order)
{
    if (order == null)
    {
        Snackbar.Add("Please select an order to edit", Severity.Warning);
        return;
    }

    var saved = await ViewModel.OpenOrderEditDialogAsync(order);

    if (saved)
    {
        await ReloadGridDataAsync();
    }
}
```

✅ **Why this is correct:** Code-behind validates input, delegates to ViewModel, handles result

---

### ❌ Bad Example: Search Logic in Code-Behind

```csharp
// File: OrderDialog.razor.cs - ❌ WRONG
private Dictionary<string, GetPrefCustomerDto> _customersCache = new();

private async Task<IEnumerable<string>> SearchCustomersAsync(string value)
{
    if (string.IsNullOrWhiteSpace(value))
        return new List<string>();

    var customers = await PrefSuiteApi.GetCustomersAsync(value);
    _customersCache.Clear();
    var displayStrings = new List<string>();
    foreach (var customer in customers)
    {
        var displayString = $"{customer.CustomerName} ({customer.CustomerCode})";
        displayStrings.Add(displayString);
        _customersCache[displayString] = customer;
    }
    return displayStrings;
}
```

❌ **Why this is wrong:**
- API call in code-behind
- Business logic (caching) in code-behind
- Mixing concerns

**Should be:** Move entire search logic to ViewModel, code-behind just delegates

---

## When in Doubt

Ask these questions:

1. **Will this be tested?** → If yes, put it in ViewModel
2. **Will this be reused by another component?** → If yes, put it in ViewModel
3. **Does this depend on MudBlazor services?** → If yes, put it in code-behind or ViewModel
4. **Is this pure display?** → If yes, put it in `.razor`
5. **Is this a side effect (API call, notification)?** → If yes, put it in ViewModel
6. **Is this just managing UI appearance?** → If yes, put it in code-behind

**Default:** When in doubt, put it in ViewModel. It's easier to move down to code-behind later than up from markup.
