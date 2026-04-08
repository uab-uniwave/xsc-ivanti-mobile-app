# Visual Folder Structure Guide

## 🎨 Before & After Comparison

### Current Structure (Mixed Organization)
```
❌ BEFORE - Inconsistent Mix
══════════════════════════════════════════════════════════════

src/Application/
├── Common/                          ✅ Good
├── DTOs/                            ⚠️ Technical grouping
│   └── Incident/
│       ├── IncidentDto.cs
│       ├── IncidentListItemDto.cs
│       └── IncidentUpdateRequestDto.cs
├── Interfaces/                      ✅ Good
│   ├── Authentication/
│   ├── Navigation/
│   └── Workspaces/
├── Models/                          ⚠️ Mixed - some feature, some not
│   ├── Login/                       ✅ Feature-based
│   ├── UserData/                    ⚠️ Should be in Common
│   ├── SessionData/                 ⚠️ Should be in Common
│   ├── FormDefaultData/             ⚠️ Should be in Workspaces
│   ├── FormViewData/                ⚠️ Should be in Workspaces
│   └── WorkspaceData/               ⚠️ Should be in Workspaces
├── Requests/                        ⚠️ Technical grouping
└── Responses/                       ⚠️ Technical grouping

src/WebUI/
└── Features/                        ✅ Good start
    ├── Login/                       ⚠️ Should be Authentication
    │   ├── Login.razor              ⚠️ Should be in Pages/
    │   ├── Login.razor.cs           ⚠️ Should be in Pages/
    │   ├── SelectRole.razor         ⚠️ Should be in Pages/
    │   ├── SelectRole.razor.cs      ⚠️ Should be in Pages/
    │   └── ViewModels/              ✅ Good
    └── Incidents/
        ├── Incidents.razor          ⚠️ Should be in Pages/
        ├── Incidents.razor.cs       ⚠️ Should be in Pages/
        └── ViewModels/              ✅ Good
```

### Recommended Structure (Feature-Based)
```
✅ AFTER - Consistent Feature Organization
══════════════════════════════════════════════════════════════

src/Application/
├── Common/                          ✅ Truly shared only
│   ├── Models/
│   │   ├── SessionData/
│   │   └── UserData/
│   └── Result.cs
│
├── Interfaces/                      ✅ All service contracts
│   ├── Authentication/
│   ├── Navigation/
│   └── Workspaces/
│
├── Features/                        ⭐ NEW - Feature organization
│   ├── Authentication/
│   │   ├── DTOs/
│   │   │   ├── LoginRequest.cs
│   │   │   ├── LoginResponse.cs
│   │   │   └── GetVerificationTokenResponse.cs
│   │   └── Models/
│   │       ├── VerificationToken.cs
│   │       └── AuthenticationResult.cs
│   │
│   ├── Incidents/
│   │   ├── DTOs/
│   │   │   ├── IncidentDto.cs
│   │   │   ├── IncidentListItemDto.cs
│   │   │   └── IncidentUpdateRequestDto.cs
│   │   └── Models/
│   │
│   └── Workspaces/
│       ├── DTOs/
│       └── Models/
│           ├── FormDefaultData/
│           ├── FormViewData/
│           ├── RoleWorkspaces/
│           └── WorkspaceData/
│
├── Requests/                        ✅ API requests
└── Responses/                       ✅ API responses

src/WebUI/
├── Components/                      ⭐ NEW - Component hierarchy
│   ├── Common/                      # Generic reusable
│   │   ├── LoadingSpinner.razor
│   │   └── ErrorDisplay.razor
│   ├── Layout/                      # Layouts
│   │   ├── MainLayout.razor
│   │   └── NavMenu.razor
│   └── Shared/                      # Shared across features
│       └── UserAvatar.razor
│
└── Features/                        ✅ Feature-based
    ├── Authentication/              ⭐ Renamed from Login
    │   ├── Pages/                   ⭐ NEW subfolder
    │   │   ├── Login.razor
    │   │   ├── Login.razor.cs
    │   │   ├── SelectRole.razor
    │   │   └── SelectRole.razor.cs
    │   ├── Components/              ⭐ NEW - Feature-specific
    │   │   └── LoginForm.razor
    │   └── ViewModels/              ✅ Existing
    │       ├── LoginViewModel.cs
    │       └── SelectRoleViewModel.cs
    │
    └── Incidents/
        ├── Pages/                   ⭐ NEW subfolder
        │   ├── Incidents.razor
        │   ├── Incidents.razor.cs
        │   ├── IncidentDetails.razor
        │   ├── IncidentNew.razor
        │   └── IncidentEdit.razor
        ├── Components/              ⭐ NEW - Feature-specific
        │   ├── IncidentCard.razor
        │   └── IncidentStatusBadge.razor
        └── ViewModels/              ✅ Existing
            ├── IncidentsViewModel.cs
            ├── IncidentNewViewModel.cs
            └── IncidentEditViewModel.cs
```

