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
        AddPieces(tetromino.PiecesCoords, tetromino.Shape, TetrominoState.Moving);

    public void AddLockedPieces(Tetromino tetromino) =>
        AddPieces(tetromino.PiecesCoords, tetromino.Shape, TetrominoState.Locked);

    public void AddPieces(Coord[] coords, TetrominoShape shape, TetrominoState state)
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

    public bool AreCoordsFree(Coord[] coords) =>
        coords.All(IsCoordFree);

    // TOOD: Consider coord free if it's above the playfield.
    public bool IsCoordFree(Coord coord) =>
        coord.X >= 0 && coord.X < PlayfieldSize.Width
        && coord.Y >= 0 && coord.Y < PlayfieldSize.Height
        && Pieces[coord.X, coord.Y]?.State != TetrominoState.Locked;
}
