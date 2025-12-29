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
        foreach (var (x, y) in tetromino.GetPiecesCoords())
        {
            if (y >= PlayfieldSize.Height)
                continue;

            var piece = new TetrominoPiece { Shape = tetromino.Shape, State = state };
            Pieces[x, y] = piece;
        }
    }

    public void RemovePieces(Tetromino tetromino)
    {
        foreach (var (x, y) in tetromino.GetPiecesCoords())
        {
            if (y >= PlayfieldSize.Height)
                continue;

            Pieces[x, y] = null;
        }
    }

    public bool AreCoordsFree((int x, int y)[] coords) =>
        coords.All(coord => IsCoordFree(coord.x, coord.y));

    public bool IsCoordFree(int x, int y) =>
        x >= 0 && x < PlayfieldSize.Width
        && y >= 0 && y < PlayfieldSize.Height
        && Pieces[x, y]?.State != TetrominoState.Locked;
}
