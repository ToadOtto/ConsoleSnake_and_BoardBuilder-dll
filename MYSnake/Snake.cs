using System;
using System.Collections.Generic;
using System.Threading;

namespace MYSnake
{
    internal class Snake
    {
        int score;

        int highscore;

        int tries;
        //Each array in the "snake" List should be seen as a part of the snake's body with two indexes - [0] (x) and [1] (y)
        List<int[]> snake = new List<int[]>();

        int[] candy = new int[2];
        private enum PossibleDirections
        {
            DOWN = 0,
            UP = 1,
            LEFT = 2,
            RIGHT = 3
        }
        int snake_direction = (int)PossibleDirections.DOWN;


        //CREATE NEW SNAKE-------------
        internal void BuildNewSnake()
        {
            //Delete previous snake
            snake.Clear();

            //Build a new snake which consists of 4 parts and set a starting coordinates for each part
            snake.Add(new int[] { SnakeBoard.SnakeBoardBorder_right - 13, SnakeBoard.SnakeBoardBorder_top + 5 });
            snake.Add(new int[] { SnakeBoard.SnakeBoardBorder_right - 13, SnakeBoard.SnakeBoardBorder_top + 4 });
            snake.Add(new int[] { SnakeBoard.SnakeBoardBorder_right - 13, SnakeBoard.SnakeBoardBorder_top + 3 });
            snake.Add(new int[] { SnakeBoard.SnakeBoardBorder_right - 13, SnakeBoard.SnakeBoardBorder_top + 2 });
            
            snake_direction = 0;
        }

        internal void DrawNewSnake()
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;

            foreach (int[] snakepart in snake)
            {
                Console.SetCursorPosition(snakepart[0], snakepart[1]);
                Console.Write("#");
            }

            Console.SetCursorPosition(0, 0);
        }

        //MAIN LOOP
        internal void Update()
        {
            DeleteTail();

            UpdateBodyPos();

            UpdateHeadPos();

            if (!CollidedWithBorder() && !CollidedWithSelf())
            {
                DrawHead();
            }

            if (CollidedWithApple())
            {
                BuildNewApple();
                DrawNewApple();
                snake.Add(new int[] { snake[snake.Count - 1][0], snake[snake.Count - 1][1]}); //Add 1 more part to the end of the snake
                score++;
                highscore = score > highscore ? score : highscore;
            }

            SnakeBoard.UpdateSnakeStats(score, highscore, tries);
        }

