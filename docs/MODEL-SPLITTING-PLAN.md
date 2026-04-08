# Model Splitting Action Plan

## ✅ COMPLETED

### RoleWorkspaces (9 classes)
- ✅ RoleWorkspaces.cs (main class)
- ✅ Workspace.cs
- ✅ RoleWorkspaceConfiguration.cs
- ✅ RoleWorkspaceNotifications.cs
- ✅ RoleWorkspaceBrandingOptions.cs
- ✅ RoleWorkspaceSystemMenuOptions.cs
- ✅ RoleWorkspaceSelectorOptions.cs
- ✅ RoleWorkspaceHelpLink.cs
- ✅ RoleWorkspaceLink.cs

### FormValidationListData (2 classes)
- ✅ FormValidationListData.cs (main class)
- ✅ FieldData.cs

---

## ⏳ REMAINING MODELS

### 1. FormDefaultData (5 classes)
**Location**: `src/Application/Features/Workspaces/Models/FormDefaultData/FormDefaultData.cs`

**Classes to Extract**:
1. `FormDefaultData` - Main class (KEEP in original file)
2. `FormDefaultDataContainer` - Extract to FormDefaultDataContainer.cs
3. `ReferencedFieldDefinition` - Extract to ReferencedFieldDefinition.cs
4. `FormDataModel` - Extract to FormDataModel.cs
5. `DataObject` - Extract to DataObject.cs

**Notes**: 
- Uses file-scoped namespace: `namespace Application.Features.Workspaces.Models.FormDefaultData;`
- FormDefaultData is auto-generated, keep structure as-is

---

### 2. FormViewData (25 classes) ⚠️ LARGEST
**Location**: `src/Application/Features/Workspaces/Models/FormViewData/FormViewData.cs`

**Classes to Extract** (in order of appearance):
1. `FormViewData` - Main class (KEEP)
2. `FormDefinition` → FormDefinition.cs
3. `ToolbarDef` → ToolbarDef.cs
4. `TableMeta` → TableMeta.cs
5. `CalculatedRule` → CalculatedRule.cs
6. `ValidatedField` → ValidatedField.cs
7. `Relationship` → Relationship.cs
8. `ForeignKeyInfo` → ForeignKeyInfo.cs
9. `IncludeObjectsFromField` → IncludeObjectsFromField.cs
10. `UpdateField` → UpdateField.cs
11. `Rel2Relationship` → Rel2Relationship.cs
12. `LinkTableInfo` → LinkTableInfo.cs
13. `SearchPreviewField` → SearchPreviewField.cs
14. `FieldMeta` → FieldMeta.cs
15. `FieldLink` → FieldLink.cs
16. `FormMeta` → FormMeta.cs
17. `FormCell` → FormCell.cs
18. `FormControl` → FormControl.cs
19. `RuleMeta` → RuleMeta.cs
20. `RuleValue` → RuleValue.cs
21. `Expression` → Expression.cs
22. `ExpressionTree` → ExpressionTree.cs
23. `AutoFillRule` → AutoFillRule.cs
24. `FinalStateRule` → FinalStateRule.cs
25. `ExpressionRule` → ExpressionRule.cs

**Notes**:
- Largest file with complex nested structures
- Recommend using the PowerShell script approach after manual files are created
- Keep careful track of JsonPropertyName attributes

---

### 3. WorkspaceData (10 classes)
**Location**: `src/Application/Features/Workspaces/Models/WorkspaceData/WorkspaceData.cs`

**Classes to Extract**:
1. `WorkspaceData` - Main class (KEEP)
2. `WorkspaceSearchData` → WorkspaceSearchData.cs
3. `WorkspaceFavorite` → WorkspaceFavorite.cs
4. `WorkspaceFieldsTreeData` → WorkspaceFieldsTreeData.cs
5. `WorkspaceTableDef` → WorkspaceTableDef.cs
6. `WorkspaceFieldItem` → WorkspaceFieldItem.cs
7. `WorkspaceFieldMetaData` → WorkspaceFieldMetaData.cs
8. `WorkspaceFieldLink` → WorkspaceFieldLink.cs
9. `WorkspaceLayoutData` → WorkspaceLayoutData.cs
10. `ModalFormDefinition` → ModalFormDefinition.cs

---

## 🎯 EXECUTION STEPS

### For FormDefaultData:
1. Identify each class section in the file
2. Copy class code with all attributes and properties
3. Create new file with appropriate using statements
4. Use namespace: `Application.Features.Workspaces.Models.FormDefaultData;`
5. Delete from original file
6. Build and test

### For FormViewData:
1. Use the PowerShell script approach (recommended due to size)
2. Or manually extract 25 classes following FormDefaultData pattern
3. Ensure all using statements are included (System.Collections.Generic, System.Text.Json.Serialization, etc.)
4. Build and verify no circular dependencies

### For WorkspaceData:
1. Follow same pattern as FormDefaultData
2. Watch for property dependencies (e.g., WorkspaceFieldItem depends on WorkspaceFieldMetaData)
3. Verify namespace consistency

---

## 📋 VALIDATION CHECKLIST

After splitting each file:
- [ ] Each class has its own `.cs` file
- [ ] File-scoped namespace used: `namespace X.Y.Z;`
- [ ] All necessary using statements included
- [ ] No class definition in original file except main class
- [ ] All JsonPropertyName attributes preserved
- [ ] Build succeeds with no errors
- [ ] No circular dependencies introduced

---

## 🔧 PowerShell Script for Large Files

Use `scripts/Split-MultiClassModels.ps1` for:
- FormViewData.cs (25 classes)
- Any other files with 10+ classes

Run:
```powershell
.\scripts\Split-MultiClassModels.ps1
```

---

## 📊 SUMMARY

**Completed**: 11 files split (9 + 2)
**Remaining**: 40 files to split (5 + 25 + 10)
**Total**: 56 classes → 56 individual files

---

## Next Steps:
1. Run PowerShell script on FormViewData
2. Manually handle FormDefaultData
3. Manually handle WorkspaceData
4. Build and verify everything compiles
5. Commit changes to git
