using System.Drawing;
using System.Linq;
using BattleCity.Enums;
using BattleCity.Game;
using BattleCity.Interfaces;
using BattleCity.MapItems.Base;
using BattleCity.MapItems.ShellModule;

namespace BattleCity.MapItems.StaticItems
{
    public class BrickWall : BaseItem, IResponse
    {
        public BrickWall(Point position) : base(position)
        {
            SpriteImage = GetImage(GetType().Name);
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
