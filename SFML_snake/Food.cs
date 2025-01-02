using SFML.Graphics;
using SFML.System;

namespace SFML_snake
{
    internal class Food
    {
        private Random random;

        internal List<Vector2f> coords;
        internal CircleShape FoodShape { get; private set; }
        
        public Food(List<Vector2f> coords)
        {
            this.coords = coords;
            random = new Random();
            FoodShape = new CircleShape();
            FoodShape.Radius = 10.0f;
            FoodShape.FillColor = Color.Red;
            Random rnd = new Random();
            FoodShape.Position = coords[rnd.Next(coords.Count)];
        }
        public void Respawn(List<Vector2f> snakeBody, List<Vector2f> coords)
        {
            Vector2f newPosition;
            bool isInSnake;

            do
            {
                int x = random.Next(0, coords.Count);
                newPosition = coords[x];

                isInSnake = false;
                foreach (var bodyPart in snakeBody)
                {
                    if (bodyPart == newPosition)
                    {
                        isInSnake = true;
                        break;
                    }
                }
            } while (isInSnake);

            FoodShape.Position = newPosition;
        }
    }
}
