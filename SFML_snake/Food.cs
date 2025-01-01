using SFML.Graphics;

namespace SFML_snake
{
    internal class Food : Element
    {
        Random _rnd;
        //RenderWindow _rw;
        internal Food(int xx, int yy) : base(xx,yy)
        {
            _rnd = new Random();
            //_rw = rw;
            shape.Position = new SFML.System.Vector2f(xx,yy);
            shape.Radius = 12.0f;
            shape.FillColor = Color.Red;
        }
        internal void FoodSpawn(/*float foodX, float foodY*/RenderWindow _rw, HashSet<float[]>coords)
        {
            int rndX = _rnd.Next(0+ (int)shape.Radius/2, (int)(_rw.Size.X - shape.Radius / 2));
            int rndY = _rnd.Next(0 + (int)shape.Radius / 2, (int)(_rw.Size.Y - shape.Radius / 2));

            shape.Position = new SFML.System.Vector2f(rndX, rndY);
            _rw.Draw(shape);
        }
    }
}
