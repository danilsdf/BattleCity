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
            //using var resxSet = new ResXResourceSet(Constants.Path.ResxFile);

            _gameOver = new Label
            {
                Parent = control,
                Location = new Point(0, 0),
                Size = new Size(control.ClientRectangle.Width, control.ClientRectangle.Height),
                BackColor = Color.Black,
                //Image = (Bitmap) resxSet.GetObject("GameOver", true),
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
