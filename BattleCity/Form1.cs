using System.Drawing;
using System.Windows.Forms;
using BattleCity.Game;
using BattleCity.Shared;

namespace BattleCity
{
    public partial class Form1 : Form
    {
        private StartGame _startGame;
        public Form1()
        {
            InitializeComponent();

            ClientSize = new Size(Constants.Size.WindowClientWidth, Constants.Size.WindowClientHeight);

            _startGame = new StartGame(this);
            _startGame.Play();
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);

            switch (e.KeyCode)
            {
                case Keys.Left:
                    Keyboard.Left = true;
                    break;
                case Keys.Right:
                    Keyboard.Right = true;
                    break;
                case Keys.Up:
                    Keyboard.Up = true;
                    break;
                case Keys.Down:
                    Keyboard.Down = true;
                    break;
                case Keys.Space:
                    Keyboard.Space = true;
                    break;
                case Keys.Enter:
                    Keyboard.Enter = true;
                    break;
                case Keys.Escape:
                    Keyboard.Escape = true;
                    break;
                case Keys.Z:
                    CurrentLevel.ChangeAlgorithm();
                    break;
            }
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            base.OnKeyUp(e);

            switch (e.KeyCode)
            {
                case Keys.Left:
                    Keyboard.Left = false;
                    break;
                case Keys.Right:
                    Keyboard.Right = false;
                    break;
                case Keys.Up:
                    Keyboard.Up = false;
                    break;
                case Keys.Down:
                    Keyboard.Down = false;
                    break;
                case Keys.Space:
                    Keyboard.Space = false;
                    break;
                case Keys.Enter:
                    Keyboard.Enter = false;
                    break;
                case Keys.Escape:
                    Keyboard.Escape = false;
                    break;
            }
        }
    }
}
