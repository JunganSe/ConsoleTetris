using ConsoleTetris.GameComponents;

namespace ConsoleTetris.Rendering;

internal class PlayfieldRenderer
{
    /// <summary> Draw the entire playfield. </summary>
    public void DrawPlayfield(Playfield playfield)
    {
        // TODO: Optimize to only redraw changed pieces. Maybe keep a buffer of the last drawn state?

        int playfieldWidth = playfield.Pieces.GetLength(0);
        int playfieldHeight = playfield.Pieces.GetLength(1);
        int cursorOffsetX = GuiPosition.PlayfieldX;
        int cursorOffsetY = GuiPosition.PlayfieldY + PlayfieldSize.Height - 1;

        for (int x = 0; x < playfieldWidth; x++)
        {
            for (int y = 0; y < playfieldHeight; y++)
            {
                int cursorX = cursorOffsetX + x * 2;
                int cursorY = cursorOffsetY - y;
                Console.SetCursorPosition(cursorX, cursorY);

                var piece = playfield.Pieces[x, y];
                if (piece is not null)
                {
                    Console.ForegroundColor = ColorMapper.GetTetrominoColor(piece.Shape);
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
