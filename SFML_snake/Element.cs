using SFML.Graphics;
using SFML.System;

namespace SFML_snake
{
    internal enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }
    internal abstract class Element
    {
        internal Sprite sprite { get; set; }
        internal CircleShape shape { get; set; }
        protected Element(float xx, float yy)
        {
            shape = new CircleShape();
            shape.Radius = 10.0f;
            shape.Origin = new Vector2f(shape.Radius, shape.Radius);
            shape.Position = new Vector2f(xx, yy);
        }
        internal virtual void Draw(RenderWindow rw)
        {
            rw.Draw(shape);
        }
    }
}
