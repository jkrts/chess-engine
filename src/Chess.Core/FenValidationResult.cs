namespace Chess.Core;

public class FenValidationResult
{
    public bool IsValid { get; }
    public string? ErrorMessage { get; }

    private FenValidationResult(bool isValid, string? errorMessage)
    {
        IsValid = isValid;
        ErrorMessage = errorMessage; 
    }

    public static FenValidationResult Valid() => new(true, null);
    public static FenValidationResult Invalid(string reason) => new(false, reason);
}