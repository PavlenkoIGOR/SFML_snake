using SFML.Graphics;
using SFML.System;
using SFML.Window;
using SFML_snake.SnakeLogic;
using System.Diagnostics;

namespace SFML_snake
{
    internal class Program
    {
        private static RenderWindow window;
        private static Snake snake;
        private static Food food;
        private static Clock clock;
        private static float elapsedTime;
        private static float speed = 400.0f;
        private static List<Vector2f> coord = new List<Vector2f>();
        private static TextScores textController ;
        private static RestartBttn restartBttn;
        private static int scores = 0;
        private static int record = 0;

        private static void Main(string[] args)
        {
            // Инициализация окна
            window = new RenderWindow(new VideoMode(600, 400), "Snake Game");
            window.Closed += (sender, e) => window.Close();
            textController = new TextScores(window);

            // Инициализация змейки и еды
            snake = new Snake(110, 110);
            restartBttn = new RestartBttn(window, "Restart", new Vector2f(window.Size.X - 180, 320), new Vector2f(170, 40));
            restartBttn._onClick = RestartGame; // метод перезапуска на действие кнопки

            InitializeGrid();
            Random rnd = new Random();
            int ranfFoodCoord = rnd.Next(coord.Count+1);
            food = new Food(coord);
            clock = new Clock();
            
            // Основной цикл игры
            while (window.IsOpen)
            {
                ProcessEvents();
                Update();
                Draw();
            }
        }

        private static void ProcessEvents()
        {
            window.DispatchEvents();
            if (Keyboard.IsKeyPressed(Keyboard.Key.W)) snake.ChangeDirection(Direction.Up);
            if (Keyboard.IsKeyPressed(Keyboard.Key.S)) snake.ChangeDirection(Direction.Down);
            if (Keyboard.IsKeyPressed(Keyboard.Key.A)) snake.ChangeDirection(Direction.Left);
            if (Keyboard.IsKeyPressed(Keyboard.Key.D)) snake.ChangeDirection(Direction.Right);
        }

        private static void Update()
        {
            if (clock.ElapsedTime.AsMilliseconds() > speed) // Скорость игры
            {
                snake.Move(window);

                if (snake.Head.shape.GetGlobalBounds().Intersects(food.FoodShape.GetGlobalBounds()))
                {
                    snake.Grow();
                    food.Respawn(snake.GetBodyPositions(), coord);
                    speed -= 20;
                    scores++;
                }

                if (snake.CheckCollision())
                {
                    snake.pase = 0;
                    
                    if (scores > record)
                    {
                        record = scores;
                    }
                }

                clock.Restart();
            }

            // проверяем нажатие кнопки на каждом обновлении
            restartBttn.Update(Color.Red);
        }

        private static void Draw()
        {
            window.Clear(SFML.Graphics.Color.Black);
            snake.Draw(window);
            window.Draw(food.FoodShape);
            textController.ShowScore(scores, record);
            window.Draw(restartBttn._shape);
            DrawLine();
            restartBttn.DrawRestartBttn();
            window.Display();
        }
        private static void DrawLine()
        {
            RectangleShape line = new RectangleShape();
            line.Position = new Vector2f(400, 0);
            line.Size = new Vector2f(10, window.Size.Y);
            line.FillColor = Color.White;
            window.Draw(line);
        }


        private static void InitializeGrid()
        {
            for (int x = 0; x < 400; x += 20)
            {
                for (int y = 0; y < 400; y += 20)
                {
                    coord.Add(new Vector2f(x, y));
                }
            }
        }

        private static void RestartGame()
        {
            scores = 0;
            speed = 400.0f; 
            snake = new Snake(110, 110); 
            food = new Food(coord);
            clock.Restart(); 
            Console.WriteLine("Restart");
        }
    }
}



