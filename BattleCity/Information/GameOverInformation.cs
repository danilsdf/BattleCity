using System.Drawing;
using BattleCity.Enums;
using BattleCity.Game;
using BattleCity.Shared;
using BattleCity.SoundPart;

namespace BattleCity.Information
{
    public class GameOverInformation : GameInformation
    {
        private bool _isShow;
        private int _intervalGameOver;
        public GameOverInformation()
            : base(new Rectangle(Constants.Size.HeightTile * 11, Constants.Size.WindowClientHeight, Constants.Size.WidthTile * 4, Constants.Size.HeightTile * 2))
        {
            _isShow = true;
            _intervalGameOver = Constants.Timer.GameOverTimeout;
            SpriteImage = GetImage("GameOver");
        }

        public void Update()
        {
            if (SpriteRectangle.Y > Constants.Size.WindowClientHeight / 2 - SpriteImage.Height / 2)
                SpriteRectangle.Y -= 5;

            _intervalGameOver--;

            switch (_intervalGameOver)
            {
                case 190:
                    SoundService.GameOver();
                    break;
                case 0:
                    _isShow = false;
                    StartGame.GameState = GameState.GameOver;
                    break;
            }
        }

        public bool IsShow()
        {
            return _isShow;
        }
    }
}