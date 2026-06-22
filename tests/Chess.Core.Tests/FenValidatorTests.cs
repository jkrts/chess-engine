namespace Chess.Core.Tests;

public class FenValidatorTests
{
    // HasOnlyValidFenCharacters
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

    // HasNoConsecutiveWhitespaces
    [Fact]
    public void HasNoConsecutiveWhiteSpaces_ShouldReturnFalse_WhenInputFenHasConsecutiveWhitespaces()
    {
        var input = "rnbqkbnr/p1pp1ppp/1p6/P7/1pPp4/8/1P1PPPPP/RNBQKBNR  w KQkq b6 0 5";

        Assert.False(FenValidator.HasNoConsecutiveWhitespaces(input));
    }

    [Fact]
    public void HasNoConsecutiveWhiteSpaces_ShouldReturnTrue_WhenInputFenDoesNotHaveConsecutiveWhitespaces()
    {
        var input = "rnbqkbnr/p1pp1ppp/1p6/P7/1pPp4/8/1P1PPPPP/RNBQKBNR w KQkq b6 0 5";

        Assert.True(FenValidator.HasNoConsecutiveWhitespaces(input));
    }

    // HasNoLeadingTrailingWhitespace
    [Fact]
    public void HasNoLeadingTrailingWhitespace_ShouldReturnTrue_WhenInputFenHasNoLeadingTrailingWhitespace()
    {
        var input = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1";

        Assert.True(FenValidator.HasNoLeadingTrailingWhitespace(input));
    }

    // HasValidNumberOfSpaces
    [Fact]
    public void HasValidNumberOfSpaces_ShouldReturnFalse_WhenInputFenHasInvalidNumberOfParts()
    {
        var input = "rnbqkbnr/p1pp1ppp/1p6/P7/1pPp4/8/1P1PPPPP/RNBQKBNR w KQkq b6 0";

        Assert.False(FenValidator.HasValidNumberOfSpaces(input));
    }

    [Fact]
    public void HasValidNumberOfSpaces_ShouldReturnTrue_WhenInputFenHasValidNumberOfSpaces()
    {
        var input = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1";

        Assert.True(FenValidator.HasValidNumberOfSpaces(input));
    }

    // Parts
    // HasDataForEachPart
    [Fact]
    public void HasDataForEachPart_ShouldReturnTrue_WhenInputFenHasDataForEachPart()
    {
        var input = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1";

        var inputParts = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);

        Assert.True(FenValidator.HasDataForEachPart(inputParts));
    }

    [Fact]
    public void HasDataForEachPart_ShouldReturnFalse_WhenInputFenDoesNotHaveDataForEachPart()
    {
        var input = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR   KQkq - 0 1";

        var inputParts = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);

        Assert.False(FenValidator.HasDataForEachPart(inputParts));
    }

    // HasValidPiecePlacementChars
    [Fact]
    public void HasValidPiecePlacementChars_ShouldReturnTrue_WhenPiecePlacementPartHasValidChars()
    {
        var input = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR";

        Assert.True(FenValidator.HasValidPiecePlacementChars(input));
    }

    [Fact]
    public void HasValidPiecePlacementChars_ShouldReturnFalse_WhenPiecePlacementPartDoesNotHaveValidChars()
    {
        var input = "rnbqkbxr/pppppppp/8/8/8/8/PPPXPPPP/RNBQKBNR";

        Assert.False(FenValidator.HasValidPiecePlacementChars(input));
    }

    // HasValidActiveColorChar
    [Fact]
    public void HasValidActiveColorChar_ShouldReturnTrue_WhenActiveColorPartHasValidChars()
    {
        var input = "w";

        Assert.True(FenValidator.HasValidActiveColorChar(input));
    }
    
    [Fact]
    public void HasValidActiveColorChar_ShouldReturnFalse_WhenActiveColorPartDoesNotHaveValidChars()
    {
        var input = "f";

        Assert.False(FenValidator.HasValidActiveColorChar(input));
    }

    // HasValidCastlingAvailabilityChars
    [Fact]
    public void HasValidCastlingAvailabilityChars_ShouldReturnTrue_WhenHasValidCastlingAvailabilityPartHasValidChars()
    {
        var input = "kKqQ";

        Assert.True(FenValidator.HasValidCastlingAvailabilityChars(input));
    }
    
    [Fact]
    public void HasValidCastlingAvailabilityChars_ShouldReturnFalse_WhenHasValidCastlingAvailabilityPartDoesNotHaveValidChars()
    {
        var input = "tg";

        Assert.False(FenValidator.HasValidCastlingAvailabilityChars(input));
    }

    // HasValidEnPassantTargetSquareChars
    [Fact]
    public void HasValidEnPassantTargetSquareChars_ShouldReturnTrue_WhenHasValidEnPassantTargetSquarePartHasValidChars()
    {
        var input = "e3";

        Assert.True(FenValidator.HasValidEnPassantTargetSquareChars(input));
    }
    
    [Fact]
    public void HasValidEnPassantTargetSquareChars_ShouldReturnFalse_WhenHasValidEnPassantTargetSquarePartDoesNotHaveValidChars()
    {
        var input = "c5";

        Assert.False(FenValidator.HasValidEnPassantTargetSquareChars(input));
    }

    // HasValidHalfMoveClockChars
    [Fact]
    public void HasValidHalfMoveClockChars_ShouldReturnTrue_WhenHasValidHalfMoveClockPartHasValidChars()
    {
        var input = "0";

        Assert.True(FenValidator.HasValidHalfMoveClockChars(input));
    }
    
    [Fact]
    public void HasValidHalfMoveClockChars_ShouldReturnFalse_WhenHasValidHalfMoveClockPartDoesNotHaveValidChars()
    {
        var input = "d";

        Assert.False(FenValidator.HasValidHalfMoveClockChars(input));
    }

    // HasValidFullMoveNumberChars
    [Fact]
    public void HasValidFullMoveNumberChars_ShouldReturnTrue_WhenHasValidFullMoveNumberPartHasValidChars()
    {
        var input = "4";

        Assert.True(FenValidator.HasValidFullMoveNumberChars(input));
    }
    
    [Fact]
    public void HasValidFullMoveNumberChars_ShouldReturnFalse_WhenHasValidFullMoveNumberPartDoesNotHaveValidChars()
    {
        var input = "0";

        Assert.False(FenValidator.HasValidFullMoveNumberChars(input));
    }

    // Validate
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