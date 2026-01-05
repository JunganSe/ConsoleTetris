using ConsoleTetris.GameComponents;

namespace ConsoleTetris.Rendering;

internal class GuiRenderer
{
    public void DrawGui()
    {
        // Gui characters: ═ ║ ╔ ╗ ╚ ╝ ╦ ╩ ╠ ╣ ╬
        Console.ForegroundColor = GuiColor.Border;
        Console.SetCursorPosition(0, 0);
        Console.Write(
            """
            ╔════════════════════╦════════╗
            ║                    ║ SCORE  ║
            ║                    ║        ║
            ║                    ╠════════╣
            ║                    ║ HOLD   ║
            ║                    ║        ║
            ║                    ║        ║
            ║                    ╠════════╣
            ║                    ║ NEXT   ║
            ║                    ║        ║
            ║                    ║        ║
            ║                    ╠════════╝
            ║                    ║
            ║                    ║
            ║                    ║
            ║                    ║
            ║                    ║
            ║                    ║
            ║                    ║
            ║                    ║
            ║                    ║
            ╚════════════════════╝
            """);
    }

    public void DrawNextTetromino(TetrominoShape shape)
    {
        var tetromino = new Tetromino()
        {
            Shape = shape,
            Direction = Direction.A,
            X = 1,
            Y = 1,
        };
        var piecesCoords = tetromino.PiecesCoords;
        Console.ForegroundColor = ColorMapper.GetTetrominoColor(shape);

        for (int x = 0; x < GuiPosition.NextWith; x++)
        {
            for (int y = 0; y < GuiPosition.NextHeight; y++)
            {
                int cursorX = GuiPosition.NextX + x * 2;
                int cursorY = GuiPosition.NextY - y;
                var texture = (piecesCoords.Any(coord => coord.X == x && coord.Y == y))
                    ? PieceTexture.Block
                    : PieceTexture.Empty;
                Console.SetCursorPosition(cursorX, cursorY);
                Console.Write(texture);
            }
        }
    }

    public void DrawFps(double fps)
    {
        Console.ForegroundColor = GuiColor.FpsText;
        Console.SetCursorPosition(GuiPosition.FpsX, GuiPosition.FpsY);
        Console.Write($"FPS:{fps:F0}"); // F0 formats as integer.
    }
}
