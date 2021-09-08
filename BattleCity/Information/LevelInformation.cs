using System.Drawing;
using BattleCity.Shared;

namespace BattleCity.Information
{
    public class LevelInformation : GameInformation
    {
        private readonly Image _spriteImage2;
        private readonly bool _isTwoDigit;

        public LevelInformation(int currentLevel, Point position)
            : base(new Rectangle(position.X, position.Y, Constants.Size.WidthTile, Constants.Size.HeightTile))
        {
            if (currentLevel > 9)
            {
                _isTwoDigit = true;
                _spriteImage2 = GetImage(currentLevel / 10);
            }

            SpriteImage = GetImage(currentLevel % 10);
        }

        public override void Draw(Graphics g, Point offset)
        {
            if (_isTwoDigit)
            {
                g.TranslateTransform(SpriteRectangle.X + offset.X - Constants.Size.WidthTile, SpriteRectangle.Y + offset.Y);
                g.DrawImage(_spriteImage2, 0, 0, SpriteRectangle.Width, SpriteRectangle.Height);
                g.ResetTransform();
            }
            base.Draw(g, offset);
        }
    }
}
