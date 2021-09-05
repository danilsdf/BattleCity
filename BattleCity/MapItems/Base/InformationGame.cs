using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using BattleCity.Interfaces;

namespace BattleCity.MapItems.Base
{
    [Serializable]
    abstract class InformationGame : IDraw
    {
        protected Image SpriteImage;
        protected Rectangle SpriteRectangle;

        protected InformationGame(Rectangle rect)
        {
            SpriteRectangle = rect;
        }

        public virtual void Draw(Graphics g, Point offset)
        {
            g.TranslateTransform(SpriteRectangle.X + offset.X, SpriteRectangle.Y + offset.Y);
            g.DrawImage(SpriteImage, 0, 0, SpriteRectangle.Width, SpriteRectangle.Height);
            g.ResetTransform();
        }
    }
}
