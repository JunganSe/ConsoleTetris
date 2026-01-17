namespace ConsoleTetris.GameComponents;

/// <summary> Holds the state of the game, but has no behavior. </summary>
internal class Game
{
    public Playfield Playfield { get; } = new();
    public Tetromino? ActiveTetromino { get; set; }
    public int Level { get; set; } = 1;
    public int Score { get; set; } = 0;
    public int ClearedLines { get; set; } = 0;
}
