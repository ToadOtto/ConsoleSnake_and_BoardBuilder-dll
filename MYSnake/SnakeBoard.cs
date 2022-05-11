using System;
using BoardBuilder;

namespace MYSnake
{
    internal class SnakeBoard
    {
        static GameBoard _snakeBoard;
        internal static int SnakeBoardBorder_top { get; set; }
        internal static int SnakeBoardBorder_bottom { get; set; }
        internal static int SnakeBoardBorder_right { get; set; }
        internal static int SnakeBoardBorder_left { get; set; }
        internal static void SetBoardTemplate(GameBoard board)
        {
            _snakeBoard = board;

            //Defining borders of the Snake Board
            SnakeBoardBorder_top = _snakeBoard.Border_top;

            SnakeBoardBorder_bottom = _snakeBoard.Border_bottom;

            SnakeBoardBorder_right = _snakeBoard.Border_right;

            SnakeBoardBorder_left = _snakeBoard.Border_left;
        }
        internal static void DrawnNewSnakeBoard() => _snakeBoard.DrawBoard();
        internal static void ClearSnakeBoard() => _snakeBoard.ClearBoard();
        internal static void UpdateSnakeStats(int score, int highscore, int tries) => _snakeBoard.UpdateStats(score, highscore, tries);
    }
}
