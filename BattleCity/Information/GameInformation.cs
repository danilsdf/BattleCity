using System;
using System.Drawing;
using BattleCity.Interfaces;
using BattleCity.Shared;

namespace BattleCity.Information
{
    [Serializable]
    public abstract class GameInformation : IDraw
    {
        protected Image SpriteImage;
        protected string SpriteText;
        protected Rectangle SpriteRectangle;

        protected GameInformation(Rectangle rect)
        {
            SpriteRectangle = rect;
        }

        public virtual void Draw(Graphics g, Point offset)
        {
            g.TranslateTransform(SpriteRectangle.X + offset.X, SpriteRectangle.Y + offset.Y);
            g.DrawImage(SpriteImage, 0, 0, SpriteRectangle.Width, SpriteRectangle.Height);
            g.ResetTransform();

            g.TranslateTransform(SpriteRectangle.X + offset.X - 10, SpriteRectangle.Y + offset.Y + 40);
            if (!string.IsNullOrWhiteSpace(SpriteText))
                g.DrawString(SpriteText, new Font("Arial", 14), new SolidBrush(Color.Black), 0, 0);
            g.ResetTransform();
        }

        public Image GetImage(string name)
        {
            return Image.FromFile(Constants.Path.Content + $"{name}.png");
        }

        public Image GetImage(int number)
        {
            var name = string.Empty;
            if (number < 10) name = "0";

            name += number;

            return GetImage(name);
        } 
    }
}
