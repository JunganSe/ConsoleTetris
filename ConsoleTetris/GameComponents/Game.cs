namespace ConsoleTetris.GameComponents;

internal class Game
{
    public Playfield Playfield { get; set; } = new();
    public int Level { get; set; }
    public int Score { get; set; }
}
