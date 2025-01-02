using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace SFML_snake
{
    public class RestartBttn
    {        
        public RectangleShape _shape;
        public Text _text;
        private Font _font;
        private RenderWindow _window;
        private string _bttnText;
        private string menuTextPath = Path.Combine(Directory.GetCurrentDirectory(), "Resources", "HARRINGTON.TTF");
        private bool _justPressed = false;
        public Action _onClick;

        public RestartBttn(RenderWindow window, string bttnText, Vector2f x1y1, Vector2f x2y2)
        {
            _bttnText = bttnText;
            _font = new Font(File.ReadAllBytes(menuTextPath));

            _window = window;

            _text = new Text(bttnText, _font)
            {
                FillColor = Color.Black,
                Style = Text.Styles.Bold,
                CharacterSize = 24 // размер шрифта
            };
            SetTextPosition(x1y1, x2y2);

            // Создание кнопки
            _shape = new RectangleShape(x2y2)
            {
                Position = x1y1,
                FillColor = Color.Cyan
            };
        }
        public bool IsMouseOver(Vector2i mousePosition)
        {
            if (_shape.GetGlobalBounds().Contains(mousePosition.X, mousePosition.Y))
            {
                if (Mouse.IsButtonPressed(Mouse.Button.Left) && _justPressed == false)
                {
                    _justPressed = true;
                    Vector2i mousePos = Mouse.GetPosition(_window);
                    MouseButtonEventArgs args = new MouseButtonEventArgs(new MouseButtonEvent())
                    {
                        Button = Mouse.Button.Left,
                        X = mousePos.X,
                        Y = mousePos.Y
                    };                    
                }
            }
            return _shape.GetGlobalBounds().Contains(mousePosition.X, mousePosition.Y);
        }

        public void Update(Color hoverColor)
        {
            bool isMouseOver = IsMouseOver(Mouse.GetPosition(_window));
            _shape.FillColor = isMouseOver ? hoverColor : Color.Cyan;
            Console.WriteLine(  "dasdasdasda");
            // Проверка нажатия кнопки
            if (isMouseOver && Mouse.IsButtonPressed(Mouse.Button.Left) && !_justPressed)
            {
                _justPressed = true;
                _onClick?.Invoke();
            }
            else if (!Mouse.IsButtonPressed(Mouse.Button.Left))
            {
                _justPressed = false;
            }
        }
        private void SetTextPosition(Vector2f position, Vector2f size)
        {
            // Центровка текста относительно кнопки
            _text.Position = new Vector2f(
                position.X + (size.X - _text.GetGlobalBounds().Width) / 2,
                position.Y + (size.Y - _text.GetGlobalBounds().Height) / 2
            );
        }
        internal void DrawRestartBttn()
        {
            IsMouseOver(Mouse.GetPosition(_window));
            Update(Color.Red);
            _window.Draw(_shape);
            _window.Draw(_text);    
        }
    }
}

