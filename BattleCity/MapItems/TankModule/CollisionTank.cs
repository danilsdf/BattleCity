using System.Drawing;
using BattleCity.Enums;
using BattleCity.Game;
using BattleCity.MapItems.StaticItems;

namespace BattleCity.MapItems.TankModule
{
    public abstract class CollisionTank : MovingTank
    {
        protected CollisionTank(Rectangle rect, int speed, Direction direction)
            : base(rect, speed, direction)
        { }

        protected bool CollisionLevel()
        {
            foreach (var kv in CurrentLevel.DictionaryObjGame)
            {
                if (kv.Key == MapItemKey.Shell || kv.Key == MapItemKey.Ice || kv.Key == MapItemKey.Other) continue;
                if (kv.Key == MapItemKey.Ice) continue;
                if (kv.Key == MapItemKey.Other) continue;
                foreach (var obj in kv.Value)
                {
                    if (obj == this) continue;
                    if (obj is Eagle) continue;
                    if (!SpriteRectangle.IntersectsWith(obj.Rect)) continue;

                    Offset(obj.Rect);
                    return true;
                }
            }
            return false;
        }

        private void Offset(Rectangle rect)
        {
            while (true)
            {
                if (!SpriteRectangle.IntersectsWith(rect)) return;
                SpriteRectangle.Location = Direction switch
                {
                    Direction.Up => new Point(SpriteRectangle.X, SpriteRectangle.Y + 1),
                    Direction.Right => new Point(SpriteRectangle.X - 1, SpriteRectangle.Y),
                    Direction.Down => new Point(SpriteRectangle.X, SpriteRectangle.Y - 1),
                    Direction.Left => new Point(SpriteRectangle.X + 1, SpriteRectangle.Y),
                    _ => SpriteRectangle.Location
                };
            }
        }
    }
}
