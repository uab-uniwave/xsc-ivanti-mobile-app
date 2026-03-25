# Copilot Instructions – Repository Documentation Management

## Purpose
This document defines the rules for organizing documentation in this repository.

All contributors and AI assistants (including GitHub Copilot) must follow these rules when creating, reviewing, or updating documentation.

Goals:
- maintain a clean repository structure
- separate permanent knowledge from temporary working material
- ensure documentation is discoverable through a central index
- enforce consistent documentation organization patterns

---

# Repository Structure

/README.md  
/docs  
/docs-archive  
/src  

---

# Root README.md (Documentation Index)

The repository root must contain a **single documentation entry point**:

README.md

This file acts as the **central documentation index**.

It must contain:

- Table of Contents
- Categorized documentation sections
- Hyperlinks to documentation files
- Short paragraph description for each document

Example:

```
# Project Documentation

## Architecture
- [System Architecture](docs/architecture/system-architecture.md)
  Describes the overall system architecture and core components.

## Deployment
- [Deployment Pipeline](docs/deployment/deployment-pipeline.md)
  Explains CI/CD pipeline and deployment process.
```

Rules:

- Links must be sorted alphabetically
- Documentation must be grouped by category
- Each link must include a **1–2 sentence description**
- README must always reflect the latest documentation structure

---

# Permanent Documentation – /docs

The **docs folder** contains long-term documentation.

Examples:

- architecture
- coding standards
- design patterns
- API documentation
- deployment instructions
- infrastructure configuration
- CI/CD standards
- logging and monitoring
- integration guides

Example structure:

```
docs/
 ├─ architecture/
 │   ├─ system-architecture.md
 │   └─ service-interactions.md
 │
 ├─ deployment/
 │   └─ deployment-pipeline.md
 │
 └─ standards/
     └─ coding-standards.md
```

Rules:

- Documents must describe **stable concepts**
- Files should remain relevant long-term
- Outdated documentation must be moved to `/docs-archive`

---

# Topic Organization Rule

Each **new documentation topic must begin as a folder**.

Example:

```
docs/security/
docs/api/
docs/logging/
docs/integrations/
```

Documents inside:

```
docs/security/security-model.md
docs/security/token-validation.md
```

This allows topics to grow later.

---

# Single Document Folder Rule

If a topic contains **only one document**, simplify the structure.

Example before cleanup:

```
docs/security/security-model.md
```

After cleanup:

```
docs/security-model.md
```

The folder must then be removed.

---

# Temporary Documentation – /docs-archive

The **docs-archive folder** stores temporary or historical documentation.

Examples:

- implementation plans
- refactoring checklists
- debugging notes
- migration plans
- research notes
- AI-generated planning documents

Example:

```
docs-archive/
 ├─ api-refactor-plan.md
 ├─ debugging-notes.md
 └─ migration-checklist.md
```

Rules:

- Content may be temporary
- Files may be deleted when obsolete
- Architecture documentation must not be stored here

---

# Repository Root Rules

The root folder must remain clean.

Allowed files:

```
README.md
*.sln
Directory.Build.props
Directory.Build.targets
.editorconfig
.gitignore
global.json
build scripts
configuration files
```

Forbidden in root:

```
*.doc
*.docx
*.txt
*.pdf
*.rtf
```

These must be moved to:

```
/docs
or
/docs-archive
```

---

# Source Code Review Rules

AI assistants must recursively review all projects inside:

```
/src
```

Paths:

```
root/src/*
root/src/**/*
```

Tasks:

1. detect documentation placed in source folders
2. relocate documentation to `/docs` or `/docs-archive`

Incorrect:

```
src/MyProject/notes.txt
src/MyProject/architecture.md
```

Correct:

```
docs/architecture/system-architecture.md
docs-archive/refactor-plan.md
```

Exception:

Component-specific documentation that directly describes a library may remain inside that component folder.

---

# Documentation Lifecycle

Temporary documentation may become permanent.

Process:

1. document created in `/docs-archive`
2. concept stabilizes
3. clean the document
4. move it to `/docs`
5. add link to README index

Example:

```
docs-archive/order-sync-analysis.md
→
docs/architecture/order-sync.md
```

---

# Naming Conventions

Documentation files must use:

- lowercase
- kebab-case

Examples:

```
order-processing.md
api-security.md
deployment-pipeline.md
```

Avoid:

```
OrderProcessingDoc.md
notes_final2.md
document1.md
```

---

# Rules for AI Assistants (Copilot)

When generating documentation:

1. architecture and standards → `/docs`
2. temporary plans and checklists → `/docs-archive`
3. do not place documentation in root except README.md
4. review `/src` recursively
5. move misplaced documentation
6. update README.md index when new docs appear
7. create topic folders for new topics
8. collapse folders containing only a single file
9. keep documentation index sorted alphabetically

Preferred document structure:

```
# Title

## Purpose
## Overview
## Implementation
## Configuration
## References
```

---

# Automatic README Index Rule

Whenever a new document is created:

1. detect the category folder
2. add entry into README.md
3. place the link under the correct category
4. keep alphabetical order

Example entry:

```
- [order-sync.md](docs/architecture/order-sync.md)
  Describes synchronization between ERP and production systems.
```

---

# Core Principle

Permanent knowledge belongs in:

/docs

Temporary working material belongs in:

/docs-archive

README.md serves as the **central documentation navigation for the repository**.
"""

output_path = "/mnt/data/copilot-documentation-instructions.md"

pypandoc.convert_text(
    content,
    "md",
    format="md",
    outputfile=output_path,
    extra_args=["--standalone"]
)

output_path
