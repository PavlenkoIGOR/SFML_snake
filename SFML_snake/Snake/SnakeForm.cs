using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFML_snake.Snake
{
    internal class SnakeForm
    {
        internal SnakeHead snakeHead;
        internal HashSet<float[]> snake { get; set; }
        public SnakeForm()
        {

        }

        internal void SnakeMove(RenderWindow rw, float x, float y)
        {

            //rw.Draw();
        }
    }
}
