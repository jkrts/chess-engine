namespace Chess.Core;

public class ChessGame
{
    public ChessPosition CurrentChessPosition { get; private set; }


    public ChessGame()
    {
        CurrentChessPosition = new ChessPosition();
    }

    public ChessGame(string fen)
    {
        CurrentChessPosition = new ChessPosition(fen);
    }
}