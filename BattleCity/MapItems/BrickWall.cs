using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using BattleCity.Enums;
using BattleCity.Interfaces;
using BattleCity.MapItems.Base;
using BattleCity.Shared;

namespace BattleCity.MapItems
{
    class BrickWall : BaseItem, IResponse
    {
        public BrickWall(Point position)
            : base(new Rectangle(position.X, position.Y, Constants.Size.WidthTile, Constants.Size.HeightTile))
        {
            // Установка картинки обекта
            SpriteImage = new Bitmap(; Properties.Resources.BrickWall;
        }

        public void Response(ShellObj shellObj)
        {
            shellObj.Detonation = true;

            Level.DictionaryObjGame[MapItemKey.Wall].Remove(this);

            var next = Level.DictionaryObjGame[MapItemKey.Wall]
                .FirstOrDefault(gameObj => gameObj.Rect.Location.X == Rect.X + 20 && gameObj.Rect.Location.Y == Rect.Y);

            if (next != default)
            {
                Level.DictionaryObjGame[MapItemKey.Wall].Remove(next);
            }
        }
    }
}
