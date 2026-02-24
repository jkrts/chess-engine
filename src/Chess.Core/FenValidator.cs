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


    private static readonly string _validFenCharacters = "0123456789pnbrqkPNBRQK/-w ";
    private static readonly Regex _consecutiveSpaces = new Regex(@"\s{2,}");

    public static FenValidationResult Validate(string fen)
    {

        if (string.IsNullOrWhiteSpace(fen))
            return FenValidationResult.Invalid("FEN string is null or empty");
        else if (!HasOnlyValidFenCharacters(fen))
            return FenValidationResult.Invalid("FEN string contains invalid characters.");
        else if (HasConsecutiveWhitespaces(fen))
            return FenValidationResult.Invalid("FEN string contains consecutive spaces.");
        else if (!HasValidNumberOfSpaces(fen))
            return FenValidationResult.Invalid("FEN string contains invalid number of spaces.");

        return FenValidationResult.Valid();

    }

    public static bool HasOnlyValidFenCharacters(string fen) =>
        fen.All(c => _validFenCharacters.Contains(c));

    public static bool HasConsecutiveWhitespaces(string fen)
    {
        return _consecutiveSpaces.IsMatch(fen);
    }

    public static bool HasValidNumberOfSpaces(string fen) =>
        fen.Count(c => c == ' ') == 5;
}