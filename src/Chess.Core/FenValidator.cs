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
        '-'
    */
    private static readonly string ValidFenCharacters = "012345678pnbrqkPNBRQK/-w ";

    public static FenValidationResult Validate(string fen)
    {

        if (!HasOnlyValidFenCharacters(fen))
            return FenValidationResult.Invalid("FEN string contains invalid characters.");
        else if (HasConsecutiveWhitespaces(fen))
            return FenValidationResult.Invalid("FEN string contains consecutive spaces.");
        else if (!HasValidNumberOfParts(fen))
            return FenValidationResult.Invalid("FEN string contains invalid number of parts.");

        return FenValidationResult.Valid();

    }

    public static bool HasOnlyValidFenCharacters(string fen) =>
        fen.All(c => ValidFenCharacters.Contains(c));

    public static bool HasConsecutiveWhitespaces(string fen)
    {
        Regex regex = new Regex(@"\s{2,}");
        return regex.IsMatch(fen);
    }

    public static bool HasValidNumberOfParts(string fen) =>
        fen.Count(c => c == ' ') == 5;
}