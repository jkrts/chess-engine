# FEN Specification

## Overview

FEN is "Forsyth-Edwards Notation". It is essentially a way to represent a chess position using the standard ASCII character set. A FEN record is a single variable length string of text that has 6 parts seperated by a space (" ") character. A FEN text file should have the suffix ".fen" [2]

A FEN record uses 6 data fields to represent the position:
1. Piece Placement Data
2. Active Color
3. Castling Availability
4. En Passant Target Square
5. Halfmove Clock
6. Fullmove Number

## FEN Data Fields

### 1. Piece Placement Data
- Starts with 8th rank.
- Each rank squares are specified from file a to file h.
- White pieces are uppercase, black pieces are lowercase.
- Empty squares are represented by digits 1-8. The digit represents the count of contiguous empty squares
- '/' is used to separate data between ranks

- Example Piece Placement from Starting Position:
    - 'rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKNBR'

### 2. Active Color
- Represents active player
- lowercase 'w' for white, lowercase 'b' for black

### 3. Castling Availability
- Indicates potential future castling that may or may not be possible.
- No available castling for either side is represented by '-' otherwise:
    - 'K' white has kingside castling available
    - 'Q' white has queenside castling available
    - 'k' black has kingside castling available
    - 'q' has queenside castling available
- Uppercase will come before lowercase
- Kingside will come before queenside

### 4. En Passant Target Square
- '-' no en passant target square
- Target square represent by lowercase file character and a rank digit
- Example: 'e3'
- Rank will be 3 following a white pawn double advance, 6 if black pawn double advance
- Only given if the last move was a pawn advance of two squares. Doesnt matter if opponent can take advantage and capture or not.

### 5. Halfmove Clock
- Non negative integer
- Number of half moves since last pawn advance or capture.
- Used for fifty move draw rule.

### 6. Fullmove Number
- Positive integer
- Always 1 for first move of the game for both white and black.
- Incremented by one immediately after black move.

## Examples
Starting Position:
- >rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1

After the move 1. e4:
- >rnbqkbnr/pppppppp/8/8/4P3/8/PPPP1PPP/RNBQKBNR b KQkq e3 0 1

And then after 1. ... c5:
- >rnbqkbnr/pp1ppppp/8/2p5/4P3/8/PPPP1PPP/RNBQKBNR w KQkq c6 0 2

And then after 2. Nf3:
- >rnbqkbnr/pp1ppppp/8/2p5/4P3/5N2/PPPP1PPP/RNBQKB1R b KQkq - 1 2

For two kings on their home squares and a white pawn on e2 (white to move) with thirty eight full moves played with five halfmoves since the last pawn move or capture:
- >4k3/8/8/8/8/8/4P3/4K3 w - - 5 39


## References
[1] "Forsyth-Edwards Notation," ChessProgramming.org. Accessed: Jun. 20, 2026. [Online]. Available: https://web.archive.org/web/20260621030249/https://www.chessprogramming.org/Forsyth-Edwards_Notation

[2] Sections 16.1 in “The Complete PGN Standard,” Saremba.de. Accessed: Jun. 20, 2026. [Online]. Available: https://web.archive.org/web/20260616150010/https://www.saremba.de/chessgml/standards/pgn/pgn-complete.htm

[3] “Handling FEN Strings,” Rustic-Chess.org. Accessed: Jun. 20, 2026. [Online]. Available: 
https://web.archive.org/web/20260621032000/https://rustic-chess.org/board_functionality/handling_fen_strings.html

[4] “Forsyth-Edwards Notation,” Wikipedia. Accessed: Jun. 20, 2026. [Online]. Available: https://web.archive.org/web/20260621032259/https://en.wikipedia.org/wiki/Forsyth-Edwards_Notation