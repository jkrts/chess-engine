namespace Chess.Core.Tests;

public class FenValidatorTests
{
    [Fact]
    public void ValidFenCharacters_ShouldReturnFalse_WhenInputFenHasInvalidChars()
    {
        var input = "rnbqkbnr/p1pp1ppp/1p6/P7/1pPZ4/8/1P1PPPPP/RNBQKBNR w KQkq b6 0 5";

        Assert.False(FenValidator.HasOnlyValidFenCharacters(input));
    }

    [Fact]
    public void ValidFenCharacters_ShouldReturnTrue_WhenInputFenHasValidChars()
    {
        var input = "rnbqkbnr/p1pp1ppp/1p6/P7/1pPp4/8/1P1PPPPP/RNBQKBNR w KQkq b6 0 5";

        Assert.True(FenValidator.HasOnlyValidFenCharacters(input));
    }

    [Fact]
    public void HasValidNumberOfSpaces_ShouldReturnTrue_WhenInputFenHasValidNumberOfParts()
    {
        var input = "rnbqkbnr/p1pp1ppp/1p6/P7/1pPp4/8/1P1PPPPP/RNBQKBNR w KQkq b6 0 5";

        Assert.True(FenValidator.HasValidNumberOfSpaces(input));
    }

    [Fact]
    public void HasValidNumberOfSpaces_ShouldReturnFalse_WhenInputFenHasInvalidNumberOfParts()
    {
        var input = "rnbqkbnr/p1pp1ppp/1p6/P7/1pPp4/8/1P1PPPPP/RNBQKBNR w KQkq b6 0";

        Assert.False(FenValidator.HasValidNumberOfSpaces(input));
    }

    [Fact]
    public void HasConsecutiveWhiteSpaces_ShouldReturnTrue_WhenInputFenHasConsecutiveWhitespaces()
    {
        var input = "rnbqkbnr/p1pp1ppp/1p6/P7/1pPp4/8/1P1PPPPP/RNBQKBNR  w KQkq b6 0 5";

        Assert.True(FenValidator.HasConsecutiveWhitespaces(input));
    }

    [Fact]
    public void HasConsecutiveWhiteSpaces_ShouldReturnFalse_WhenInputFenDoesNotHaveConsecutiveWhitespaces()
    {
        var input = "rnbqkbnr/p1pp1ppp/1p6/P7/1pPp4/8/1P1PPPPP/RNBQKBNR w KQkq b6 0 5";

        Assert.False(FenValidator.HasConsecutiveWhitespaces(input));
    }


    [Fact]
    public void Validate_ShouldReturnInvalidResult_WhenFENStringIsNull()
    {
        string? input = null;

        var result = FenValidator.Validate(input!);

        Assert.False(result.IsValid);
        Assert.NotNull(result.ErrorMessage);
    }

    [Fact]
    public void Validate_ShouldReturnValidResult_WhenFENStringIsStartingPosition()
    {
        string input = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1";

        var result = FenValidator.Validate(input);

        Assert.True(result.IsValid);
        Assert.Null(result.ErrorMessage);
    }

    [Fact]
    public void Validate_ShouldReturnInvalidResult_WhenFENStringIsInvalid()
    {
        string input = "rnbqkbnr/ppppppp p/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1";

        var result = FenValidator.Validate(input);

        Assert.False(result.IsValid);
        Assert.NotNull(result.ErrorMessage);
    }

}