using System;
using System.Threading;
using System.Threading.Tasks;

namespace MYSnake
{
    internal class Program
    {

        static void Main(string[] args)
        {
            Console.CursorVisible = false;

            //Change default speed dependent on the difficulty selected
            int defaultspeed = ChooseDifficulty();

            //Ignore it (Horizontal speed is different because of the columns and rows thing =o)
            double horizontalspeed = defaultspeed / 1.3; 
            Console.Clear();

            //Draw a board with a name of the game [SNAKE GAME]
            SnakeBoard gameboard = new SnakeBoard();
            gameboard.DrawnNewSnakeBoard();

            //Create another task for input check so it runs parallel with the Program
            Snake snake = new Snake();
            Task InputCheck = Task.Run(snake.DirectionSwitch);

            //Main Loop
            do
            {
                //Reset the game
                gameboard.ClearSnakeBoard();

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
                        Thread.Sleep((int)horizontalspeed);
                        continue;
                    }

                    Thread.Sleep(defaultspeed);

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
