using SFML.Graphics;
using System.Xml.Schema;

namespace SFML_snake.SnakeLogic
{
    internal class SnakeBody : Element
    {
        public SnakeBody(float xx, float yy) : base(xx, yy)
        {
            shape.FillColor = Color.Blue; // Цвет тела
        }
    }
}
