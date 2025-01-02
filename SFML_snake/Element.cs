using SFML.Graphics;
using SFML.System;

namespace SFML_snake
{
    internal abstract class Element
    {
        internal Sprite sprite { get; set; }
        internal CircleShape shape { get; set; }
        protected Element(int xx, int yy)
        {
            shape = new CircleShape();            
            shape.Origin = new Vector2f(shape.Radius, shape.Radius);
            shape.Position = new Vector2f(xx, yy);
        }
        internal virtual void DrawElement(RenderWindow rw)
        {
            shape.FillColor = Color.White;
            rw.Draw(shape);
        }
    }
}
