using System.Drawing;
using BattleCity.Enums;
using BattleCity.MapItems.TankModule.Enemy.Base;
using BattleCity.Shared;

namespace BattleCity.MapItems.TankModule.Enemy
{
    internal class FastTank : EnemyTank
    {
        public FastTank(Point position, Direction direction)
            : base(new Rectangle(position.X, position.Y, Constants.Size.WidthTank, Constants.Size.HeightTank), 6, direction, 8, 200)
        {
            Name = "Fast";
            SpriteImage = GetImage($"{Name}Down1");
        }
    }
}
