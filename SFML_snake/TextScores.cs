using SFML.Graphics;

namespace SFML_snake
{
    internal class TextScores
    {
        RenderWindow _rw;
        internal Text text = new Text();
        internal string menuTextPath = Path.Combine(Directory.GetCurrentDirectory(), "Resources", "HARRINGTON.TTF");
        public TextScores(RenderWindow rw)
        {
            _rw = rw;
        }
        internal void ShowScore(int scores, int record)
        {
            text.FillColor = Color.Yellow;
            text.Font = new SFML.Graphics.Font(File.ReadAllBytes(menuTextPath)); //путь к нужному шрифту
            text.CharacterSize = 30;
            text.Origin = new SFML.System.Vector2f(text.GetGlobalBounds().Width / 2, text.GetGlobalBounds().Height / 2);
            text.Position = new SFML.System.Vector2f(_rw.Size.X - 120, _rw.Size.Y - 360);
            text.DisplayedString = $"Scores: {scores}\nRecord: {record}";
            //Console.WriteLine($"Scores: {scores}");
            _rw.Draw(text);
        }
    }
}
