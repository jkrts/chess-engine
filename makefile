### Chess Engine Makefile ###

# Linux
build-linux:
	dotnet build ChessEngine.Linux.slnf

test-linux:
	dotnet test ChessEngine.Linux.slnf

restore-linux:
	dotnet restore ChessEngine.Linux.slnf

run-linux:
	dotnet run --project src/Chess.UI.Eto.Linux/Chess.UI.Eto.Linux.csproj

# Windows
build-win:
	dotnet build ChessEngine.Windows.slnf

test-win:
	dotnet test ChessEngine.Windows.slnf

restore-win:
	dotnet restore ChessEngine.Windows.slnf

run-win:
	dotnet run --project src/Chess.UI.Eto.Windows/Chess.UI.Eto.Windows.csproj