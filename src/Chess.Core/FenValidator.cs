using System.Text.RegularExpressions;

namespace Chess.Core;

public static class FenValidator
{
    /*
    ValidFenCharacters
        '1', '2', '3', '4', '5', '6', '7', '8',
        'p', 'n', 'b', 'r', 'q', 'k',
        'P', 'N', 'B', 'R', 'Q', 'K',
        '/',
        ' ',
        '-',
        '9'
    */

    // TODO
    // Break validation down into each part
    // 1. Move List
    // 2. Active Color
    // 3. Castling Availability
    // 4. En Passant Target
    // 5. HalfMove clock
    // 6. Fullmove number
    // 7. Chess Logic for valid position????


    private static readonly string _validPiecePlacementChars = "12345678pnbrqkPNBRQK/";
    private static readonly Regex _consecutiveSpaces = new Regex(@"\s{2,}");

    public static FenValidationResult Validate(string fen)
    {

        if (string.IsNullOrWhiteSpace(fen))
            return FenValidationResult.Invalid("FEN string is null or empty");
        else if (!HasZeroConsecutiveWhitespaces(fen))
            return FenValidationResult.Invalid("FEN string contains consecutive spaces.");
        else if (!HasValidNumberOfSpaces(fen))
            return FenValidationResult.Invalid("FEN string contains invalid number of spaces.");


        var fenParts = fen.Split(' ');

        if(!HasValidPiecePlacement(fenParts[0]))
            return FenValidationResult.Invalid("Invalid Piece Placement in FEN string"); 


        return FenValidationResult.Valid();

    }

    public static bool HasZeroConsecutiveWhitespaces(string fen) =>
        _consecutiveSpaces.IsMatch(fen) ? false : true;

    public static bool HasValidNumberOfSpaces(string fen) =>
        fen.Count(c => c == ' ') == 5;


    public static bool HasValidPiecePlacement(string piecePlacement)
    {
        // TODO

        if(!piecePlacement.All(c => _validPiecePlacementChars.Contains(c)))
            return false;



        return true;
    }

    public static bool HasValidColor(string fen)
    {
        // TODO
        return true;
    }

    public static bool HasValidCastling(string fen)
    {
        // TODO
        return true;
    }

    public static bool HasValidEnPassantTarget(string fen)
    {
        // TODO
        return true;
    }

    public static bool HasValidHalfmoveClock(string fen)
    {
        // TODO
        return true;
    }

    public static bool HasValidFullmoveNumber(string fen)
    {
        // TODO
        return true;
    }

    public static bool HasOnlyValidFenCharacters(string fen)
    {
        // TODO
        return true;
    }

    public static bool HasConsecutiveWhitespaces(string fen)
    {
        // TODO
        return true;
    }
}