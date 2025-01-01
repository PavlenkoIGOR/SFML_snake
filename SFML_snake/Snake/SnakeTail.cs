using SFML.Graphics;

namespace SFML_snake.Snake
{
    internal class SnakeTail : Element
    {
        public SnakeTail(int xx, int yy) : base(xx,yy)
        {
            shape.Radius = 10.0f;
        }

        internal override void DrawElement(RenderWindow rw)
        {
            shape.FillColor = Color.Yellow;
            rw.Draw(shape);
        }
    }
}
