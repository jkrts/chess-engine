namespace Chess.Core;

public enum PieceType
{
    // PieceType value is meant to double as relative evaluation
    // weights to be used later and give each piece an int type
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