#!/bin/bash
set -e

ACTION=${1:-all} # Options: all, build, run, test, clean
PROJECT_PATH="./src/Chess.UI.Eto.Linux/Chess.UI.Eto.Linux.csproj"
TEST_PATH="./tests/Chess.Core.Tests/Chess.Core.Tests.csproj"

case "$ACTION" in
    "clean")
        echo "=== Cleaning Build Artifacts ==="
        dotnet clean
        ;;
    "test")
        echo "=== Running Unit Tests ==="
        dotnet test "$TEST_PATH"
        ;;
    "build")
        echo "=== Building Linux Project ==="
        dotnet build "$PROJECT_PATH" --configuration Release
        ;;
    "run")
        echo "=== Running Linux Project ==="
        dotnet run --project "$PROJECT_PATH" --configuration Release
        ;;
    "all")
        echo "=== Running Full Build & Test Pipeline ==="
        dotnet build "$PROJECT_PATH" --configuration Release
        dotnet test "$TEST_PATH"
        dotnet run --project "$PROJECT_PATH" --configuration Release
        ;;
    *)
        echo "Invalid action. Use clean, test, build, run, or all."
        exit 1
        ;;
esac