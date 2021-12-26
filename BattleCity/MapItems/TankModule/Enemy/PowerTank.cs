using System.Drawing;
using BattleCity.Enums;
using BattleCity.MapItems.TankModule.Enemy.Base;
using BattleCity.Shared;

namespace BattleCity.MapItems.TankModule.Enemy
{
    internal class PowerTank : EnemyTank
    {
        public PowerTank(Point position, Direction direction)
            : base(new Rectangle(position.X, position.Y, Constants.Size.WidthTank, Constants.Size.HeightTank), Constants.EnemySpeed, direction, 12, 300)
        {
            Name = "Power";
            SpriteImage = GetImage($"{Name}Down1");
        }
    }
}
