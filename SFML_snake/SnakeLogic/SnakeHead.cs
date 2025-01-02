using SFML.Graphics;
using SFML.System;

namespace SFML_snake.SnakeLogic
{
    internal class SnakeHead : Element
    {
        internal Direction Direction { get; set; }

        public SnakeHead(float xx, float yy) : base(xx, yy)
        {
            shape.FillColor = Color.Green;
            Direction = Direction.Right; 
        }
    }
}
