using System.Drawing;
using System.Linq;
using BattleCity.Enums;
using BattleCity.Game;
using BattleCity.Interfaces;
using BattleCity.MapItems.Base;
using BattleCity.MapItems.ShellModule;
using BattleCity.Shared;

namespace BattleCity.MapItems.StaticItems
{
    public class RandomBrickWall : BaseItem, IResponse
    {
        private int _timeToRemove;

        public RandomBrickWall(Point position) : base(position)
        {
            SpriteImage = GetImage("BrickWall");

            SpriteRectangle.Size = SpriteImage.Size;

            CurrentLevel.DictionaryObjGame[MapItemKey.Wall].Add(this);
            _timeToRemove = Constants.Timer.RemovePointsTimeout;
        }

        public override void Update()
        {
            if (_timeToRemove == 0) CurrentLevel.DictionaryObjGame[MapItemKey.Other].Remove(this);
            else _timeToRemove--;
        }

        public void Response(Shell shell)
        {
            shell.Detonation = true;

            CurrentLevel.DictionaryObjGame[MapItemKey.Wall].Remove(this);

            var next = CurrentLevel.DictionaryObjGame[MapItemKey.Wall]
                .FirstOrDefault(gameObj => gameObj.Rect.Location.X == Rect.X + 20 && gameObj.Rect.Location.Y == Rect.Y);

            if (next != default)
            {
                CurrentLevel.DictionaryObjGame[MapItemKey.Wall].Remove(next);
            }
        }
    }
}
