$baseDir = "d:\Unity Projects\Tetris\Assets\Scripts"
$gameplayDir = Join-Path $baseDir "Gameplay"
if (-not (Test-Path $gameplayDir)) {
    New-Item -ItemType Directory -Path $gameplayDir | Out-Null
}

$filesToMove = @("Board.cs", "Data.cs", "GhostPiece.cs", "Piece.cs", "Tetromino.cs")
foreach ($file in $filesToMove) {
    $src = Join-Path $baseDir $file
    $dest = Join-Path $gameplayDir $file
    if (Test-Path $src) {
        Move-Item -Path $src -Destination $dest
    }
    
    $srcMeta = $src + ".meta"
    $destMeta = $dest + ".meta"
    if (Test-Path $srcMeta) {
        Move-Item -Path $srcMeta -Destination $destMeta
    }
}

$allFiles = Get-ChildItem -Path $baseDir -Filter *.cs -Recurse
foreach ($file in $allFiles) {
    $content = Get-Content $file.FullName -Raw
    
    if ($content -match "namespace Tetris") {
        continue
    }

    $usings = [regex]::Matches($content, '(?m)^using\s+[^;]+;')
    $usingText = ($usings | ForEach-Object { $_.Value }) -join "`r`n"
    
    $rest = $content -replace '(?m)^using\s+[^;]+;\r?\n?', ''
    
    # Remove empty lines at start of rest
    $rest = $rest -replace '^\s+', ''

    $newContent = ""
    if ($usingText.Trim() -ne "") {
        $newContent += $usingText + "`r`n`r`n"
    }
    $newContent += "namespace Tetris`r`n{`r`n"
    
    # Indent the rest by 4 spaces
    $indentedRest = $rest -replace '(?m)^', '    '
    
    $newContent += $indentedRest + "`r`n}`r`n"
    
    Set-Content -Path $file.FullName -Value $newContent
}
Write-Host "Done"
