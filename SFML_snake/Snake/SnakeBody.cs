using SFML.Graphics;
using System.Xml.Schema;

namespace SFML_snake.Snake
{
    internal class SnakeBody : Element
    {
        public SnakeBody(int xx, int yy): base(xx, yy)
        {
            shape.Radius = 16.0f;
        }
        internal override void DrawElement(RenderWindow rw)
        {
            //shape.Position = new SFML.System.Vector2f(shape.Position.X, shape.Position.Y);
            shape.FillColor = Color.Black;
            rw.Draw(shape);
        }
    }
}
