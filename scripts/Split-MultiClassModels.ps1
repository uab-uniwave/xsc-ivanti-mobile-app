# Comprehensive script to split all multi-class workspace models
# Creates individual files for each class

function Extract-Classes {
    param(
        [string]$FilePath,
        [string]$Namespace,
        [string[]]$Usings,
        [string]$OutputDir
    )

    $content = Get-Content $FilePath -Raw
    $createdFiles = @()

    # Split using old-style namespace (with braces)
    if ($content -match 'namespace\s+[\w\.]+\s*\{(.*)\}\s*$' -and $content -notmatch '^\s*namespace\s+[\w\.]+;') {
        # Has old-style namespace with braces
        $classContent = $matches[1]

        # Find all class declarations
        $classPattern = '((?:public|internal|private|protected)\s+(?:sealed\s+)?(?:partial\s+)?class\s+\w+\s*\{[^}]*(?:\{[^}]*\}[^}]*)*\})'
        $classes = [regex]::Matches($classContent, $classPattern)

        foreach ($class in $classes) {
            $classCode = $class.Value.Trim()

            # Extract class name
            if ($classCode -match 'class\s+(\w+)') {
                $className = $matches[1]

                # Create new file with file-scoped namespace
                $newContent = @()
                if ($Usings.Count -gt 0) {
                    $newContent += $Usings
                    $newContent += ""
                }
                $newContent += "namespace $Namespace;"
                $newContent += ""
                $newContent += $classCode

                $newFilePath = Join-Path $OutputDir "$className.cs"
                Set-Content -Path $newFilePath -Value ($newContent -join "`n") -Encoding UTF8

                $createdFiles += $className
                Write-Host "  ✓ Created: $className.cs"
            }
        }
    }

    return $createdFiles
}

$modelsPath = "src\Application\Features\Workspaces\Models"
$files = Get-ChildItem -Path $modelsPath -Recurse -Filter "*.cs" -File

foreach ($file in $files) {
    $classCount = (Select-String -Path $file.FullName -Pattern '^\s*(public|internal|private)\s+\w*\s*class\s+' | Measure-Object).Count

    if ($classCount -le 1) {
        continue
    }

    Write-Host "`n📄 Processing: $($file.Name) ($classCount classes)"

    $content = Get-Content $file.FullName -Raw
    $directory = $file.Directory.FullName

    # Extract namespace
    if ($content -match 'namespace\s+([\w\.]+)') {
        $namespace = $matches[1]
    }

    # Extract using statements
    $usings = @()
    $lines = $content -split "`n"
    foreach ($line in $lines) {
        $trimmed = $line.Trim()
        if ($trimmed -match '^using\s+' -and $trimmed -match ';$') {
            $usings += $trimmed
        } elseif ($trimmed -match '^namespace\s+') {
            break
        }
    }

    $createdFiles = Extract-Classes -FilePath $file.FullName -Namespace $namespace -Usings $usings -OutputDir $directory

    if ($createdFiles.Count -eq $classCount) {
        Write-Host "  ✓ All $classCount classes extracted"
        Remove-Item $file.FullName -Force
        Write-Host "  ✓ Deleted original file"
    }
}

Write-Host "`n✅ Done!"
