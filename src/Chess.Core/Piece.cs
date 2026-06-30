namespace Chess.Core;

public enum PieceType
{
    Pawn = 1,
    Knight,
    King,
    Bishop, 
    Rook,
    Queen
}

public enum PieceColor
{
    White = 1,
    Black = -1
}

public record Piece(PieceType Type, PieceColor Color);