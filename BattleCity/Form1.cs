using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using BattleCity.Game;
using BattleCity.Shared;

namespace BattleCity
{
    public partial class Form1 : Form
    {
        Thread consoleThread;

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool AllocConsole();

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool FreeConsole();

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
                default:
                    if (e.KeyCode == Keys.C && e.Control)
                    {
                        //Height = 800;
                        //_startGame.ShowConsole();
                        AllocConsole();
                        consoleThread = new Thread(Console);
                        consoleThread.Start();
                    }
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

        public void Console()
        {
            System.Console.WriteLine("Commands:");
            System.Console.WriteLine("!e-speed {number} - to change enemy speed");
            System.Console.WriteLine("!p-speed {number} - to change player speed");
            System.Console.WriteLine("!algorithm {string} - to change algorithm");
            System.Console.WriteLine("!algorithm-step {number} - to change algorithm step speed");
            System.Console.WriteLine("!enemy {number} - to change count of enemies speed");
            System.Console.WriteLine("!hit-count {number} - to change count of hits to kill player");
            System.Console.WriteLine("!p-cooldown {number} - to change player cooldown");
            System.Console.WriteLine("!e-cooldown {number} - to change enemy cooldown");
            System.Console.Write("user > ");
            ParseCommand(System.Console.ReadLine());
            Console();
        }

        private void ParseCommand(string command)
        {
            if (command.StartsWith("!p-speed")) //todo
            {
                if (int.TryParse(command.Substring(8), out var result))
                    CurrentLevel.Player.Speed = result;
                else System.Console.WriteLine("Invalid value");
            }
            else if(command.StartsWith("!e-speed"))//todo
            {
                if (int.TryParse(command.Substring(8), out var result))
                {
                    CurrentLevel.ChangeEnemiesSpeed(result);
                    Constants.EnemySpeed = result;
                }
                else System.Console.WriteLine("Invalid value");
            }
            else if(command.StartsWith("!algorithm"))
            {
                CurrentLevel.ChangeAlgorithm(command.Substring(10));
            }
            else if(command.StartsWith("!algorithm-step"))
            {
                if (int.TryParse(command.Substring(15), out var result))
                    Constants.Speed = result;
                else System.Console.WriteLine("Invalid value");
            }
            else if (command.StartsWith("!enemy"))
            {
                if (int.TryParse(command.Substring(6), out var result))
                    CurrentLevel.ChangeEnemies(result);
                else System.Console.WriteLine("Invalid value");
            }
            else if (command.StartsWith("!hit-count"))
            {
                if (int.TryParse(command.Substring(10), out var result))
                {
                    Constants.HitCount = result;
                    CurrentLevel.AlgorithmInformation.ChangeTime(result);
                }
                else System.Console.WriteLine("Invalid value");

            }
            else if (command.StartsWith("!p-cooldown"))
            {
                if (int.TryParse(command.Substring(11), out var result))
                {
                    Constants.PlayerCoolDown = result;
                }
                else System.Console.WriteLine("Invalid value");

            }
            else if (command.StartsWith("!e-cooldown"))
            {
                if (int.TryParse(command.Substring(11), out var result))
                {
                    Constants.EnemyCoolDown = result;
                }
                else System.Console.WriteLine("Invalid value");

            }

            else System.Console.WriteLine("Unknown command");
        }
    }
}
