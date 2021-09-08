using System;
using System.Drawing;
using BattleCity.Enums;

namespace BattleCity.MapItems.TankModule
{
    public abstract class ActiveTank : FireTank
    {
        protected string Name;
        protected ActiveTank(Rectangle rect, int speed, Direction direction, int shellSpeed)
            : base(rect, speed, direction, shellSpeed)
        { }

        private int _current = 0;

        protected void Animation()
        {
            _current = _current == 1 ? 0 : 1;

            SpriteImage = Direction switch
            {
                Direction.Up => GetImage($"{Name}Up_{_current}"),
                Direction.Right => GetImage($"{Name}Right_{_current}"),
                Direction.Down => GetImage($"{Name}Down_{_current}"),
                Direction.Left => GetImage($"{Name}Left_{_current}"),
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}