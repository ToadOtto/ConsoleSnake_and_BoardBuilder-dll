using System;
using System.Threading;
using System.Threading.Tasks;
using BoardBuilder;

namespace MYSnake
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.CursorVisible = false;

            //Change updateTime dependent on the difficulty selected
            int updateTime = ChooseDifficulty();

            //Horizontal updateTime is different because of the rows draws faster
            double horizontalUpdateTime = updateTime / 1.5;
            Console.Clear();

            //Create a board with a name of the game [SNAKE GAME]
            GameBoard board = new GameBoard("SNAKE GAME", -2, 4);

            SnakeBoard.SetBoardTemplate(board);

            SnakeBoard.DrawnNewSnakeBoard();

            //Create another task for input check so it runs parallel with the Program
            Snake snake = new Snake();
            Task InputCheck = Task.Run(snake.DirectionSwitch);

            //Main Loop
            do
            {
                //Reset the game
                SnakeBoard.ClearSnakeBoard();

                snake.BuildNewSnake();
                snake.DrawNewSnake();

                snake.BuildNewApple();
                snake.DrawNewApple();

                Thread.Sleep(800);

                //Snake updates every [speed] seconds. Loop ends after Colliding and execute OnDeath function
                do
                {
                    snake.Update();

                    if (snake.SnakeMovingHorizontally())
                    {
                        Thread.Sleep((int)horizontalUpdateTime);
                        continue;
                    }

                    Thread.Sleep(updateTime);

                } while (!snake.CollidedWithBorder() && !snake.CollidedWithSelf());

                snake.OnDeath();

            } while (true);
        }
        private static int ChooseDifficulty()
        {
            Console.WriteLine("Choose Difficulty: \n*1 - Easy\n*2 - Medium\n*3 - Hard");
            switch (Console.ReadKey(true).KeyChar)
            {
                case '1':
                    Console.WriteLine("Chosen easy Difficulty");
                    Thread.Sleep(400);
                    return 300;
                case '2':
                    Console.WriteLine("Chosen medium Difficulty");
                    Thread.Sleep(400);
                    return 200;
                case '3':
                    Console.WriteLine("Chosen hard Difficulty");
                    Thread.Sleep(400);
                    return 150;

                default:
                    Console.WriteLine("Chosen medium Difficulty");
                    Thread.Sleep(400);
                    return 200;
            }
        }
    }
}
