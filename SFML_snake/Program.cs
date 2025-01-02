using SFML.Graphics;
using SFML.System;
using SFML.Window;
using SFML_snake.Snake;
using System.Xml.Linq;
using System;

namespace SFML_snake
{
    internal class Program
    {
        internal enum Directions
        {
            Right,
            Left,
            Up,
            Down
        }

        static (uint, uint) wndSize = (800, 600);
        static string wndTitle = "Snake";

        static uint scores = default;
        static uint maxScores = default;
        static uint lastMaxScores = default;
        static bool isLose = false;
        static Directions direction = default;

        static RenderWindow rw = new RenderWindow(new VideoMode(wndSize.Item1, wndSize.Item2), "wndTitle");
        static SnakeHead snakeHead = new SnakeHead(playerX, playerY);
        static Food food = new Food(foodX, foodY);
        static SnakeForm snakeForm = new SnakeForm();

        static int playerX = 140;
        static int playerY = 40;

        static int playerSpeed = 300;

        static int foodX = default;
        static int foodY = default;

        static Clock clock = new Clock();
        static float moveTime = 0.5f; // время задержки в секундах


        static void Main(string[] args)
        {            
            rw.Closed += (s, e) => rw.Close();
            
            HashSet<float[]> coord = new HashSet<float[]>();
            int cellSize = 20;

            int gridWidth = (int)((wndSize.Item1 - 200)/ cellSize);
            int gridHeight = (int)(wndSize.Item2 / cellSize);

            for (int i = 0; i < gridHeight; i++)
            {
                for (int j = 0; j < gridWidth; j++)
                {
                    float centerX = (j * cellSize) + (cellSize / 2);
                    float centerY = (i * cellSize) + (cellSize / 2);
                    float[] coordinate = new float[] { centerX, centerY };
                    coord.Add(coordinate);
                }
            }

            snakeForm.InitializeSnake(playerX,playerY);
            bool isGameOver = false;
            while (rw.IsOpen)
            {
                rw.DispatchEvents();
                rw.Clear(new Color(200, 200, 200));

                //if (isGameOver)
                //{
                    SnakeMove();
                    food.FoodSpawn(rw, coord, snakeForm.snake);
                    food.CheckCollision(rw, coord, snakeForm.snake);
                    //CheckFoodCollision();
                    //CheckSelfCollision();
                //}

                DrawLine(rw);
                snakeForm.DrawSnake(rw);

                
                
                //snakeForm.SnakeMove();

                rw.Display();
            }
        }
        static void DrawLine(RenderWindow rw)
        {
            RectangleShape line = new RectangleShape();
            line.FillColor = Color.White;
            line.Position = new Vector2f(wndSize.Item1 - 200, 0);
            line.Size = new Vector2f(10, wndSize.Item2);
            rw.Draw(line);
        }
        static void SnakeMove()
        {
            int pase = 20;
            Vector2f previousHeadPosition = snakeForm.snake[0].shape.Position; // Сохранение позиции головы

            // Изменение направления в зависимости от нажатой клавиши
            if (Keyboard.IsKeyPressed(Keyboard.Key.W) && direction != Directions.Down)
            {
                direction = Directions.Up;
            }
            else if (Keyboard.IsKeyPressed(Keyboard.Key.S) && direction != Directions.Up)
            {
                direction = Directions.Down;
            }
            else if (Keyboard.IsKeyPressed(Keyboard.Key.A) && direction != Directions.Right)
            {
                direction = Directions.Left;
            }
            else if (Keyboard.IsKeyPressed(Keyboard.Key.D) && direction != Directions.Left)
            {
                direction = Directions.Right;
            }

            if (clock.ElapsedTime.AsSeconds() >= moveTime)
            {
                clock.Restart();

                // Обновление позиции головы в зависимости от направления
                switch (direction)
                {
                    case Directions.Up:
                        playerY -= pase;
                        break;
                    case Directions.Down:
                        playerY += pase;
                        break;
                    case Directions.Left:
                        playerX -= pase;
                        break;
                    case Directions.Right:
                        playerX += pase;
                        break;
                }

                // Обновление позиции головы
                snakeForm.snake[0].shape.Position = new Vector2f(playerX, playerY);

                // Обновление позиции тела змейки
                for (int i = 1; i < snakeForm.snake.Count; i++)
                {
                    Vector2f temp = snakeForm.snake[i].shape.Position; // Сохранение текущей позиции элемента
                    if (snakeForm.snake[i] is SnakeBody)
                    {
                        snakeForm.snake[i].shape.Position = new Vector2f(previousHeadPosition.X + 4, previousHeadPosition.Y + 4); // Перенос позиции на текущий элемент
                    }
                    else if (snakeForm.snake[i] is SnakeTail)
                    {
                        snakeForm.snake[i].shape.Position = new Vector2f(previousHeadPosition.X + 6, previousHeadPosition.Y + 6); // Перенос позиции на текущий элемент
                    }
                    else
                    {
                        snakeForm.snake[i].shape.Position = previousHeadPosition; // Перенос позиции предыдущей на текущий элемент
                    }
                    previousHeadPosition = temp; // Обновление для следующего элемента
                }
            }
        }
    }
}
