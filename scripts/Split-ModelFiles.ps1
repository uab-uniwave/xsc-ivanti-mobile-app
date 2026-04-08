# PowerShell script to split multi-class model files
# This script extracts each class from large model files and creates individual files

$modelFiles = @(
    @{
        Path = "src\Application\Common\Models\SessionData\SessionData.cs"
        Namespace = "Application.Common.Models.SessonData"
    },
    @{
        Path = "src\Application\Common\Models\UserData\UserData.cs"
        Namespace = "Application.Common.Models.UserData"
    },
    @{
        Path = "src\Application\Features\Workspaces\Models\WorkspaceData\WorkspaceData.cs"
        Namespace = "Application.Features.Workspaces.Models.WorkspaceData"
    },
    @{
        Path = "src\Application\Features\Workspaces\Models\FormViewData\FormViewData.cs"
        Namespace = "Application.Features.Workspaces.Models.FormViewData"
    },
    @{
        Path = "src\Application\Features\Workspaces\Models\FormDefaultData\FormDefaultData.cs"
        Namespace = "Application.Features.Workspaces.Models.FormDefaultData"
    },
    @{
        Path = "src\Application\Features\Workspaces\Models\ValidatedSearch\ValidatedSearch.cs"
        Namespace = "Application.Features.Workspaces.Models.ValidatedSearch"
    }
)

function Split-ModelFile {
    param(
        [string]$FilePath,
        [string]$TargetNamespace
    )

    Write-Host "Processing: $FilePath" -ForegroundColor Cyan

    if (-not (Test-Path $FilePath)) {
        Write-Host "File not found: $FilePath" -ForegroundColor Red
        return
    }

    $content = Get-Content $FilePath -Raw
    $directory = Split-Path $FilePath -Parent

    # Extract all classes using regex
    $classPattern = '(?ms)^\s*(public|internal|private|protected)\s+(sealed\s+)?class\s+(\w+).*?^\s*\}'
    $matches = [regex]::Matches($content, $classPattern)

    if ($matches.Count -le 1) {
        Write-Host "  Only 1 class found, skipping split" -ForegroundColor Yellow
        return
    }

    Write-Host "  Found $($matches.Count) classes" -ForegroundColor Green

    # Extract using statements
    $usingPattern = '(?m)^using\s+[^;]+;'
    $usings = [regex]::Matches($content, $usingPattern) | ForEach-Object { $_.Value }
    $uniqueUsings = $usings | Select-Object -Unique | Where-Object { $_ -notmatch 'namespace' }

    foreach ($match in $matches) {
        $className = $match.Groups[3].Value
        $classCode = $match.Value

        # Create new file content
        $newContent = @"
$($uniqueUsings -join "`n")

namespace $TargetNamespace;

$classCode
"@

        $newFilePath = Join-Path $directory "$className.cs"

        # Only create if it doesn't exist or is different
        if (-not (Test-Path $newFilePath)) {
            Set-Content -Path $newFilePath -Value $newContent -Encoding UTF8
            Write-Host "    Created: $className.cs" -ForegroundColor Green
        } else {
            Write-Host "    Skipped (exists): $className.cs" -ForegroundColor Yellow
        }
    }

    Write-Host "  Completed splitting $FilePath`n" -ForegroundColor Green
}

# Process each model file
foreach ($modelFile in $modelFiles) {
    Split-ModelFile -FilePath $modelFile.Path -TargetNamespace $modelFile.Namespace
}

Write-Host "`nAll model files processed!" -ForegroundColor Green
Write-Host "Note: Original files have NOT been deleted. Please review the split files first." -ForegroundColor Yellow
