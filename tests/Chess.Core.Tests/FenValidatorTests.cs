using Xunit;
using Chess.Core;

namespace Chess.Core.Tests;

public class FenValidatorTests
{
    // HasOnlyValidFenCharacters
    [Theory]
    [InlineData("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1")]
    [InlineData("rnbqkbnr/p1pp1ppp/1p6/P7/1pPp4/8/1P1PPPPP/RNBQKBNR w KQkq b6 0 5")]
    public void ValidFenCharacters_ShouldReturnTrue_WhenInputFenHasValidChars(string input)
    {
        Assert.True(FenValidator.HasOnlyValidFenCharacters(input));
    }

    [Theory]
    [InlineData("rnbqkbnr/p1pp1ppp/1p6/P7/1pPZ4/8/1P1PPPPP/RNBQKBNR w KQkq b6 0 5")]
    [InlineData("6=&rnbqkbnr/p1pp1ppp/1p6/P7/1pP4/8/1P1PPPPP/RNBQKBNR w KQkq b6 0 5")]
    public void ValidFenCharacters_ShouldReturnFalse_WhenInputFenHasInvalidChars(string input)
    {
        Assert.False(FenValidator.HasOnlyValidFenCharacters(input));
    }

    // HasNoConsecutiveWhitespaces
    [Theory]
    [InlineData("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1")]
    public void HasNoConsecutiveWhiteSpaces_ShouldReturnTrue_WhenInputFenDoesNotHaveConsecutiveWhitespaces(string input)
    {
        Assert.True(FenValidator.HasNoConsecutiveWhitespaces(input));
    }

    [Theory]
    [InlineData("rnbqkbnr/p1pp1ppp/1p6/P7/1pPp4/8/1P1PPPPP/RNBQKBNR  w KQkq b6 0 5")]
    [InlineData("rnbqkbnr/p1pp1ppp/1p6/P7/1pPp4/8/1P1PPPPP/RNBQKBNR w KQkq b6     0 5")]
    [InlineData("    rnbqkbnr/p1pp1ppp/1p6/P7/1pPp4/8/1P1PPPPP/RNBQKBNR wKQkqb605")]
    public void HasNoConsecutiveWhiteSpaces_ShouldReturnFalse_WhenInputFenHasConsecutiveWhitespaces(string input)
    {
        Assert.False(FenValidator.HasNoConsecutiveWhitespaces(input));
    }

    // HasNoLeadingTrailingWhitespace
    [Theory]
    [InlineData("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1")]
    public void HasNoLeadingTrailingWhitespace_ShouldReturnTrue_WhenInputFenHasNoLeadingTrailingWhitespace(string input)
    {
        Assert.True(FenValidator.HasNoLeadingTrailingWhitespace(input));
    }

    [Theory]
    [InlineData("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1 ")]
    [InlineData(" rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1")]
    [InlineData(" rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1 ")]
    public void HasNoLeadingTrailingWhitespace_ShouldReturnFalse_WhenInputFenHasLeadingTrailingWhitespace(string input)
    {
        Assert.False(FenValidator.HasNoLeadingTrailingWhitespace(input));
    }

    // Parts
    // HasDataForEachPart
    [Theory]
    [InlineData("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1")]
    [InlineData("4k3/8/8/8/8/8/4P3/4K3 w - - 5 39")]
    public void HasDataForEachPart_ShouldReturnTrue_WhenInputFenHasDataForEachPart(string input)
    {
        var inputParts = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);

