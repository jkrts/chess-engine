namespace Chess.Core;

public class ChessPosition
{
    public const string STARTING_FEN = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1";
    private Dictionary<int, Piece> _pieces = new ();
    public IReadOnlyDictionary<int, Piece> Pieces => _pieces;

    readonly int[] mailbox120 = new int[120] {
        -1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
        -1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
        -1,  0,  1,  2,  3,  4,  5,  6,  7, -1,
        -1,  8,  9, 10, 11, 12, 13, 14, 15, -1,
        -1, 16, 17, 18, 19, 20, 21, 22, 23, -1,
        -1, 24, 25, 26, 27, 28, 29, 30, 31, -1,
        -1, 32, 33, 34, 35, 36, 37, 38, 39, -1,
        -1, 40, 41, 42, 43, 44, 45, 46, 47, -1,
        -1, 48, 49, 50, 51, 52, 53, 54, 55, -1,
        -1, 56, 57, 58, 59, 60, 61, 62, 63, -1,
        -1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
        -1, -1, -1, -1, -1, -1, -1, -1, -1, -1
    };

    readonly int[] mailbox64 = new int[64] {
        21, 22, 23, 24, 25, 26, 27, 28,
        31, 32, 33, 34, 35, 36, 37, 38,
        41, 42, 43, 44, 45, 46, 47, 48,
        51, 52, 53, 54, 55, 56, 57, 58,
        61, 62, 63, 64, 65, 66, 67, 68,
        71, 72, 73, 74, 75, 76, 77, 78,
        81, 82, 83, 84, 85, 86, 87, 88,
        91, 92, 93, 94, 95, 96, 97, 98
    };

    public ChessPosition()
    {
        SetStartPositions();
    }

    public ChessPosition(string fen)
    {
        if (String.IsNullOrWhiteSpace(fen))        
            throw new ArgumentException("FEN string cannot be empty.", nameof(fen));
        
        LoadFenPosition(fen);
    }

    public void SetStartPositions()
    {
        LoadFenPosition(STARTING_FEN);
    }

    public void ClearBoard()
    {
        _pieces.Clear();
    }

    public void LoadFenPosition(string fen)
    {
        
        ClearBoard();

        var pieceTypeFromChar = new Dictionary<char, PieceType>()
        {
            ['p'] = PieceType.Pawn,
            ['n'] = PieceType.Knight,
            ['k'] = PieceType.King,
            ['b'] = PieceType.Bishop,
            ['r'] = PieceType.Rook,
            ['q'] = PieceType.Queen
        };

        string fenBoard = fen.Split(' ')[0];
        int file = 0;
        int rank = 7;

        foreach (char symbol in fenBoard)
        {
            if (symbol == '/')
            {
                file = 0;
                rank--;
            }
            else
            {
                if (char.IsDigit(symbol))
                {
                    file += (int)char.GetNumericValue(symbol);
                }
                else
                {
                    PieceColor pieceColor = (char.IsUpper(symbol)) ? PieceColor.White : PieceColor.Black;
                    PieceType pieceType = pieceTypeFromChar[char.ToLower(symbol)];
                    int piecePos = (7 - rank) * 8 + file; // flip rank for A8 at top left - Visual
                    int topBoardIndex = piecePos;

                    var newPiece = new Piece(pieceType, pieceColor);
                    
                    if (_pieces.ContainsKey(topBoardIndex))
                        throw new ArgumentException($"FEN is invalid: two pieces occupy square {IndexToSquare(topBoardIndex)}.");

                    _pieces.Add(topBoardIndex, newPiece);

                    file++;
                }
            }
        }
    }

/*
    public void PrintBoard()
    {
        for (int i = 0; i < 64; i++)
        {
            //var boardIndex = topBoard[i];
            var pieceOnSquare = false;
            foreach (var piece in PieceList)
            {

                if (piece.PositionIndex == i)
                {
                    Console.Write((int)piece.Type + " ");
                    pieceOnSquare = true;
                }
            }
            if (!pieceOnSquare)
                Console.Write("- ");

            if ((i + 1) % 8 == 0)
            {
                Console.WriteLine();
            }
        }
    }
    */
    /*
        public void MovePiece(string from, string to)
        {
            int fromIndex = SquareToIndex(from);
            int toIndex = SquareToIndex(to);

            foreach (Piece p in PieceList)
            {
                if (mailbox64[fromIndex] == p.PositionIndex)
                    p.PositionIndex = mailbox64[toIndex];
            }
        }
    */

