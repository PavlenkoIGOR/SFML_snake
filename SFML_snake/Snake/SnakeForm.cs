using SFML.Graphics;
using SFML.System;
using SFML.Window;
using static SFML_snake.Program;

namespace SFML_snake.Snake
{
    internal class SnakeForm
    {
        internal SnakeHead snakeHead;
        //internal SnakeBody snakeBody;
        //internal SnakeTail snakeTail;
        internal List<Element>? snake;
        internal Directions? direction;
        internal Clock clock;
        internal float moveTime = 0.5f;
        internal int playerX = default;
        internal int playerY = default;
        //internal HashSet<float[]> snake { get; set; }
        public SnakeForm()
        {            
            //playerY = Y;
            //playerX = X;
            snake = new List<Element>();
            clock = new Clock();
        }

        internal void SnakeMove()
        {
            int pase = 20;
            Vector2f previousHeadPosition = snake[0].shape.Position; // Сохранение позиции головы

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
                snake[0].shape.Position = new Vector2f(playerX, playerY);

                // Обновление позиции тела змейки
                for (int i = 1; i < snake.Count; i++)
                {
                    Vector2f temp = snake[i].shape.Position; // Сохранение текущей позиции элемента
                    if (snake[i] is SnakeBody)
                    {
                        snake[i].shape.Position = new Vector2f(previousHeadPosition.X + 4, previousHeadPosition.Y + 4); // Перенос позиции на текущий элемент
                    }
                    else if (snake[i] is SnakeTail)
                    {
                        snake[i].shape.Position = new Vector2f(previousHeadPosition.X + 6, previousHeadPosition.Y + 6); // Перенос позиции на текущий элемент
                    }
                    else
                    {
                        snake[i].shape.Position = previousHeadPosition; // Перенос позиции предыдущей на текущий элемент
                    }
                    previousHeadPosition = temp; // Обновление для следующего элемента
                }
            }
        }
        internal void DrawSnake(RenderWindow window)
        {
            for (int i = 0; i < snake.Count; i++)
            {
                snake[i].DrawElement(window);
            }
        }
        internal void InitializeSnake(int playerX, int playerY)
        {
            snakeHead = new SnakeHead(playerX,playerY);
            snake?.Clear();
            snakeHead.shape.Position = new Vector2f(playerX, playerY);

            snake?.Add(snakeHead); // Добавляем голову
                                  //snake.Add(new SnakeBody((int)snakeHead.shape.Position.X - 18, (int)snakeHead.shape.Position.Y + 4)); // добавляем тело
                                  //snake.Add(new SnakeTail((int)snakeHead.shape.Position.X - 30, (int)snakeHead.shape.Position.Y + 10)); // Добавляем голову
            snake?.Add(new SnakeBody((int)snakeHead.shape.Position.X - 18, (int)snakeHead.shape.Position.Y + 4)); // добавляем тело
            snake?.Add(new SnakeTail((int)snakeHead.shape.Position.X - 30, (int)snakeHead.shape.Position.Y + 10)); // Добавляем голову
        }
    }
}
