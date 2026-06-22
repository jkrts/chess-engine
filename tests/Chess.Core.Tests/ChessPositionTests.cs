namespace Chess.Core.Tests;

public class ChessPositionTests
{
    private readonly ChessPosition _chessPosition = new ChessPosition();

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
        var index = _chessPosition.SquareToIndex(square);

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
        var square = _chessPosition.IndexToSquare(index);

        Assert.Equal(expected, square);
    }


    [Fact]
    public void SquareToIndex_AndIndexToSquare_AreInverseForAllSquares()
    {
        for (int i = 0; i < 64; i++)
        {
            string square = _chessPosition.IndexToSquare(i);
            int roundTripped = _chessPosition.SquareToIndex(square);
            Assert.True(i == roundTripped, $"Failed for index {i} (square '{square}')");
        }
    }

    [Fact]
    public void GetPieceAt_WhenSquareHasPiece_ReturnsPiece()
    {
        var piece = _chessPosition.GetPieceAt("a8");

        Assert.NotNull(piece);
        Assert.Equal(PieceType.Rook, piece.Type);
        Assert.Equal(PieceColor.Black, piece.Color);
    }

    [Fact]
    public void GetPieceAt_WhenSquareIsEmpty_ReturnsNull()
    {
        var piece = _chessPosition.GetPieceAt("e4");

        Assert.Null(piece);
    }

    [Fact]
    public void GetPieceAt_WhenSquareHasWhitePiece_ReturnsPiece()
    {
        var piece = _chessPosition.GetPieceAt("e1");

        Assert.NotNull(piece);
        Assert.Equal(PieceType.King, piece.Type);
        Assert.Equal(PieceColor.White, piece.Color);
    }

}