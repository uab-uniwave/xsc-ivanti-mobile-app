# Script to split multi-class model files into individual files
# Each class gets its own file in the same directory

param(
    [string]$ModelsPath = "src\Application\Features\Workspaces\Models"
)

Write-Host "🔧 Splitting multi-class model files..." -ForegroundColor Cyan
Write-Host "Path: $ModelsPath`n"

# Get all CS files that have multiple classes
$files = Get-ChildItem -Path $ModelsPath -Recurse -Filter "*.cs" -File

foreach ($file in $files) {
    $classCount = (Select-String -Path $file.FullName -Pattern "^\s*(public|internal|private).*class\s+" | Measure-Object).Count

    if ($classCount -le 1) {
        Write-Host "✓ $($file.Name): $classCount class(es) - Skip" -ForegroundColor Green
        continue
    }

    Write-Host "`n📄 Processing: $($file.Name) ($classCount classes)" -ForegroundColor Yellow

    $content = Get-Content $file.FullName -Raw
    $directory = $file.Directory.FullName
    $namespace = Select-String -Path $file.FullName -Pattern "namespace\s+([^;]+)" | Select-Object -First 1 -ExpandProperty Matches | ForEach-Object { $_.Groups[1].Value }

    # Extract using statements
    $usings = @()
    $lines = $content -split "`n"
    foreach ($line in $lines) {
        if ($line -match "^\s*using\s+") {
            $usings += $line.Trim()
        }
        if ($line -match "^\s*namespace\s+") {
            break
        }
    }

    # Find all class definitions and their content
    $classPattern = '(?ms)((?:public|internal|private|protected)\s+(?:sealed\s+)?(?:partial\s+)?class\s+\w+[^{]*\{(?:[^{}]|(?:\{[^{}]*\}))*\})'
    $classMatches = [regex]::Matches($content, $classPattern)

    Write-Host "  Found $($classMatches.Count) classes to extract"

    $createdFiles = @()

    foreach ($match in $classMatches) {
        $classCode = $match.Value

        # Extract class name
        $classNameMatch = [regex]::Match($classCode, 'class\s+(\w+)')
        if (-not $classNameMatch.Success) {
            Write-Host "  ⚠️  Could not extract class name" -ForegroundColor Yellow
            continue
        }

        $className = $classNameMatch.Groups[1].Value
        $newFilePath = Join-Path $directory "$className.cs"

        # Check if file already exists and skip
        if ((Test-Path $newFilePath) -and (Get-Item $newFilePath).Length -gt 100) {
            Write-Host "  ⚠️  $className.cs already exists - Skip" -ForegroundColor Yellow
            continue
        }

        # Build new file content
        $newContent = @()
        $newContent += ($usings -join "`n")
        $newContent += ""
        $newContent += "namespace $namespace;"
        $newContent += ""
        $newContent += $classCode

        try {
            Set-Content -Path $newFilePath -Value ($newContent -join "`n") -Encoding UTF8
            Write-Host "  ✓ Created: $className.cs" -ForegroundColor Green
            $createdFiles += $className
        }
        catch {
            Write-Host "  ✗ Error creating $className.cs: $_" -ForegroundColor Red
        }
    }

    # Only delete original file if we successfully created all classes
    if ($createdFiles.Count -eq $classMatches.Count) {
        Write-Host "  ✓ All classes extracted, deleting original file" -ForegroundColor Green
        Remove-Item $file.FullName -Force
    } else {
        Write-Host "  ⚠️  Not all classes extracted, keeping original file" -ForegroundColor Yellow
    }
}

Write-Host "`n✅ Split complete!" -ForegroundColor Green
