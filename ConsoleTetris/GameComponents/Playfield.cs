namespace ConsoleTetris.GameComponents;

internal class Playfield
{
    /// <summary> The individual pieces of the tetrominos in the playfield. </summary>
    /// <remarks> Defined as [x, y] where (0,0) is bottom left. </remarks>
    public TetrominoPiece?[,] Pieces { get; init; }

    public Playfield()
    {
        Pieces = new TetrominoPiece?[PlayfieldSize.Width, PlayfieldSize.Height];
    }

    public void AddMovingPieces(Tetromino tetromino) =>
        AddPieces(tetromino.PiecesCoords, tetromino.Shape, TetrominoPieceState.Moving);

    public void AddLockedPieces(Tetromino tetromino) =>
        AddPieces(tetromino.PiecesCoords, tetromino.Shape, TetrominoPieceState.Locked);

    public void AddPieces(Coord[] coords, TetrominoShape shape, TetrominoPieceState state)
    {
        foreach (var coord in coords)
        {
            if (coord.Y >= PlayfieldSize.Height)
                continue;

            var piece = new TetrominoPiece { Shape = shape, State = state };
            Pieces[coord.X, coord.Y] = piece;
        }
    }

    public void RemovePieces(Coord[] coords)
    {
        foreach (var coord in coords)
        {
            if (coord.Y >= PlayfieldSize.Height)
                continue;

            Pieces[coord.X, coord.Y] = null;
        }
    }

    public void MoveLinesDown(int yStart)
    {
        yStart = Math.Max(1, yStart); // Bottom line can't be moved down.
        for (int y = yStart; y < PlayfieldSize.Height; y++)
        {
            for (int x = 0; x < PlayfieldSize.Width; x++)
            {
                if (IsPieceMovable(x, y) && IsPieceMovable(x, y - 1))
                {
                    Pieces[x, y - 1] = Pieces[x, y];
                    Pieces[x, y] = null;
                }
            }
        }

        bool IsPieceMovable(int x, int y) =>
            Pieces[x, y]?.State is null or TetrominoPieceState.Locked;
    }

    public void ClearLine(int y)
    {
        for (int x = 0; x < PlayfieldSize.Width; x++)
        {
            Pieces[x, y] = null;
        }
    }

    public bool AreCoordsFree(Coord[] coords) =>
        coords.All(IsCoordFree);

    public bool IsCoordFree(Coord coord)
    {
        return coord.X >= 0 && coord.X < PlayfieldSize.Width // Within X bounds.
            && ((coord.Y >= PlayfieldSize.Height) // Above playfield is ok.
                || (coord.Y >= 0 && Pieces[coord.X, coord.Y]?.State != TetrominoPieceState.Locked)); // In playfield and no locked piece at coordinate.
    }

    public bool IsLineComplete(int y)
    {
        for (int x = 0; x < PlayfieldSize.Width; x++)
        {
            if (Pieces[x, y]?.State != TetrominoPieceState.Locked)
                return false;
        }
        return true;
    }
}
