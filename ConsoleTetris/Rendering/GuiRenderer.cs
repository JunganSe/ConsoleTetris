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
            ║                    ║ LEVEL  ║
            ║                    ║        ║
            ║                    ╠════════╣
            ║                    ║ LINES  ║
            ║                    ║        ║
            ║                    ╠════════╣
            ║                    ║ SCORE  ║
            ║                    ║        ║
            ║                    ╠════════╣
            ║                    ║ NEXT   ║
            ║                    ║        ║
            ║                    ║        ║
            ║                    ╠════════╣
            ║                    ║ HOLD   ║
            ║                    ║        ║
            ║                    ║        ║
            ║                    ╠════════╝
            ║                    ║
            ║                    ║
            ║                    ║
            ╚════════════════════╝
            """);
    }

    public void DrawLevel(int level)
    {
        Console.ForegroundColor = GuiColor.Text;
        Console.SetCursorPosition(GuiPosition.LevelX, GuiPosition.LevelY);
        string levelText = level.ToString().PadRight(3);
        Console.Write(levelText);
    }

    public void DrawClearedLines(int linesCount)
    {
        Console.ForegroundColor = GuiColor.Text;
        Console.SetCursorPosition(GuiPosition.LinesX, GuiPosition.LinesY);
        string scoreText = linesCount.ToString().PadRight(6);
        Console.Write(scoreText);
    }

    public void DrawScore(int score)
    {
        Console.ForegroundColor = GuiColor.Text;
        Console.SetCursorPosition(GuiPosition.ScoreX, GuiPosition.ScoreY);
        string scoreText = score.ToString().PadRight(6);
        Console.Write(scoreText);
    }

    public void DrawNextTetromino(TetrominoShape shape) =>
        DrawTetromino(shape, GuiPosition.NextX, GuiPosition.NextY);

    public void DrawHeldTetromino(TetrominoShape? shape) =>
        DrawTetromino(shape, GuiPosition.HoldX, GuiPosition.HoldY);

    private void DrawTetromino(TetrominoShape? shape, int x, int y)
    {
        Console.ForegroundColor = (shape.HasValue)
            ? ColorMapper.GetTetrominoColor(shape.Value)
            : ConsoleColor.White;
        var relativeCoords = (shape.HasValue)
            ? Tetromino.GetRelativePiecesCoords(shape.Value, Direction.A)
            : [];

        for (int iX = 0; iX < GuiPosition.TetrominoWidth; iX++)
        {
            for (int iY = 0; iY < GuiPosition.TetrominoHeight; iY++)
            {
                int cursorX = x + iX * 2;
                int cursorY = y - iY;
                string texture = relativeCoords.Any(coord => coord.X == iX && coord.Y == iY)
                    ? PieceTexture.Block
                    : PieceTexture.Empty;
                Console.SetCursorPosition(cursorX, cursorY);
                Console.Write(texture);
            }
        }
    }

    public void DrawFps(double fps)
    {
        string fpsText = string.Format($"{fps:F0}").PadLeft(2); // F0 formats as integer.
        Console.ForegroundColor = GuiColor.FpsText;
        Console.SetCursorPosition(GuiPosition.FpsX, GuiPosition.FpsY);
        Console.Write($"FPS:{fpsText}");
    }
}
