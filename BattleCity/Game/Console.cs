using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using BattleCity.Enums;
using BattleCity.Shared;

namespace BattleCity.Game
{
    internal class Console
    {
        private static Label _menuControl;
        private static TextBox _consoleInput;
        private readonly Label _marker;
        private int _position = 0;

        public Console(Control control)
        {
            _menuControl = new Label
            {
                Location = new Point(0, Constants.Size.WindowClientHeight),
                BackColor = Color.Black,
                Font = new Font("COURER", 20, FontStyle.Bold),
                ForeColor = Color.White,
                TextAlign = ContentAlignment.MiddleLeft,
                Size = new Size(200, 30),
                Text = "CONSOLE"
            };
        }

        public MenuState MenuState{ get; set; }

        public static Control MenuControl => _menuControl;

        public void Update()
        {
            if (_menuControl.Top > _menuControl.Parent.Height - _menuControl.Height - 30)
            {
                _position -= 4;
            }
            else if (Keyboard.Down)
            {
                if (MenuState < MenuState.Mute)
                {
                    MenuState++;
                    Thread.Sleep(200);
                }
            }
            else if (Keyboard.Up)
            {
                if (MenuState > MenuState.Game)
                {
                    MenuState--;
                    Thread.Sleep(200);
                }
            }

            _marker.Location = MenuState == MenuState.Game ? new Point(210, 765) : new Point(210, 795);
        }

        public void Draw()
        {
            _menuControl.Location = new Point(0, Constants.Size.WindowClientHeight);
        }
    }
}
