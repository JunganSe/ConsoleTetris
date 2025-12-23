using ConsoleTetris.GameComponents;

namespace ConsoleTetris.Rendering;

internal class PlayfieldRenderer
{
    public void DrawPlayfield(Playfield playfield)
    {
        int playfieldWidth = playfield.Pieces.GetLength(0);
        int playfieldHeight = playfield.Pieces.GetLength(1);
        int cursorOffsetX = GlobalConstants.Playfield.X * 2;
        int cursorOffsetY = GlobalConstants.Playfield.Y + GlobalConstants.Playfield.Height - 1;

        for (int x = 0; x < playfieldWidth; x++)
        {
            for (int y = 0; y < playfieldHeight; y++)
            {
                int cursorX = cursorOffsetX + x * 2;
                int cursorY = cursorOffsetY - y;
                Console.SetCursorPosition(cursorX, cursorY);

                var piece = playfield.Pieces[x, y];
                if (piece.HasValue)
                {
                    Console.ForegroundColor = ColorMapper.GetTetrominoColor(piece.Value);
                    Console.Write(PieceTexture.Block);
                }
                else
                {
                    Console.Write(PieceTexture.Empty);
                }
            }
        }
    }
}
