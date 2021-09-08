using System.Drawing;
using System.Resources;
using System.Windows.Forms;
using BattleCity.Shared;

namespace BattleCity.Game
{
    internal class GameOver
    {
        private static Label _gameOver;

        public GameOver(Control control)
        {

            _gameOver = new Label
            {
                Parent = control,
                Location = new Point(0, 0),
                Size = new Size(control.ClientRectangle.Width, control.ClientRectangle.Height),
                BackColor = Color.Black,
                Image = Image.FromFile(Constants.Path.Content + $"GameOver.png"),
                ImageAlign = ContentAlignment.MiddleCenter
            };
        }

        public void Update()
        {
            _gameOver.BringToFront();
        }

        public void Draw()
        {
            _gameOver.Refresh();
        }
    }
}
