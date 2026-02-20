using Chess.Core;

namespace Chess.Core.Tests;

public class BoardTests
{
    private readonly Board _board = new Board();

    [Theory]
    [InlineData("a8", 0)]
    [InlineData("h8", 7)]
    [InlineData("c6", 18)]
    [InlineData("e4", 36)]
    [InlineData("g3", 46)]
    [InlineData("a1", 56)]
    [InlineData("h1", 63)]
    public void SquareToIndex_WhenCalled_ShouldReturnValidBoardPositionIndex(string square, int expected)
    {
        var index = _board.SquareToIndex(square);

        Assert.Equal(expected, index);
    }


    [Theory]
    [InlineData(0, "a8")]
    [InlineData(7, "h8")]
    [InlineData(18, "c6")]
    [InlineData(36, "e4")]
    [InlineData(46, "g3")]
    [InlineData(56, "a1")]
    [InlineData(63, "h1")]
    public void IndexToSquare_WhenCalled_ShouldReturnValidSquare(int index, string expected)
    {
        var square = _board.IndexToSquare(index);

        Assert.Equal(expected, square);
    }
}