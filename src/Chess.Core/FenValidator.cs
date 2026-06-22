using System.Reflection.Metadata.Ecma335;
using System.Runtime.Serialization;
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

    private static readonly string _validFenCharacters = "0123456789pnbrqkPNBRQK/-w ";
    private static readonly Regex _consecutiveWhitepaces = new Regex(@"\s{2,}", RegexOptions.Compiled);
    private static readonly string _validPiecePlacementChars = "12345678pnbrqkPNBRQK/";
    
    public static FenValidationResult Validate(string fen)
    {
        if (string.IsNullOrEmpty(fen))
            return FenValidationResult.Invalid("Null Input.");

        if (!HasOnlyValidFenCharacters(fen))
            return FenValidationResult.Invalid("FEN string contains invalid characters.");

        if (!HasNoConsecutiveWhitespaces(fen))
            return FenValidationResult.Invalid("FEN string contains consecutive whitespaces.");
        
        if (!HasNoLeadingTrailingWhitespace(fen))
            return FenValidationResult.Invalid("FEN string begins or ends with a whitespace.");

        if (!HasValidNumberOfSpaces(fen))
            return FenValidationResult.Invalid("FEN string has invalid number of spaces.");
        
        var fenParts = fen.Split(' ', StringSplitOptions.RemoveEmptyEntries);

        if (!HasDataForEachPart(fenParts))
            return FenValidationResult.Invalid("FEN string part count is invalid.");

        // Validate part 1. Piece placement characters
        if (!HasValidPiecePlacementChars(fenParts[0]))
            return FenValidationResult.Invalid("FEN string has invalid characters in piece placement section.");
        
        // Validate part 2. Active color
        if (!HasValidActiveColorChar(fenParts[1]))
            return FenValidationResult.Invalid("FEN string has invalid active color.");

        // Validate part 3. Castling availability
        if (!HasValidCastlingAvailabilityChars(fenParts[2]))
            return FenValidationResult.Invalid("FEN string has invalid castling availability.");

        // Validate part 4. En passant target square
        if (!HasValidEnPassantTargetSquareChars(fenParts[3]))
            return FenValidationResult.Invalid("FEN string has invalid en passant target square.");

        // Validate part 5. Halfmove clock
        if (!HasValidHalfMoveClockChars(fenParts[4]))
            return FenValidationResult.Invalid("FEN string has invalid halfmove clock.");
        
        // Validate part 6. Fullmove number
        if (!HasValidFullMoveNumberChars(fenParts[5]))
            return FenValidationResult.Invalid("FEN string has invalid fullmove number.");

        return FenValidationResult.Valid();

    }

    public static bool HasOnlyValidFenCharacters(string fen)
    {
        return fen.All(c => _validFenCharacters.Contains(c));
    }

    public static bool HasNoConsecutiveWhitespaces(string fen)
    {
        if (_consecutiveWhitepaces.IsMatch(fen))
            return false;
        return true;
    }

    public static bool HasNoLeadingTrailingWhitespace(string fen)
    {
        if (char.IsWhiteSpace(fen[0]) || char.IsWhiteSpace(fen[fen.Length - 1]))
            return false;
        return true;
    }

    public static bool HasValidNumberOfSpaces(string fen)
    {
        if (fen.Count(c => c == ' ') != 5)
            return false;
        return true;
    }

    public static bool HasDataForEachPart(string[] fenParts)
    {
        if (fenParts.Length != 6)
            return false;
        return true;
    }

    public static bool HasValidPiecePlacementChars(string fenPartPiecePlacement)
    {
        return fenPartPiecePlacement.All(c => _validPiecePlacementChars.Contains(c));
    }

    public static bool HasValidActiveColorChar(string fenPartActiveColor)
    {
        if (fenPartActiveColor.Length != 1)
            return false;
        if (fenPartActiveColor[0] != 'w' && fenPartActiveColor[0] != 'b')
            return false;
        return true;
    }

    public static bool HasValidCastlingAvailabilityChars(string fenPartCastlingAvailability)
    {
        if (fenPartCastlingAvailability.Length < 1 || fenPartCastlingAvailability.Length > 4)
            return false;
        if (fenPartCastlingAvailability == "-")
            return true;
        return fenPartCastlingAvailability.All(c => c is 'k' or 'K' or 'q' or 'Q');
    }

    public static bool HasValidEnPassantTargetSquareChars(string fenPartEnPassant)
    {
        if (fenPartEnPassant == "-")
            return true;
        if (fenPartEnPassant.Length != 2)
            return false;
        
        char file = fenPartEnPassant[0];
        char rank = fenPartEnPassant[1];

        return file is >= 'a' and <= 'h' && rank is '3' or '6';
    }

    public static bool HasValidHalfMoveClockChars(string fenPartHalfMove)
    {
        if (int.TryParse(fenPartHalfMove, out int number) && number >= 0)
            return true;
        return false;
    }

    public static bool HasValidFullMoveNumberChars(string fenPartFullMove)
    {
        if (int.TryParse(fenPartFullMove, out int number) && number > 0)
            return true;
        return false;
    }

}