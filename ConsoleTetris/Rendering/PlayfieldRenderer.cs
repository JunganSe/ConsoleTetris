using ConsoleTetris.GameComponents;

namespace ConsoleTetris.Rendering;

internal class PlayfieldRenderer
{
    public void DrawPlayfield(Playfield playfield)
    {
        for (int x = 0; x < playfield.Pieces.GetLength(0); x++)
        {
            for (int y = 0; y < playfield.Pieces.GetLength(1); y++)
            {
                int cx = GlobalConstants.PlayArea.X * 2 + x * 2;
                int cy = GlobalConstants.PlayArea.Y + (GlobalConstants.PlayArea.Height - 1 - y);
                Console.SetCursorPosition(cx, cy);

                var piece = playfield.Pieces[x, y];
                if (piece.HasValue)
                {
                    Console.ForegroundColor = ColorMapper.GetTetrominoColor(piece.Value);
                    Console.Write("██");
                }
                else
                {
                    Console.Write("  ");
                }
            }
        }
    }
}
