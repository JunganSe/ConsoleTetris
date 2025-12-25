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
}
