using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using BattleCity.Enums;
using BattleCity.Shared;
using BattleCity.SoundPart;
using Timer = System.Windows.Forms.Timer;


namespace BattleCity.Game
{
    internal class StartGame
    {
        private readonly CurrentLevel _currentLevel;
        public static GameState GameState;
        public static bool IsConsole;
        private static GameWindow _gameWindow;
        private StartMenu menu;
        private readonly GameOver _gameOver;
        private readonly Console _console;
        private readonly Timer _timer;

        public StartGame(Form form)
        {
            _timer = new Timer();
            form.ClientSize = new Size(Constants.Size.WindowClientWidth, Constants.Size.WindowClientHeight);
            form.StartPosition = FormStartPosition.CenterScreen;
            form.Text = @"Battle City";

            _currentLevel = new CurrentLevel();

            menu = new StartMenu(form);
            _gameOver = new GameOver(form);
            _gameWindow = new GameWindow(form);
            _console = new Console(form);

            GameState = GameState.Menu;
            StartMenu.MenuControl.BringToFront();
            _currentLevel.DownloadLevel(1);
        }

        public static Control GameWindow => _gameWindow;

        public void Play()
        {
            _timer.Interval = 20;
            _timer.Tick += Timer_Tick;
            _timer.Start();
        }

        public void ShowConsole()
        {
            _console.Draw();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            Update();
            Draw();
        }

        private void Draw()
        {
            switch (GameState)
            {
                case GameState.Menu:
                    menu.Draw();
                    break;
                case GameState.Game:
                    _currentLevel.Draw();
                    break;
                case GameState.GameOver:
                    _gameOver.Draw();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void Update()
        {
            CatchClickOnKey();

            switch (GameState)
            {
                case GameState.Menu:
                    menu.Update();
                    break;
                case GameState.Game:
                    _currentLevel.Update();
                    break;
                case GameState.GameOver:
                    _gameOver.Update();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void CatchClickOnKey()
        {
            if (Keyboard.Escape)
            {
                SoundService.Stop();
                GameState = GameState.Menu;
                StartMenu.MenuControl.BringToFront();
                Thread.Sleep(100);
            }
            else
            {
                if (Keyboard.Enter && GameState == GameState.GameOver)
                {
                    GameState = GameState.Menu;
                    StartMenu.MenuControl.BringToFront();
                    _currentLevel.DownloadLevel(1);
                    Thread.Sleep(100);
                }
                else if (Keyboard.Enter && GameState == GameState.Menu && menu.MenuState == MenuState.Game)
                {
                    var rnd = new Random();
                    Constants.HitCount = rnd.Next(1, 10);
                    CurrentLevel.StartTimer();
                    CurrentLevel.AlgorithmInformation.ChangeTime(Constants.HitCount);
                    GameState = GameState.Game;
                    _gameWindow.BringToFront();
                    menu.ResetPosition();
                }
                else if (Keyboard.Enter && menu.MenuState == MenuState.Mute)
                {
                    Keyboard.Enter = false;
                    SoundService.Mute();
                    menu._changeVolume.Text = SoundService.IsMuted ? "Turn on Music" : "Turn off Music";

                    GameState = GameState.Game;
                    _gameWindow.BringToFront();
                    menu.ResetPosition();
                }
            }
        }
    }
}
