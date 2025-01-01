using SFML.Graphics;
using SFML.System;

namespace SFML_snake.Snake
{
    internal class SnakeHead : Element
    {
        public SnakeHead(int xx, int yy) : base(xx,yy)
        {
            shape.Radius = 20.0f;
        }
        internal override void DrawElement(RenderWindow rw)
        {
            shape.FillColor = Color.Blue;
            rw.Draw(shape);
        }
    }
}