        //SNAKE LOGIC------------------
        private void DeleteTail()
        {
            //Delete the last part of the snake
            Console.SetCursorPosition(snake[snake.Count - 1][0], snake[snake.Count - 1][1]);
            Console.Write(" ");
            Console.SetCursorPosition(0, 0);
        }
        private void UpdateBodyPos()
        {
            for (int i = snake.Count - 1; i > 0; i--)
            {
                snake[i][0] = snake [i - 1][0];
                snake[i][1] = snake [i - 1][1];
            }
        }
        private void UpdateHeadPos()
        {
            switch (snake_direction)
            { 
            
                case (int)PossibleDirections.DOWN:
                    snake[0][1]++;
                    break;

                case (int)PossibleDirections.UP:
                    snake[0][1]--;
                    break;

                case ((int)PossibleDirections.LEFT):
                    snake[0][0]--;
                    break;

                case (int)PossibleDirections.RIGHT:
                    snake[0][0]++;
                    break;
            }
        }
        private void DrawHead()
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.SetCursorPosition(snake[0][0], snake[0][1]);
            Console.Write("#");
            Console.SetCursorPosition(0, 0);
        }

        //CREATE NEW APPLE-------------
        internal void BuildNewApple()
        {
            Array.Clear(candy, 0, 2);
            Random rng = new Random();

            bool possible_position = false;

            while (!possible_position)
            {
                candy[0] = rng.Next(SnakeBoard.SnakeBoardBorder_left + 1, SnakeBoard.SnakeBoardBorder_right - 1);
                candy[1] = rng.Next(SnakeBoard.SnakeBoardBorder_top + 1,  SnakeBoard.SnakeBoardBorder_bottom - 1);

                //Check every part of the snake, possible_position will remain true if no overlap of candy and the parts appears.
                for (int i = snake.Count - 1; i >= 0; i--)
                {
                    possible_position = true;
                    if (snake[i][0] == candy[0] && snake[i][1] == candy[1])
                    {
                        possible_position = false;
                        break;
                    }
                }
            }
        }
        internal void DrawNewApple()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.SetCursorPosition(candy[0], candy[1]);
            Console.Write("■");
        }

        //COLLIDERS--------------------
        private bool CollidedWithApple()
        {
            if(snake[0][0] == candy[0] && snake[0][1] == candy[1])
            {
                return true;
            }

            return false;
        }
        internal bool CollidedWithBorder()
        {
            return (snake[0][0] >=  SnakeBoard.SnakeBoardBorder_right  || snake[0][0] <=  SnakeBoard.SnakeBoardBorder_left ||
                    snake[0][1] >=  SnakeBoard.SnakeBoardBorder_bottom || snake[0][1] <=  SnakeBoard.SnakeBoardBorder_top);
        }
        internal bool CollidedWithSelf()
        {
            for (int i = snake.Count - 1; i > 0; i--)
            {
                if (snake[0][0] == snake[i][0] && snake[0][1] == snake[i][1])
                {
                    return true;
                }
            }
            return false;
        }

        //INPUT CHECK------------------
        internal void DirectionSwitch()
        {
            ConsoleKeyInfo input;
            while (true)
            {
                input = Console.ReadKey(true);
                switch (input.Key)
                {
                    case ConsoleKey.DownArrow:
                        if (snake_direction != (int)PossibleDirections.UP)
                        {
                            snake_direction = (int)PossibleDirections.DOWN;
                        }
                        break;

                    case ConsoleKey.UpArrow:
                        if (snake_direction != (int)PossibleDirections.DOWN)
                        {
                            snake_direction = (int)PossibleDirections.UP;
                        }
                        break;

                    case ConsoleKey.LeftArrow:
                        if (snake_direction != (int)PossibleDirections.RIGHT)
                        {
                            snake_direction = (int)PossibleDirections.LEFT;
                        }
                        break;

                    case ConsoleKey.RightArrow:
                        if (snake_direction != (int)PossibleDirections.LEFT)
                        {
                            snake_direction = (int)PossibleDirections.RIGHT;
                        }
                        break;
                }
            }
        }

        //MOVING HORIZONTALLY CHECK
        internal bool SnakeMovingHorizontally()
        {
            return snake_direction == (int)PossibleDirections.LEFT || snake_direction == (int)PossibleDirections.RIGHT;
        }

        //ONDEATH----------------------
        internal void OnDeath()
        {
            DeathAnimation();
            score = 0;
            tries++;
        }
        private void DeathAnimation()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            for (int i = 0; i < 2; i++)
            {
                Console.SetCursorPosition( SnakeBoard.SnakeBoardBorder_right - 20,  SnakeBoard.SnakeBoardBorder_top + 5);
                Console.Write("                    ");
                Console.SetCursorPosition( SnakeBoard.SnakeBoardBorder_right - 20,  SnakeBoard.SnakeBoardBorder_top + 5);
                Console.Write("YOU ARE DEAD");
                Thread.Sleep(400);
                Console.SetCursorPosition( SnakeBoard.SnakeBoardBorder_right - 20,  SnakeBoard.SnakeBoardBorder_top + 5);
                Console.Write("                    ");
                Console.SetCursorPosition( SnakeBoard.SnakeBoardBorder_right - 18,  SnakeBoard.SnakeBoardBorder_top + 5);
                Console.Write("YOU  ARE  DEAD");
                Thread.Sleep(400);
            }
            Console.SetCursorPosition( SnakeBoard.SnakeBoardBorder_right - 18,  SnakeBoard.SnakeBoardBorder_top + 5);
            Console.Write("                  ");
        }
    }
}
