param(
    [string]$AssetsPath
)

Write-Host "Checking assets in: $AssetsPath"

$langs = @('en', 'fa')
$errors = @()
$enDir = Join-Path $AssetsPath 'en'

Write-Host "Looking in: $enDir"

if (-not (Test-Path $enDir)) {
    Write-Error "en folder not found at: $enDir"
    exit 1
}

$files = Get-ChildItem -Path $enDir -Filter '*.yaml'
Write-Host "Found $($files.Count) yaml file(s) in en folder"

foreach ($file in $files) {
    $base = $file.BaseName
    foreach ($lang in $langs) {
        $expected = Join-Path $AssetsPath $lang ($base + '.yaml')
        if (-not (Test-Path $expected)) {
            $errors += "MISSING: $expected [base=$base, lang=$lang]"
        }
    }
}

if ($errors.Count -gt 0) {
    foreach ($e in $errors) {
        Write-Error $e
    }
    exit 1
}

Write-Host "All localized assets validated successfully."
