namespace Chess.Core;

public enum PieceType
{
    Pawn = 1,
    Knight = 2,
    King = 3,
    Bishop = 5,
    Rook = 6,
    Queen = 7
}

public enum PieceColor
{
    White = 1,
    Black = -1
}

public class Piece
{
    public PieceType Type;
    public PieceColor Color;
    public int PositionIndex;

    public Piece(PieceType type, PieceColor color, int positionIndex)
    {
        Type = type;
        Color = color;
        PositionIndex = positionIndex;
    }
}