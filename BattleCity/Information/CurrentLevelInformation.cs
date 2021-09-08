using System.Drawing;
using BattleCity.Game;
using BattleCity.Shared;
using BattleCity.SoundPart;

namespace BattleCity.Information
{
    public class CurrentLevelInformation : GameInformation
    {
        private bool _isShow;
        private Rectangle _bottom;
        private Rectangle _top;
        private int _countFrame;
        private readonly LevelInformation _levelInformation;
        
        public CurrentLevelInformation(int currentLevel)
            : base(new Rectangle(Constants.Size.WindowClientWidth / 2 - Constants.Size.WidthTile * 5,
                Constants.Size.WindowClientHeight / 2 - Constants.Size.HeightTile / 2,
                Constants.Size.WidthTile * 7,
                Constants.Size.HeightTile))
        {
            _countFrame = 40;
            _isShow = true;
            _bottom = new Rectangle(
                0,
                Constants.Size.WindowClientHeight,
                Constants.Size.WindowClientWidth,
                Constants.Size.WindowClientHeight / 2);
            _top = new Rectangle(
                0,
                -(Constants.Size.WindowClientHeight / 2),
                Constants.Size.WindowClientWidth,
                Constants.Size.WindowClientHeight / 2);

            SpriteImage = GetImage("Stage");

            _levelInformation = new LevelInformation(currentLevel,
                new Point(
                    Constants.Size.WindowClientWidth / 2 + Constants.Size.WidthTile * 2,
                    Constants.Size.WindowClientHeight / 2 - Constants.Size.HeightTile / 2));

            CurrentLevel.ListInformation.Add(this);
        }

        public void Update()
        {
            if (_top.Y < 0)
            {
                _top.Y += 5;
                _bottom.Y -= 5;
            }
            else
                _countFrame--;

            switch (_countFrame)
            {
                case 0:
                    _isShow = false;
                    CurrentLevel.ListInformation.Remove(this);
                    break;
                case 39:
                    SoundService.GameStart();
                    break;
            }
        }

        public override void Draw(Graphics g, Point offset)
        {
            var solidBrush = new SolidBrush(Color.FromArgb(99, 99, 99));
            g.FillRectangle(solidBrush, this._top);
            g.FillRectangle(solidBrush, this._bottom);
            if (_top.Y < 0) return;
            
            base.Draw(g, offset);
            _levelInformation.Draw(g, offset);
        }

        public bool IsShow()
        {
            return _isShow;
        }
    }
}