## 🎯 Feature Organization Visual

### Complete Feature Structure for "Incidents"

```
┌─────────────────────────────────────────────────────────────┐
│                  Incidents Feature                          │
│              (Vertical Slice Example)                       │
└─────────────────────────────────────────────────────────────┘

Application/Features/Incidents/
├── DTOs/                            📦 Data Transfer
│   ├── IncidentDto.cs              # Full incident data
│   ├── IncidentListItemDto.cs      # List view data
│   ├── IncidentCreateDto.cs        # Create request
│   └── IncidentUpdateDto.cs        # Update request
│
└── Models/                          🎯 Domain/Business
    ├── IncidentStatus.cs           # Enum
    ├── IncidentPriority.cs         # Enum
    └── IncidentSearchCriteria.cs   # Search model

Infrastructure/ExternalServices/Ivanti/
└── [Incident-related API calls]     🔌 External

WebUI/Features/Incidents/
├── Pages/                           📄 Routable pages
│   ├── Incidents.razor             # List (/incidents)
│   ├── Incidents.razor.cs
│   ├── IncidentDetails.razor       # Details (/incidents/{id})
│   ├── IncidentDetails.razor.cs
│   ├── IncidentNew.razor           # Create (/incidents/new)
│   ├── IncidentNew.razor.cs
│   ├── IncidentEdit.razor          # Edit (/incidents/{id}/edit)
│   └── IncidentEdit.razor.cs
│
├── Components/                      🧩 Non-routable components
│   ├── IncidentCard.razor          # Reusable card
│   ├── IncidentFilter.razor        # Filter panel
│   ├── IncidentStatusBadge.razor   # Status display
│   └── IncidentPriorityIcon.razor  # Priority icon
│
└── ViewModels/                      🎨 Presentation logic
    ├── IncidentsViewModel.cs       # List view logic
    ├── IncidentDetailsViewModel.cs # Details logic
    ├── IncidentNewViewModel.cs     # Create logic
    └── IncidentEditViewModel.cs    # Edit logic

✅ BENEFIT: Everything for Incidents in one place!
```

## 🏗️ Clean Architecture Layer Visualization

```
┌──────────────────────────────────────────────────────────┐
│                    CLEAN ARCHITECTURE                    │
│                   Dependency Flow →                      │
└──────────────────────────────────────────────────────────┘

      UI Layer                WebUI/
    ┌────────────┐           ├── Features/
    │            │           │   ├── Authentication/
    │  WebUI     │           │   └── Incidents/
    │            │           └── Components/
    └─────┬──────┘                    │
          │ Depends on                │
          │ (Interfaces)              │
          ↓                            │
    ┌────────────┐                    │
    │            │   Application/     │
    │Application │   ├── Interfaces/ ←┘ (Defines contracts)
    │   Core     │   ├── Features/
    │            │   └── Common/
    └─────┬──────┘        │
          │               │
          │               ↓
    ┌─────┴──────┐   Infrastructure/
    │            │   ├── Authentication/
    │Infrastructure  ├── ExternalServices/
    │            │   │   └── Ivanti/
    │            │   └── Workspaces/
    └────────────┘
     (Implements)
```

## 📊 Component Hierarchy Visual

