# Copilot Instructions for XSC Ivanti Mobile App

## Purpose

These instructions should help generate code that matches the current repository, not an idealized architecture from another project.

This solution is a Blazor Server application for Ivanti ServiceDesk. It uses MudBlazor for UI, follows Clean Architecture boundaries where practical, and uses a ViewModel-driven pattern for Blazor pages.

## Current Solution Shape

### Projects

- `src/Domain`
  - Domain exceptions and core domain concepts.
  - Must not depend on other solution layers.

- `src/Application`
  - Interfaces, contracts, DTOs, feature models, and application-facing abstractions.
  - Must not depend on `Infrastructure` or `WebUI`.

- `src/Infrastructure`
  - Ivanti API integration, authentication/session plumbing, DI registration, mapping, and external concerns.
  - Implements interfaces from `Application`.

- `src/WebUI`
  - Blazor Server UI, feature pages, page code-behind files, ViewModels, layout components, and UI-specific services.

### Current Feature Organization

Use the actual folder structure already present in the repo.

- `src/WebUI/Features/Authentication/Pages`
- `src/WebUI/Features/Authentication/ViewModels`
- `src/WebUI/Features/Incidents/Pages`
- `src/WebUI/Features/Incidents/ViewModels`
- `src/WebUI/Components`
- `src/WebUI/Services`
- `src/Infrastructure/Ivanti`
- `src/Infrastructure/Authentication`
- `src/Infrastructure/State`
- `src/Application/Features/.../DTOs`
- `src/Application/Interfaces/...`

Do not invent parallel folder structures unless the repo is being intentionally refactored.

## Architecture Rules

### Layer Boundaries

- `Domain` must not depend on `Application`, `Infrastructure`, or `WebUI`.
- `Application` must not reference `Infrastructure` or `WebUI`.
- `Infrastructure` may depend on `Application` and `Domain`.
- `WebUI` may depend on `Application` and use `Infrastructure` only through composition/registration already established by the solution.

### Where Logic Should Live

- Put API contracts, DTOs, models, and service interfaces in `Application`.
- Put HTTP calls, cookie handling, Ivanti endpoints, mapping, and integration code in `Infrastructure`.
- Put page state, UI orchestration, and user-interaction flow in `WebUI` ViewModels and UI services.
- Keep `.razor` markup focused on presentation.
- Keep `.razor.cs` files thin and focused on lifecycle wiring and event forwarding.

Do not move UI orchestration into `Application` just to satisfy a generic rule. Follow the current repo pattern unless explicitly refactoring it.

## Blazor Page Pattern

For pages in `WebUI`, prefer this structure:

1. `<PageName>.razor`
   - Markup and component composition only.
   - Use MudBlazor components for UI.
   - Bind to ViewModel state.

2. `<PageName>.razor.cs`
   - Thin code-behind only.
   - Inject the ViewModel.
   - Wire component lifecycle methods.
   - Forward UI events to ViewModel methods when needed.

3. `<PageName>ViewModel.cs`
   - Page state.
   - Loading/error flags.
   - UI-facing orchestration logic.
   - Calls into services/interfaces.

### Injection Conventions

- In Blazor page code-behind, use `[Inject]` property injection for the page ViewModel. This matches the current repository convention.
- In services and ViewModels, use constructor injection.
- Register ViewModels as scoped in `src/WebUI/Program.cs`.
- Register infrastructure/application service implementations in `src/Infrastructure/DependencyInjection.cs`.

### Lifecycle Conventions

- If initialization depends on JS interop or browser storage, use `OnAfterRenderAsync(bool firstRender)` and guard on `firstRender`.
- If JS interop is not required, `OnInitializedAsync()` is acceptable.
- Do not move JS-dependent initialization into `OnInitializedAsync()`.

## UI Rules

### MudBlazor

- Razor pages and reusable UI components should use MudBlazor as the default UI component library.
- Prefer existing MudBlazor patterns already used in the repo over introducing raw HTML when a MudBlazor component fits.
- Keep component usage idiomatic and valid for the current MudBlazor version in the repo.

### Responsive Design

All user-facing UI should be responsive by default.

- Design for desktop, tablet, and phone.
- Do not build layouts that only work at a single width.
- Prefer flexible MudBlazor layout primitives such as `MudGrid`, `MudItem`, `MudStack`, `MudContainer`, `MudHidden`, and breakpoint-aware properties.
- Test whether the page remains usable on narrow mobile widths before considering the task complete.
- Preserve touch-friendly spacing and button sizes on tablet and phone.
- Avoid large fixed widths/heights unless there is a strong reason.

### UI Behavior

- Show loading states for async actions.
- Show user-friendly error states in the UI.
- Keep business rules out of the `.razor` file.
- Keep markup readable; do not bury logic in inline expressions if it can live in the ViewModel.

## Ivanti-Specific Rules

### Authentication and Session

- Ivanti authentication is cookie-based.
- Verification token and cookie/session pairing are important.
- Cookie handling behavior in `Infrastructure` is intentional; do not casually replace it with a new auth pattern.
- Scoped state services are used to preserve user/session state within the Blazor circuit.

### Current Initialization Flow

Follow the repo’s current behavior unless the task explicitly changes it:

1. Fetch verification token and cookies.
2. Submit login.
3. Initialize Ivanti session.
4. Load user data.
5. Load role workspaces for navigation.
6. Lazy-load full workspace form data when the user navigates into a workspace.

