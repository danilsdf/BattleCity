using System.Drawing;
using BattleCity.Enums;
using BattleCity.Game;
using BattleCity.MapItems.Base;
using BattleCity.Shared;

namespace BattleCity.MapItems.StaticItems
{
    public class ColorPoint : BaseItem
    {
        private int _timeToRemove;
        public ColorPoint(Point position, string color)
            : base(new Rectangle(position.X, position.Y, 0, 0))
        {
            SpriteImage = GetImage(color);

            SpriteRectangle.Size = SpriteImage.Size;

            CurrentLevel.DictionaryObjGame[MapItemKey.Other].Add(this);
            _timeToRemove = Constants.Timer.RemovePathTimeout;
        }

        public override void Update()
        {
            if (_timeToRemove == 0) CurrentLevel.DictionaryObjGame[MapItemKey.Other].Remove(this);
            else _timeToRemove--;
        }
    }
}