        Assert.True(FenValidator.HasDataForEachPart(inputParts));
    }

    [Theory]
    [InlineData("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR   KQkq - 0 1")]
    [InlineData("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR   KQkq -  0 1")]
    [InlineData("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR KQkq- 01")]
    public void HasDataForEachPart_ShouldReturnFalse_WhenInputFenDoesNotHaveDataForEachPart(string input)
    {
        var inputParts = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);

        Assert.False(FenValidator.HasDataForEachPart(inputParts));
    }

    // HasValidPiecePlacementChars
    [Theory]
    [InlineData("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR")]
    [InlineData("rnbqkbnr/pppppppp/8/8/4P3/8/PPPP1PPP/RNBQKBNR")]
    [InlineData("rnbqkbnr/pp1ppppp/8/2p5/4P3/8/PPPP1PPP/RNBQKBNR")]
    [InlineData("rnbqkbnr/pp1ppppp/8/2p5/4P3/5N2/PPPP1PPP/RNBQKB1R")]
    [InlineData("4k3/8/8/8/8/8/4P3/4K3")]
    public void HasValidPiecePlacementChars_ShouldReturnTrue_WhenPiecePlacementPartHasValidChars(string input)
    {
        Assert.True(FenValidator.HasValidPiecePlacementChars(input));
    }

    [Theory]
    [InlineData("rnbqkbxr/pppppppp/8/8/8/8/PPP PPPP/RNBQKBNR")]
    [InlineData("rnbqkbxrpppppppp/8/8/8/8/PPPPPPP/RNBQKBNR")]
    [InlineData("rnbqkbxr/ppppppp/8/8/8/8/PPPPPPP/RNBQKBNR")]       // rank with 7
    [InlineData("rnbqkbxr/pppppppp/8/8/8/8/PPPPPPP1/RNBQKBNR")]     // rank with 9
    [InlineData("rnbqkbxrpppppppp/8/8/8/8/PPPPPPP/8/RNBQKBNR")]     // 9 ranks
    [InlineData("rnbqkbnr/pppppppp/8/8/8/8/111PPPPP/RNBQKBNR")]     // consecutive digits
    public void HasValidPiecePlacementChars_ShouldReturnFalse_WhenPiecePlacementPartDoesNotHaveValidChars(string input)
    {
        Assert.False(FenValidator.HasValidPiecePlacementChars(input));
    }

    // HasValidActiveColorChar
    [Theory]
    [InlineData("w")]
    [InlineData("b")]
    public void HasValidActiveColorChar_ShouldReturnTrue_WhenActiveColorPartHasValidChars(string input)
    {
        Assert.True(FenValidator.HasValidActiveColorChar(input));
    }
    
    [Theory]
    [InlineData("-")]
    [InlineData("X")]
    [InlineData("W")]
    [InlineData("B")]
    [InlineData("")]
    [InlineData(" ")]
    public void HasValidActiveColorChar_ShouldReturnFalse_WhenActiveColorPartDoesNotHaveValidChars(string input)
    {
        Assert.False(FenValidator.HasValidActiveColorChar(input));
    }

    // HasValidCastlingAvailabilityChars
    [Theory]
    [InlineData("-")]       // White and Blaock None
    [InlineData("KQkq")]    // White and Black Full
    [InlineData("Kk")]      // White and Black Kingside
    [InlineData("Qq")]      // White and Black Queenside
    [InlineData("K")]       // White Kingside only
    [InlineData("q")]       // Black Queenside only
    [InlineData("Kq")]      // White Kingside and Black Queenside
    public void HasValidCastlingAvailabilityChars_ShouldReturnTrue_WhenHasValidCastlingAvailabilityPartHasValidChars(string input)
    { 
        Assert.True(FenValidator.HasValidCastlingAvailabilityChars(input));
    }
    
    [Theory]
    [InlineData("")]        // empty string
    [InlineData("kK")]       // Wrong order
    [InlineData("qK")]       // Wrong order
    [InlineData("QK")]       // Wrong order
    [InlineData("qQ")]       // Wrong order
    [InlineData("ts")]       // Wrong characters
    [InlineData("KQkqK")]    // Too many valid characters
    public void HasValidCastlingAvailabilityChars_ShouldReturnFalse_WhenHasValidCastlingAvailabilityPartDoesNotHaveValidChars(string input)
    {
        Assert.False(FenValidator.HasValidCastlingAvailabilityChars(input));
    }

    // HasValidEnPassantTargetSquareChars
    [Theory]
    [InlineData("e3")]
    [InlineData("b6")]
    [InlineData("a3")]
    [InlineData("h6")]
    public void HasValidEnPassantTargetSquareChars_ShouldReturnTrue_WhenHasValidEnPassantTargetSquarePartHasValidChars(string input)
    {
        Assert.True(FenValidator.HasValidEnPassantTargetSquareChars(input));
    }
    
    [Theory]
    [InlineData("c5")]
    [InlineData("a1")]
    [InlineData("a9")]
    [InlineData("h0")]
    [InlineData("aa")]
    [InlineData("a 1")]
    public void HasValidEnPassantTargetSquareChars_ShouldReturnFalse_WhenHasValidEnPassantTargetSquarePartDoesNotHaveValidChars(string input)
    {
        Assert.False(FenValidator.HasValidEnPassantTargetSquareChars(input));
    }

    // HasValidHalfMoveClockChars
    [Theory]
    [InlineData("0")]
    [InlineData("40")]
    public void HasValidHalfMoveClockChars_ShouldReturnTrue_WhenHasValidHalfMoveClockPartHasValidChars(string input)
    {
        Assert.True(FenValidator.HasValidHalfMoveClockChars(input));
    }
    
    [Theory]
    [InlineData("d")]
    [InlineData("-20")]
    [InlineData("1 0")]
    public void HasValidHalfMoveClockChars_ShouldReturnFalse_WhenHasValidHalfMoveClockPartDoesNotHaveValidChars(string input)
    {
        Assert.False(FenValidator.HasValidHalfMoveClockChars(input));
    }

    // HasValidFullMoveNumberChars
    [Theory]
    [InlineData("1")]
    [InlineData("99")]
    public void HasValidFullMoveNumberChars_ShouldReturnTrue_WhenHasValidFullMoveNumberPartHasValidChars(string input)
    {
        Assert.True(FenValidator.HasValidFullMoveNumberChars(input));
    }
    
    [Theory]
    [InlineData("-49")]
    [InlineData("-")]
    [InlineData("0")]
    [InlineData("X")]
    [InlineData("bjds")]
    [InlineData("1 2")]
    public void HasValidFullMoveNumberChars_ShouldReturnFalse_WhenHasValidFullMoveNumberPartDoesNotHaveValidChars(string input)
    {
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
    public void Validate_ShouldReturnInvalidResult_WhenFENStringIsEmpty()
    {
        string input = String.Empty;

        var result = FenValidator.Validate(input);

        Assert.False(result.IsValid);
        Assert.NotNull(result.ErrorMessage);
    }

    [Theory]
    [InlineData("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1")]
    [InlineData("rnbqkbnr/pppppppp/8/8/4P3/8/PPPP1PPP/RNBQKBNR b KQkq e3 0 1")]
    [InlineData("rnbqkbnr/pp1ppppp/8/2p5/4P3/8/PPPP1PPP/RNBQKBNR w KQkq c6 0 2")]
    [InlineData("rnbqkbnr/pp1ppppp/8/2p5/4P3/5N2/PPPP1PPP/RNBQKB1R b KQkq - 1 2")]
    [InlineData("4k3/8/8/8/8/8/4P3/4K3 w - - 5 39")]
    [InlineData("rnbqkb1r/pppppppp/5n2/8/2P5/2N5/PP1PPPPP/R1BQKBNR b KQkq - 2 3")]
    public void Validate_ShouldReturnValidResult_WhenFENStringIsValid(string input)
    {
        var result = FenValidator.Validate(input);

        Assert.True(result.IsValid);
        Assert.Null(result.ErrorMessage);
    }

    [Theory]
    [InlineData("rnbqkbnr/ppppppp p/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1")]
    [InlineData("rnbqkbnr/ppppxppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1")]
    [InlineData("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR  w KQkq - 0 1")]
    [InlineData(" rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 01")]
    [InlineData("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 01")]
    [InlineData("rnbqrkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1")]
    [InlineData("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR r KQkq - 0 1")]
    [InlineData("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w kqKQ - 0 1")]
    [InlineData("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq e8 0 1")]
    [InlineData("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - -7 1")]
    [InlineData("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 0")]
    [InlineData(" ")]
    [InlineData("40")]
    [InlineData("-x-")]
    [InlineData("  hjdas  asdasasd")]
    public void Validate_ShouldReturnInvalidResult_WhenFENStringIsInvalid(string input)
    {
        var result = FenValidator.Validate(input);

        Assert.False(result.IsValid);
        Assert.NotNull(result.ErrorMessage);
    }

}