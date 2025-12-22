namespace ConsoleTetris.GameComponents;

internal class Playfield
{
    /// <summary> The individual pieces of the tetrominos in the playfield. </summary>
    /// <remarks> Defined as [x, y] where bottom left is (0,0). </remarks>
    public TetrominoType?[,] Pieces { get; init; }

    public Playfield()
    {
        Pieces = new TetrominoType?[10, 20];
    }
}