```
┌──────────────────────────────────────────────────────────┐
│              WebUI Component Organization                │
└──────────────────────────────────────────────────────────┘

WebUI/Components/
│
├── Common/                    🌐 Universal Reusable
│   ├── LoadingSpinner.razor   # Used everywhere
│   ├── ErrorDisplay.razor     # Error messages
│   ├── ConfirmDialog.razor    # Confirmations
│   └── Toast.razor            # Notifications
│
├── Layout/                    📐 Application Structure
│   ├── MainLayout.razor       # Main app layout
│   ├── NavMenu.razor          # Navigation menu
│   ├── Header.razor           # App header
│   └── Footer.razor           # App footer
│
└── Shared/                    🔄 Cross-Feature
    ├── UserAvatar.razor       # User display
    ├── WorkspaceSelector.razor # Workspace switcher
    └── SearchBox.razor        # Global search

WebUI/Features/Incidents/Components/
│
└── [Feature-Specific]         🎯 Incident Only
    ├── IncidentCard.razor     # Only for Incidents
    ├── IncidentFilter.razor   # Only for Incidents
    └── IncidentTimeline.razor # Only for Incidents

┌─────────────────────────────────────────┐
│            REUSABILITY RULE             │
├─────────────────────────────────────────┤
│ Common/  = Used by 3+ features          │
│ Shared/  = Used by 2+ features          │
│ Feature/ = Used by 1 feature only       │
└─────────────────────────────────────────┘
```

## 🎭 MVVM Pattern in WebUI

```
┌──────────────────────────────────────────────────────────┐
│              MVVM Pattern Structure                      │
│        (Every page follows this pattern)                 │
└──────────────────────────────────────────────────────────┘

Features/Incidents/Pages/Incidents.razor
┌─────────────────────────────────────┐
│   VIEW (.razor)                     │   @page "/incidents"
│   ─────────────────────────────────│
│   • Markup only                     │   <MudDataGrid Items="@ViewModel.Incidents"
│   • Binds to ViewModel              │                Loading="@ViewModel.IsLoading">
│   • No business logic               │   </MudDataGrid>
│   • MudBlazor components            │
└─────────────┬───────────────────────┘
              │
              ├─> CODE-BEHIND (.razor.cs)
              │   ┌─────────────────────────────────────┐
              │   │   • Minimal code                     │
              │   │   • Lifecycle events                │
              │   │   • Delegates to ViewModel          │
              │   └──────────┬──────────────────────────┘
              │              │
              └──────────────┼─> VIEWMODEL (.cs)
                             │   ┌─────────────────────────────────────┐
                             │   │   • All business logic               │
                             └───│   • Properties for binding          │
                                 │   • Service injection                │
                                 │   • State management                 │
                                 │   • No UI dependencies               │
                                 └─────────────────────────────────────┘

FLOW:
  User Action → View → Code-Behind → ViewModel → Services → API
  Response    ← View ← Code-Behind ← ViewModel ← Services ← API
```

## 📁 Migration Path Visual

```
┌──────────────────────────────────────────────────────────┐
│                   Migration Roadmap                      │
└──────────────────────────────────────────────────────────┘

PHASE 1: Feature Folders                    ⏱️ 2-3 hours
┌────────────────────────────────────────────────────────┐
│ 1. Create Application/Features/           structure    │
│ 2. Move DTOs to features                              │
│ 3. Move Models to features                            │
│ 4. Update namespaces                                   │
│ 5. Fix build errors                                    │
└────────────────────────────────────────────────────────┘
               ↓

PHASE 2: WebUI Reorganization                ⏱️ 1-2 hours
┌────────────────────────────────────────────────────────┐
│ 1. Create WebUI/Components/ hierarchy                 │
│ 2. Create Features/*/Pages/ subfolders                │
│ 3. Create Features/*/Components/ subfolders           │
│ 4. Rename Login → Authentication                      │
│ 5. Move files to correct locations                    │
└────────────────────────────────────────────────────────┘
               ↓

PHASE 3: Infrastructure Cleanup              ⏱️ 30 min
┌────────────────────────────────────────────────────────┐
│ 1. Create ExternalServices/Ivanti/                    │
│ 2. Move Ivanti client files                           │
│ 3. Update DependencyInjection.cs                      │
│ 4. Update namespaces                                   │
└────────────────────────────────────────────────────────┘
               ↓

PHASE 4: Documentation                       ⏱️ 30 min
┌────────────────────────────────────────────────────────┐
│ 1. Update copilot-instructions.md                     │
│ 2. Update ARCHITECTURE.md                             │
│ 3. Create migration notes                             │
└────────────────────────────────────────────────────────┘

Total Estimated Time: 4-6 hours
```