Do not assume all workspace form metadata is loaded eagerly at login.

## Error Handling and Logging

- Prefer the existing `Result<T>` pattern for service boundaries where the repo already uses it.
- Do not swallow exceptions silently.
- Log failures with useful context.
- Use `Information` for normal flow, `Warning` for handled anomalies, and `Error` for failures/exceptions.
- Keep user-facing error messages clear and actionable.

## Naming and Code Style

- Use PascalCase for classes, methods, properties, files, and folders.
- Use `I` prefix for interfaces.
- Use `_camelCase` for private fields.
- Use `camelCase` for parameters and local variables.
- Suffix asynchronous methods with `Async`.
- Prefer nullable-safe code and explicit null handling.
- Avoid magic strings when a constant or mapping helper is more appropriate.

## Code Comments and XML Documentation

Write more comments and XML documentation when they improve understanding of intent, constraints, side effects, or integration behavior.

### General Comment Rules

- Prefer comments that explain why, not comments that restate what the code already says.
- Add comments when behavior is non-obvious, integration-heavy, stateful, or easy to misuse.
- Keep comments accurate, concrete, and close to the code they describe.
- Update or remove stale comments when changing behavior.
- Do not add filler comments such as "set property value" or "loop through items".

### When Inline Comments Are Recommended

Add inline or block comments for:

- Ivanti-specific authentication/session behavior
- cookie, token, and state-management assumptions
- lifecycle constraints in Blazor components
- non-obvious mapping or serialization behavior
- workaround logic for framework or API quirks
- sequencing requirements where order matters
- code paths with important side effects

### Inline Comment Style

- Keep comments short and direct.
- Place the comment immediately above the code it explains.
- Prefer one high-value comment before a block over many low-value line-by-line comments.
- Use TODO comments sparingly and only when they name a real follow-up action.
- If a TODO is completed, remove it.

### XML Documentation Rules

Use XML documentation for public and protected members when it helps another developer understand purpose, usage, or behavior.

XML documentation is strongly recommended for:

- public interfaces
- service contracts
- public services
- public models with non-obvious meaning
- public methods with side effects, ordering requirements, or integration-specific behavior
- extension methods
- reusable UI services

XML documentation may be skipped for:

- trivial private members
- obvious property getters/setters
- simple DTOs whose meaning is already clear from names alone
- internal code where XML docs would add noise without new information

### What Good XML Docs Should Include

When writing XML docs, prefer the common .NET style:

- `<summary>` for purpose and behavior
- `<param>` for non-obvious parameters
- `<returns>` when the return value needs explanation
- `<remarks>` for important workflow, lifecycle, or side-effect notes
- `<exception>` only when exceptions are intentionally part of the API contract

### XML Documentation Content Guidance

- Describe what the member does and when it should be used.
- Mention important side effects, state changes, or ordering requirements.
- Explain integration-specific expectations when interacting with Ivanti or browser storage.
- Keep summaries concise; do not turn every summary into a paragraph.
- Do not copy the method or property name into sentence form without adding meaning.

### Examples of Good Documentation Intent

- explain why `OnAfterRenderAsync(firstRender)` is required instead of `OnInitializedAsync()`
- explain why a service must remain scoped for Blazor circuit state
- explain why a shared cookie container exists and what would break if changed
- explain why a workspace-loading call is lazy instead of eager
- explain what a DTO property means when the external Ivanti field name is unclear

## Documentation Rules

Treat documentation as code: keep what explains the system, remove what no longer helps.

### Keep and Improve

Keep and improve documents that explain:

- solution architecture
- important business or integration logic
- authentication/session behavior
- workspace loading flow
- setup, run, and debugging instructions
- troubleshooting notes that reflect current behavior
- README files that help a developer understand or run the solution

If a README or design note is outdated but still useful, update it instead of deleting it.

### Remove or Avoid

Remove or avoid maintaining documents that no longer provide value, such as:

- temporary refactoring stage notes
- one-off migration checklists that are already completed
- stale TODO plans that no longer match the code
- abandoned implementation checklists
- duplicate notes that repeat what is already captured in better documentation
- throwaway progress documents created only for a short-lived task

Before deleting documentation, verify it is not the only place that explains an important decision or workflow.

### Documentation Editing Guidance

- Prefer updating existing README/design docs over creating many small fragmented files.
- Keep documentation factual and repo-specific.
- Remove stale sections when they contradict the implementation.
- If logic is non-obvious and important, document it close to the code and in the relevant README.

## What To Avoid

- Do not generate code that introduces a new architectural pattern without matching the repo.
- Do not invent folders, services, or abstractions that are not needed.
- Do not push business or integration logic into `.razor` markup.
- Do not put significant logic in `.razor.cs` if it belongs in a ViewModel or service.
- Do not reference `Infrastructure` from `Application`.
- Do not replace MudBlazor with another UI approach unless explicitly requested.
- Do not add documentation boilerplate that says little and explains nothing.
- Do not preserve stale refactoring plans or obsolete checklists just because they already exist.

## Preferred Copilot Behavior

When generating code for this repository:

- mirror existing naming, folder, and dependency patterns
- prefer small, local changes over speculative rewrites
- use MudBlazor for Razor UI
- keep pages responsive for desktop, tablet, and phone
- preserve important architectural and logic documentation
- remove or consolidate stale low-value docs when asked to clean up documentation

If the existing code and these instructions conflict, prefer the current repo pattern unless the task is explicitly a refactor.
