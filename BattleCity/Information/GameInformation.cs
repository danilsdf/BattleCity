﻿using System;
using System.Drawing;
using System.Resources;
using BattleCity.Interfaces;
using BattleCity.Shared;

namespace BattleCity.Information
{
    [Serializable]
    public abstract class GameInformation : IDraw
    {
        protected Image SpriteImage;
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
