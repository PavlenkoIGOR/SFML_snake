using SFML.Graphics;
using SFML.System;
using System.Xml.Linq;
using System;
using SFML_snake.Snake;

namespace SFML_snake
{
    internal class Food : Element
    {
        Random _rnd;
        //RenderWindow _rw;
        internal Food(int xx, int yy) : base(xx, yy)
        {
            _rnd = new Random();
            //_rw = rw;
            shape.Position = new SFML.System.Vector2f(xx, yy);
            shape.Radius = 12.0f;
            shape.FillColor = Color.Red;
        }
        internal void FoodSpawn(/*float foodX, float foodY*/RenderWindow _rw, HashSet<float[]> coords, List<Element> snake)
        {
            int rndX, rndY;
            Vector2f foodPosition;
            bool validPosition;
            do
            {
                rndX = _rnd.Next(0 + (int)shape.Radius / 2, (int)(_rw.Size.X - shape.Radius / 2));
                rndY = _rnd.Next(0 + (int)shape.Radius / 2, (int)(_rw.Size.Y - shape.Radius / 2));
                foodPosition = new Vector2f(rndX, rndY);
                validPosition = !snake.Any(segment => segment.shape.Position == foodPosition);
            } while (!validPosition);

            shape.Position = new SFML.System.Vector2f(rndX, rndY);
            _rw.Draw(shape);
        }
        internal void CheckCollision(RenderWindow _rw, HashSet<float[]> coords, List<Element>snake)
        {
            if (((SnakeHead)snake[0]).shape.GetGlobalBounds().Intersects(shape.GetGlobalBounds()))
            {
                FoodSpawn(_rw, coords, snake);
                Console.WriteLine("collide");
            }
        }
    }
}
