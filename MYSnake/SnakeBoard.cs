using BoardBuilder;

namespace MYSnake
{
    internal class SnakeBoard
    {
        internal int SnakeBoard_Border_top { get; }
        internal int SnakeBoard_Border_bottom { get; }
        internal int SnakeBoard_Border_right { get; }
        internal int SnakeBoard_Border_left { get; }

        GameBoard gameboard = new GameBoard("SNAKE", 2, -4);
        public SnakeBoard()
        {
            //Defining borders of the Snake Board
            SnakeBoard_Border_top = gameboard.Border_top;

            SnakeBoard_Border_bottom = gameboard.Border_bottom;

            SnakeBoard_Border_right = gameboard.Border_right;

            SnakeBoard_Border_left = gameboard.Border_left;
        }
        internal void DrawnNewSnakeBoard()
        {
            gameboard.DrawBoard();
        }
        internal void ClearSnakeBoard()
        {
            gameboard.ClearBoard();
        }

        internal void UpdateStats(int score, int highscore, int tries)
        {
            gameboard.UpdateStats(score, highscore, tries);
        }
    }
}
