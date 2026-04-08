# Script to update all model namespaces in the new Feature folders

Write-Host "Updating model namespaces in Feature folders..." -ForegroundColor Cyan

# Define namespace mappings
$namespaceMappings = @{
    "src\Application\Features\Workspaces\Models\FormDefaultData" = "Application.Features.Workspaces.Models.FormDefaultData"
    "src\Application\Features\Workspaces\Models\FormValidationListData" = "Application.Features.Workspaces.Models.FormValidationListData"
    "src\Application\Features\Workspaces\Models\FormViewData" = "Application.Features.Workspaces.Models.FormViewData"
    "src\Application\Features\Workspaces\Models\GridDataHandler" = "Application.Features.Workspaces.Models.GridDataHandler"
    "src\Application\Features\Workspaces\Models\RoleWorkspaces" = "Application.Features.Workspaces.Models.RoleWorkspaces"
    "src\Application\Features\Workspaces\Models\ValidatedSearch" = "Application.Features.Workspaces.Models.ValidatedSearch"
    "src\Application\Features\Workspaces\Models\WorkspaceData" = "Application.Features.Workspaces.Models.WorkspaceData"
    "src\Application\Common\Models\SessonData" = "Application.Common.Models.SessonData"
    "src\Application\Common\Models\UserData" = "Application.Common.Models.UserData"
}

foreach ($folder in $namespaceMappings.Keys) {
    $targetNamespace = $namespaceMappings[$folder]

    if (-not (Test-Path $folder)) {
        Write-Host "  Skipping $folder (not found)" -ForegroundColor Yellow
        continue
    }

    $files = Get-ChildItem -Path $folder -Filter "*.cs" -File

    Write-Host "`nProcessing folder: $folder" -ForegroundColor Green
    Write-Host "  Target namespace: $targetNamespace" -ForegroundColor Gray
    Write-Host "  Files found: $($files.Count)" -ForegroundColor Gray

    foreach ($file in $files) {
        $content = Get-Content $file.FullName -Raw

        # Replace old namespace patterns
        $patterns = @(
            "namespace Application\.Models\.\w+",
            "namespace Application\.Models",
            "namespace Application\.DTOs",
            "namespace Application\.Requests",
            "namespace Application\.Responses"
        )

        $updated = $false
        foreach ($pattern in $patterns) {
            if ($content -match $pattern) {
                $content = $content -replace $pattern, "namespace $targetNamespace"
                $updated = $true
            }
        }

        if ($updated) {
            Set-Content -Path $file.FullName -Value $content -NoNewline
            Write-Host "    Updated: $($file.Name)" -ForegroundColor Green
        } else {
            Write-Host "    Skipped: $($file.Name) (already correct)" -ForegroundColor Gray
        }
    }
}

Write-Host "`nAll model namespaces updated!" -ForegroundColor Green
