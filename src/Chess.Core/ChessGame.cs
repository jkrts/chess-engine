namespace Chess.Core;

public class ChessGame
{
    public ChessPosition currentChessPosition { get; private set; }


    public ChessGame()
    {
        currentChessPosition = new ChessPosition();
    }

    public ChessGame(string fen)
    {
        currentChessPosition = new ChessPosition(fen);
    }
}