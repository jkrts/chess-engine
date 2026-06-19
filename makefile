### Chess Engine Makefile ###

# Linux
build-linux:
	dotnet build ChessEngine.Linux.slnf

test-linux:
	dotnet test ChessEngine.Linux.slnf

restore-linux:
	dotnet restore ChessEngine.Linux.slnf

run-linux:
	dotnet run --project src/ChessEngine.UI.Eto.Linux/ChessEngine.UI.Eto.Linux.csproj

# Windows
build-win:
	dotnet build ChessEngine.Windows.slnf

test-win:
	dotnet test ChessEngine.Windows.slnf

restore-win:
	dotnet restore ChessEngine.Windows.slnf

run-win:
	dotnet run --project src/ChessEngine.UI.Eto.Windows/ChessEngine.UI.Eto.Windows.csproj