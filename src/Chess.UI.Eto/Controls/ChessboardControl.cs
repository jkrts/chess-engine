using System.Reflection;
using Eto.Drawing;
using Eto.Forms;
using Chess.Core;

namespace Chess.UI.Eto.Controls;

public sealed class ChessboardControl : Drawable
{
    private ChessPosition _chessPosition;
    private readonly Dictionary<int, Bitmap> _pieceImages = [];
    private Size _boardSize;
    private float _squareSize;
    private string? _selectedSquare;

    private readonly Font _coordinateFont = new("Monospace", 9);
    private readonly SolidBrush _coordinateBrush = new(Colors.Gray);


    public ChessboardControl(ChessPosition chessPosition, Size boardSize)
    {
        _chessPosition = chessPosition;
        // repaint when resized
        this.Size = boardSize + new Size(5, 5);
        _boardSize = boardSize;
        _squareSize = _boardSize.Width / 8;
        this.SizeChanged += (sender, e) => Invalidate();

        LoadPieceImages();

        this.CanFocus = true;
        this.MouseDown += ChessboardControl_MouseDown;
    }

    public void UpdateChessPosition(ChessPosition newChessPosition)
    {
        _chessPosition = newChessPosition;
    }

    private void LoadPieceImages()
    {
        var assembly = Assembly.GetExecutingAssembly();

        var pieceMap = new Dictionary<int, string>
        {
            {  1, "w_pawn_png_128px.png"},
            {  2, "w_knight_png_128px.png"},
            {  3, "w_king_png_128px.png"},
            {  5, "w_bishop_png_128px.png"},
            {  6, "w_rook_png_128px.png"},
            {  7, "w_queen_png_128px.png"},
            { -1, "b_pawn_png_128px.png"},
            { -2, "b_knight_png_128px.png"},
            { -3, "b_king_png_128px.png"},
            { -5, "b_bishop_png_128px.png"},
            { -6, "b_rook_png_128px.png"},
            { -7, "b_queen_png_128px.png"}
        };

        foreach (var kvp in pieceMap)
        {
            string resourceName = $"Chess.UI.Eto.Assets.Pieces.small_no_shadow.{kvp.Value}";  // ← change to match your project!

            try
            {
                using var stream = assembly.GetManifestResourceStream(resourceName);
                if (stream == null)
                {
                    Console.WriteLine($"Resource not found: {resourceName}");
                    continue;
                }

                var bitmap = new Bitmap(stream);
                _pieceImages[kvp.Key] = bitmap;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to load {resourceName}: {ex.Message}");
            }
        }
    }


    protected override void OnPaint(PaintEventArgs e)
    {
        
        base.OnPaint(e);

        var g = e.Graphics;
        g.ImageInterpolation = ImageInterpolation.High;
        DrawChessboard(g);
        DrawPieces(g);
     

    }

    private void DrawPieces(Graphics g)
    {

        foreach (var (posIndex, piece) in _chessPosition.Pieces)
        {
            // White = 1, Black = 2. Pawn = 1, so white would be 1*1, black would be -1*1
            //                    White = 1          Pawn = 1     = 1
            //                    Black = -1          Pawn = 1     = -1
            int pieceValue = (int)piece.Color * (int)piece.Type;

             // 87.5 x 87.5
            Image img = _pieceImages[pieceValue];
            float scale = Math.Min(_squareSize * .8f / img.Width, _squareSize * .8f / img.Height);
            int drawWidth = (int)(img.Width * scale);
            int drawHeight = (int)(img.Height * scale);
            float x = (_squareSize - drawWidth) / 2f;
            float y = (_squareSize - drawHeight) / 2f;

            var pieceSquare = _chessPosition.IndexToSquare(posIndex);
            var file = (int)(pieceSquare[0] - 'a');
            var rank = (int)7-(pieceSquare[1] - '1'); // flip rank for A8 top left -visual

            float xOffset = (float)(file * _squareSize);
            float yOffset = (float)(rank * _squareSize);

            var rect = new RectangleF(x+xOffset, y+yOffset, drawWidth, drawHeight);
            g.DrawImage(img, rect);
        }
    }

    private void DrawChessboard(Graphics g)
    {
        const int files = 8;
        const int ranks = 8;

        // decide starting color
        bool isDark = true;

        for (int rank = 0; rank < ranks; rank++)
        {
            for (int file = 0; file < files; file++)
            {
                var color = isDark ? Colors.LightGreen : Colors.Cornsilk;

                var rect = new RectangleF(
                    file * _squareSize,
                    rank * _squareSize,
                    _squareSize,
                    _squareSize);

                g.FillRectangle(color, rect);

                g.DrawRectangle(Colors.Gray, rect);

                isDark = !isDark;
            }
            isDark = !isDark;
        }

        DrawCoordinates(g);
    }

    private void DrawCoordinates(Graphics g)
    {
        for (int file = 0; file < 8; file++)
        {
            char letter = (char)(('a') + file);
            var text = letter.ToString();
            var x = file * _squareSize + _squareSize / 2 - 5;
            var y = 8 * _squareSize - 18;
            g.DrawText(_coordinateFont, _coordinateBrush, x, y, text);
        }

        for (int rank = 0; rank < 8; rank++)
        {
            int number = 8 - rank;
            var text = number.ToString();
            var x = 4;
            var y = rank * _squareSize + _squareSize / 2 - 6;
            g.DrawText(_coordinateFont, _coordinateBrush, x, y, text);
        }
    }

    private void ChessboardControl_MouseDown(object? sender, MouseEventArgs e)
    {
        if (e.Buttons != MouseButtons.Primary)
            return;

        PointF clickPos = e.Location;

        int file = (int)(clickPos.X / _squareSize);
        int rank = 7 - (int)(clickPos.Y / _squareSize);

        if (file < 0 || file >= 8|| rank < 0 || rank >= 8)
            return;
        
        char c = (char)('a' + (file));
        string sq = c + (rank + 1).ToString(); 
      
        var pieceAt = _chessPosition.GetPieceAt(sq);

        if (pieceAt == null)
        {
            _selectedSquare = null;
        }
        else
        {
            _selectedSquare = sq;
        }

        Invalidate();
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            _coordinateFont.Dispose();
            _coordinateBrush.Dispose();
            foreach (var bmp in _pieceImages.Values)
                bmp.Dispose();
            _pieceImages.Clear();
        }
        base.Dispose(disposing);
    }
    
}