## 🎯 Decision Tree: Where Should This File Go?

```
                  New File/Class to Add
                          │
                          ↓
            ┌─────────────┴─────────────┐
            │   Is it business logic?   │
            └─────────────┬─────────────┘
                     Yes  │  No
            ┌─────────────┴─────────────┐
            ↓                            ↓
     ┌─────────────┐            ┌──────────────┐
     │ Application │            │Infrastructure│
     └──────┬──────┘            └──────┬───────┘
            │                          │
  ┌─────────┴──────────┐   ┌───────────┴──────────┐
  │  Is it a contract  │   │  Is it UI-specific?  │
  │  (interface)?      │   └───────────┬──────────┘
  └─────────┬──────────┘          Yes  │  No
       Yes  │  No              ┌───────┴────────┐
  ┌─────────┴──────────┐       ↓                ↓
  ↓                     ↓    WebUI/        Infrastructure/
Interfaces/        Features/  Services/     ExternalServices/
  └─ {Concern}/      └─{Feature}/           └─{Service}/
                       ├─DTOs/
                       └─Models/

EXAMPLE:
┌────────────────────────────────────────────────────┐
│ New: IncidentStatusService                         │
│ Q: Business logic? YES                             │
│ Q: Contract? NO                                    │
│ → Application/Features/Incidents/Services/         │
│                                                     │
│ New: IvantiIncidentClient                          │
│ Q: Business logic? NO (infrastructure)             │
│ Q: UI-specific? NO                                 │
│ → Infrastructure/ExternalServices/Ivanti/          │
│                                                     │
│ New: IncidentViewModel                             │
│ Q: Business logic? NO (presentation)               │
│ Q: UI-specific? YES                                │
│ → WebUI/Features/Incidents/ViewModels/             │
└────────────────────────────────────────────────────┘
```

## 📚 Quick Reference Card

```
┌────────────────────────────────────────────────────────┐
│            FOLDER STRUCTURE QUICK RULES                │
├────────────────────────────────────────────────────────┤
│                                                         │
│  📁 Application/                                       │
│     Interfaces/     → All service contracts            │
│     Features/       → Group by business feature        │
│     Common/         → Truly shared utilities only      │
│                                                         │
│  🔧 Infrastructure/                                    │
│     Authentication/ → Auth implementations             │
│     ExternalServices/ → Third-party APIs               │
│     Workspaces/     → Workspace service                │
│                                                         │
│  🎨 WebUI/                                             │
│     Components/                                        │
│       Common/       → 3+ features use it               │
│       Shared/       → 2 features use it                │
│       Layout/       → Layout components                │
│     Features/                                          │
│       {Feature}/                                       │
│         Pages/      → Routable components              │
│         Components/ → Feature-specific only            │
│         ViewModels/ → All presentation logic           │
│                                                         │
│  🎯 Naming:                                            │
│     Folders → PascalCase, usually plural               │
│     Files → Match class name exactly                   │
│     Routes → lowercase-with-hyphens                    │
│                                                         │
└────────────────────────────────────────────────────────┘
```

---

## 💡 Tips for Success

1. **Start with one feature** (e.g., Incidents) and complete it fully
2. **Move files in batches** by feature, not by file type
3. **Update namespaces immediately** after moving files
4. **Test after each phase** to ensure nothing breaks
5. **Use Find/Replace** to update namespace references
6. **Commit after each successful phase** for safety

---

**Remember**: Consistency is more important than perfection! 🚀
