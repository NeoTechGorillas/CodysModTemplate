$MyInvocation.MyCommand.Path | Split-Path | Push-Location

$Name = (Get-ChildItem -Filter *.csproj).BaseName

dotnet build -c Release

$PluginDirectory = "BepInEx\plugins\$Name"
if (-not (Test-Path -Path $PluginDirectory)) {
    New-Item -Path $PluginDirectory -ItemType Directory
}

$SourceDll = "bin\Release\netstandard2.0\$Name.dll"
$DestinationDll = "$PluginDirectory\$Name.dll"
if (Test-Path $SourceDll) {
    Copy-Item -Path $SourceDll -Destination $DestinationDll
    Write-Host "Copied $SourceDll to $DestinationDll"
} else {
    Write-Error "Source DLL does not exist: $SourceDll"
    Exit 1
}

$ArchiveName = "$Name-v$(Get-Date -Format 'yyyyMMdd-HHmmss').zip"
Compress-Archive -Path .\BepInEx\ -DestinationPath $ArchiveName
Write-Host "Created archive: $ArchiveName"

Remove-Item -Path .\BepInEx\ -Recurse -Force
Write-Host "Cleaned up BepInEx directory."

Pop-Location