    /*
    public List<string> LegalMoves(string square)
    {
        // square = 'b1'
        List<string> legalMoves = new List<string>();
        List<string> validMoves = new List<string>();
        int squareIndex = SquareToIndex(square); // 57

        Piece? p = null;
        foreach (var piece in dPieceList)
        {
            if (squareIndex == piece.PositionIndex)
            {
                p = piece;
                continue;
            }
        }
    
        if (p?.Type == PieceType.Pawn)
        {
            //int[] moves = { 10, 20 };
            //Color c = p.pieceColor;


            // value of mailbox[] gives me the index of mailbox64[];
            //
            if (mailbox[squareIndex - 10] != -1)
            {
                legalMoves.Add(IndexToSquare(squareIndex - 10));
            }
            if (mailbox[squareIndex - 20] != -1)
            {
                legalMoves.Add(IndexToSquare(squareIndex - 20));
            }


        }
        
        if (p?.Type == PieceType.Knight)
        {
            if (underBoard[topBoard[squareIndex] - 21] != -1)
                legalMoves.Add(IndexToSquare(underBoard[topBoard[squareIndex] - 21]));
            if (underBoard[topBoard[squareIndex] - 19] != -1)
                legalMoves.Add(IndexToSquare(underBoard[topBoard[squareIndex] - 19]));
            if (underBoard[topBoard[squareIndex] - 12] != -1)
                legalMoves.Add(IndexToSquare(underBoard[topBoard[squareIndex] - 12]));
            if (underBoard[topBoard[squareIndex] - 8] != -1)
                legalMoves.Add(IndexToSquare(underBoard[topBoard[squareIndex] - 8]));
            if (underBoard[topBoard[squareIndex] + 21] != -1)
                legalMoves.Add(IndexToSquare(underBoard[topBoard[squareIndex] + 21]));
            if (underBoard[topBoard[squareIndex] + 19] != -1)
                legalMoves.Add(IndexToSquare(underBoard[topBoard[squareIndex] + 19]));
            if (underBoard[topBoard[squareIndex] + 12] != -1)
                legalMoves.Add(IndexToSquare(underBoard[topBoard[squareIndex] + 12]));
            if (underBoard[topBoard[squareIndex] + 8] != -1)
                legalMoves.Add(IndexToSquare(underBoard[topBoard[squareIndex] + 8]));


            foreach (var move in legalMoves)
            {
                bool blocked = false;
                foreach (var piece in PieceList)
                {
                    if (piece.Color == p.Color && piece.PositionIndex == SquareToIndex(move))
                    {
                        blocked = true;
                    }
                }

                if (!blocked)
                {
                    validMoves.Add(move);
                }
            }

        }

        //if (validMoves.Count == 0)
            


        return validMoves;

    }
*/

    public Piece? GetPieceAt(string square)
    {
        var index = SquareToIndex(square);

        return _pieces.TryGetValue(index, out var piece) ? piece : null;
        
    }

    public string IndexToSquare(int index)
    {
        // index = 57 => 'b2'
        string square = string.Empty;
        int posIndex = index;
        if (posIndex < 0 || posIndex > 63)
            throw new ArgumentException("invalid index");

        char file = (char)('a' + (posIndex % 8)); // 57 mod 8 = 1, 'a'(97) + 1 = 98 = 'b'
        char rank = (char)('8' - (posIndex / 8)); // 57 / 8 = 8, '8'(56)- 8 =

        square = $"{file}{rank}";

        return square;
    }

    public int SquareToIndex(string square)
    {
        // square = 'b1' => 57
        if (square.Length != 2)
            throw new ArgumentException("invalid square");

        char file = char.ToLower(square[0]);
        char rank = square[1];

        if (file < 'a' || file > 'h' || rank < '1' || rank > '8')
            throw new ArgumentException("Invalide chess square");

        int fileIndex = file - 'a';  // b , 98 - 97 = 1

        int rankIndex = 7 - (rank - '1'); // 7 - (49 - 49) 0 = 7

        int newIndex = (rankIndex * 8) + fileIndex; //57

        return newIndex;
    }

}