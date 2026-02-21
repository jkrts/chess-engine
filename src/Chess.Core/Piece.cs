namespace Chess.Core;


public enum PieceType
{
    // PieceType value is mean to be used during evaluation later
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

public record Piece
{
    public PieceType Type { get; init; }
    public PieceColor Color { get; init; }


    public Piece(PieceType type, PieceColor color)
    {
        Type = type;
        Color = color;
    }
}