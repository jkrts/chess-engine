param (
    [string]$Action = "all" # Options: all, build, run, test, clean
)

$ErrorActionPreference = "Stop"
$ProjectPath = "./src/Chess.UI.Eto.Windows/Chess.UI.Eto.Windows.csproj"
$TestPath = "./tests/Chess.Core.Tests/Chess.Core.Tests.csproj"

switch ($Action) {
    "clean" {
        Write-Host "=== Cleaning Build Artifacts ===" -ForegroundColor Yellow
        dotnet clean
    }
    "test" {
        Write-Host "=== Running Unit Tests ===" -ForegroundColor Magenta
        dotnet test $TestPath
    }
    "build" {
        Write-Host "=== Building Windows Project ===" -ForegroundColor Cyan
        dotnet build $ProjectPath --configuration Release
    }
    "run" {
        Write-Host "=== Running Windows Project ===" -ForegroundColor Green
        dotnet run --project $ProjectPath --configuration Release
    }
    "all" {
        Write-Host "=== Running Full Build & Test Pipeline ===" -ForegroundColor Cyan
        dotnet build $ProjectPath --configuration Release
        dotnet test $TestPath
        dotnet run --project $ProjectPath --configuration Release
    }
    Default {
        Write-Error "Invalid action. Use clean, test, build, run, or all."
    }
}
