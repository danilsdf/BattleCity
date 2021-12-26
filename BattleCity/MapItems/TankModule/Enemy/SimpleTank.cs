using System.Drawing;
using BattleCity.Enums;
using BattleCity.MapItems.TankModule.Enemy.Base;
using BattleCity.Shared;

namespace BattleCity.MapItems.TankModule.Enemy
{
    internal class SimpleTank : EnemyTank
    {
        public SimpleTank(Point position, Direction direction)
            : base(new Rectangle(position.X, position.Y, Constants.Size.WidthTank, Constants.Size.HeightTank), Constants.EnemySpeed, direction, 8, 100)
        {
            Name = "Simple";
            SpriteImage = GetImage($"{Name}Down1");
        }
    }
}
