using SFML.Graphics;
using SFML.System;

namespace SFML_snake.SnakeLogic
{
    internal class Snake
    {
        internal SnakeHead Head { get; private set; }
        private List<SnakeBody> bodyParts;
        private const int InitialLength = 3;
        internal int pase = 20;

        public Snake(float startX, float startY)
        {
            Head = new SnakeHead(startX, startY);
            bodyParts = new List<SnakeBody>();

            // Инициализация тела
            for (int i = 1; i < InitialLength; i++)
            {
                bodyParts.Add(new SnakeBody(startX - i * 20, startY));
            }
        }

        /// <summary>
        /// Отрисовка змейки
        /// </summary>
        /// <param name="rw"></param>
        public void Draw(RenderWindow rw)
        {
            Head.Draw(rw);
            foreach (var bodyPart in bodyParts)
            {
                bodyPart.Draw(rw);
            }
        }

        public void Move(RenderWindow rw)
        {
            Vector2f oldHeadPosition = Head.shape.Position;

            if (Head.Direction == Direction.Left) { Head.shape.Position += new Vector2f(-pase, 0); }
            if (Head.Direction == Direction.Right) { Head.shape.Position += new Vector2f(pase, 0); }
            if (Head.Direction == Direction.Up) { Head.shape.Position += new Vector2f(0, -pase); }
            if (Head.Direction == Direction.Down) { Head.shape.Position += new Vector2f(0, pase); }

            // Проверка выхода за границы и телепортация
            int windowWidth = (int)rw.Size.X - 200;
            int windowHeight = (int)rw.Size.Y;

            if (Head.shape.Position.X < 0) // Если выходит за левую границу
            {
                Head.shape.Position = new Vector2f(windowWidth - Head.shape.Radius, Head.shape.Position.Y);
            }
            else if (Head.shape.Position.X >= windowWidth) // Если выходит за правую границу
            {
                Head.shape.Position = new Vector2f(0 + Head.shape.Radius, Head.shape.Position.Y);
            }

            if (Head.shape.Position.Y < 0) // Если выходит за верхнюю границу
            {
                Head.shape.Position = new Vector2f(Head.shape.Position.X, windowHeight - Head.shape.Radius);
            }
            else if (Head.shape.Position.Y >= windowHeight) // Если выходит за нижнюю границу
            {
                Head.shape.Position = new Vector2f(Head.shape.Position.X, 0 + Head.shape.Radius);
            }

            if (pase == 0)
            {
                return;
            }
            else
            {
                // Перемещение тела
                for (int i = bodyParts.Count - 1; i >= 0; i--)
                {
                    Vector2f tempPosition = i == 0 ? oldHeadPosition : bodyParts[i - 1].shape.Position;
                    bodyParts[i].shape.Position = tempPosition;
                }
            }
        }

        public void ChangeDirection(Direction newDirection)
        {
            // Проверка на невозможность развернуться на 180 градусов
            if ((Head.Direction == Direction.Up && newDirection == Direction.Down) ||
                (Head.Direction == Direction.Down && newDirection == Direction.Up) ||
                (Head.Direction == Direction.Left && newDirection == Direction.Right) ||
                (Head.Direction == Direction.Right && newDirection == Direction.Left))
            {
                return;
            }
            Head.Direction = newDirection;
        }

        /// <summary>
        /// Проверка на самопересечение
        /// </summary>
        /// <returns></returns>
        public bool CheckCollision()
        {
            for (int i = 0; i < bodyParts.Count; i++)
            {
                if (Head.shape.Position == bodyParts[i].shape.Position)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Добавление сегмента к змее
        /// </summary>
        public void Grow()
        {
            bodyParts.Add(new SnakeBody(bodyParts.Last().shape.Position.X, bodyParts.Last().shape.Position.Y));
        }

        public bool CheckBoundaryCollision(RenderWindow rw)
        {
            int windowWidth = (int)rw.Size.X - 200;
            int windowHeight = (int)rw.Size.Y;

            Vector2f headPosition = Head.shape.Position;

            if (headPosition.X - Head.shape.Radius < 0 || headPosition.X >= windowWidth ||
                headPosition.Y - Head.shape.Radius < 0 || headPosition.Y >= windowHeight)
            {
                return true;
            }

            return false;
        }
        public List<Vector2f> GetBodyPositions()
        {
            List<Vector2f> bodyPositions = new List<Vector2f>();
            foreach (var bodyPart in bodyParts)
            {
                bodyPositions.Add(bodyPart.shape.Position);
            }
            return bodyPositions;
        }
    }
}


/*// если нужно условие для проверки столкновения с рамкой
 *         public bool CheckBoundaryCollision(RenderWindow rw)
        {
            // Получаем размеры окна
            int windowWidth = (int)rw.Size.X-200;
            int windowHeight = (int)rw.Size.Y;

            // Получаем позицию головы змейки
            Vector2f headPosition = Head.shape.Position;

            // Проверяем, находится ли голова за пределами границ окна
            if (headPosition.X - Head.shape.Radius < 0 || headPosition.X >= windowWidth ||
                headPosition.Y - Head.shape.Radius < 0 || headPosition.Y >= windowHeight)
            {
                return true; // Пересечение с границами произошло
            }

            return false; // Нет пересечения
        }
*/