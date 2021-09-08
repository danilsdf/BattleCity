using System.Drawing;
using BattleCity.Enums;
using BattleCity.Game;
using BattleCity.MapItems.Base;
using BattleCity.Shared;

namespace BattleCity.MapItems.StaticItems
{
    public class MapPoint : BaseItem
    {
        private int _timeToRemove;
        public MapPoint(Point position, int count)
            : base(new Rectangle(position.X, position.Y, 0, 0))
        {
            SpriteImage = GetImage(count);

            SpriteRectangle.Size = SpriteImage.Size;

            CurrentLevel.DictionaryObjGame[MapItemKey.Other].Add(this);
            _timeToRemove = Constants.Timer.RemovePointsTimeout;
        }

        public override void Update()
        {
            if (_timeToRemove == 0) CurrentLevel.DictionaryObjGame[MapItemKey.Other].Remove(this);
            else _timeToRemove--;
        }
    }
}
