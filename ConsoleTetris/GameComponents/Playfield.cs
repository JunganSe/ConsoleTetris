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

    public void AddPieces(Tetromino tetromino, TetrominoState state)
    {
        foreach (var (x, y) in tetromino.PiecesCoords)
        {
            if (y >= PlayfieldSize.Height)
                continue;

            var piece = new TetrominoPiece { Shape = tetromino.Shape, State = state };
            Pieces[x, y] = piece;
        }
    }

    public void RemovePieces(Tetromino tetromino)
    {
        foreach (var (x, y) in tetromino.PiecesCoords)
        {
            if (y >= PlayfieldSize.Height)
                continue;

            Pieces[x, y] = null;
        }
    }

    public bool AreCoordsFree(Coord[] coords) =>
        coords.All(IsCoordFree);

    public bool IsCoordFree(Coord coord) =>
        coord.X >= 0 && coord.X < PlayfieldSize.Width
        && coord.Y >= 0 && coord.Y < PlayfieldSize.Height
        && Pieces[coord.X, coord.Y]?.State != TetrominoState.Locked;
}
