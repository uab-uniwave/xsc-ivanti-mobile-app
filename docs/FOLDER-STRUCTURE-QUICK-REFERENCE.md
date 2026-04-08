# Quick Reference: Folder Structure

## 🎯 Key Principles

1. **Feature-Based Organization** (Vertical Slices) over Technical Organization
2. **Clean Architecture Layers** with clear boundaries
3. **MVVM Pattern** for all Blazor pages
4. **Separation of Concerns** at every level

## 📁 Quick Structure Overview

```
Application/
├── Common/                  # Shared utilities
├── Interfaces/              # All service contracts
├── Features/                # ⭐ ORGANIZE BY FEATURE
│   ├── Authentication/
│   ├── Incidents/
│   └── Workspaces/
├── Requests/                # API requests
├── Responses/               # API responses
└── Exceptions/

Infrastructure/
├── Authentication/
├── ExternalServices/
│   └── Ivanti/             # External API clients
├── Workspaces/
├── Mapping/
└── DependencyInjection.cs

WebUI/
├── Components/
│   ├── Common/             # Generic reusable
│   ├── Layout/             # Layouts
│   └── Shared/             # Shared across features
├── Features/                # ⭐ ORGANIZE BY FEATURE
│   ├── Authentication/
│   │   ├── Pages/
│   │   ├── Components/     # Feature-specific
│   │   └── ViewModels/
│   └── Incidents/
│       ├── Pages/
│       ├── Components/
│       └── ViewModels/
└── Services/               # UI services
```

## 🔄 Migration Checklist

### Phase 1: Feature Folders ✅ Priority
- [ ] Create `Application/Features/{Feature}/DTOs`
- [ ] Create `Application/Features/{Feature}/Models`
- [ ] Move existing DTOs to features
- [ ] Create `WebUI/Features/{Feature}/Pages`
- [ ] Create `WebUI/Features/{Feature}/Components`
- [ ] Update all namespaces

### Phase 2: Clean Up
- [ ] Rename `Login` → `Authentication`
- [ ] Create `Components/Common`, `Components/Layout`, `Components/Shared`
- [ ] Move `Ivanti` → `ExternalServices/Ivanti`
- [ ] Update `copilot-instructions.md`

### Phase 3: Testing
- [ ] Create test project structure
- [ ] Mirror source structure in tests

## 📖 Full Documentation

See **[FOLDER-STRUCTURE-RECOMMENDATIONS.md](./FOLDER-STRUCTURE-RECOMMENDATIONS.md)** for:
- Detailed explanations
- Microsoft Learn references
- Complete migration guide
- Best practices
- Future considerations

## 🎓 Key Takeaways from Microsoft Learn

1. **Feature Slices** reduce navigation time in large apps
2. **Clean Architecture** makes testing easier
3. **Vertical organization** improves team collaboration
4. **Clear boundaries** prevent coupling
5. **Consistent structure** reduces cognitive load

---

**Next Steps**: Review full recommendations → Start Phase 1 → Update documentation
