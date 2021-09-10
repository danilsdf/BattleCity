using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using BattleCity.Enums;
using BattleCity.Shared;
using BattleCity.SoundPart;

namespace BattleCity.Game
{
    internal class StartMenu
    {
        private static Label _menuControl;
        private Label _player;
        public readonly Label _changeVolume;
        private readonly Label _marker;
        private int _position = 0;

        public StartMenu(Control control)
        {
            _menuControl = new Label
            {
                Parent = control,
                Location = new Point(0, 0),
                Image = Image.FromFile(Constants.Path.Content + $"Menu.png")
            };
            _menuControl.Size = _menuControl.Image.Size;

            _player = new Label
            {
                Parent = _menuControl,
                Location = new Point(265, 770),
                BackColor = Color.Transparent,
                Font = new Font("COURER", 20, FontStyle.Bold),
                ForeColor = Color.White,
                TextAlign = ContentAlignment.MiddleLeft,
                Size = new Size(200, 30),
                Text = "1 PLAYER"
            };

            _changeVolume = new Label
            {
                Parent = _menuControl,
                Location = new Point(265, 800),
                BackColor = Color.Transparent,
                Font = new Font("COURER", 20, FontStyle.Bold),
                ForeColor = Color.White,
                TextAlign = ContentAlignment.MiddleLeft,
                Size = new Size(200, 30),
                Text = "Turn off Music"
            };

            _marker = new Label {Parent = _menuControl, Image = Image.FromFile(Constants.Path.Content + $"Marker.png")};
            _marker.Size = _marker.Image.Size;
        }

        public MenuState MenuState{ get; set; }

        public static Control MenuControl => _menuControl;

        public void ResetPosition()
        {
            _position = 0;
        }

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
            _menuControl.Location = new Point(-40, _position);
        }
    }
}
