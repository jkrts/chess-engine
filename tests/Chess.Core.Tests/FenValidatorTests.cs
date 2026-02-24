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
    public void HasValidNumberOfParts_ShouldReturnTrue_WhenInputFenHasValidNumberOfParts()
    {
        var input = "rnbqkbnr/p1pp1ppp/1p6/P7/1pPp4/8/1P1PPPPP/RNBQKBNR w KQkq b6 0 5";

        Assert.True(FenValidator.HasValidNumberOfParts(input));
    }

    [Fact]
    public void HasValidNumberOfParts_ShouldReturnFalse_WhenInputFenHasInvalidNumberOfParts()
    {
        var input = "rnbqkbnr/p1pp1ppp/1p 6/P7/1pPp4/8/1P1PPPPP/RNBQKBNR w KQkq b6 0 5";

        Assert.False(FenValidator.HasValidNumberOfParts(input));
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

}