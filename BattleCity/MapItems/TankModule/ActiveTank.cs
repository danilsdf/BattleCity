using System;
using System.Collections.Generic;
using System.Drawing;
using BattleCity.Enums;

namespace BattleCity.MapItems.TankModule
{
    public abstract class ActiveTank : FireTank
    {
        protected List<Point> LastPath;
        protected string Name;
        protected ActiveTank(Rectangle rect, int speed, Direction direction, int shellSpeed)
            : base(rect, speed, direction, shellSpeed)
        { }

        private int _current = 1;

        protected void Animation()
        {
            _current = _current == 2 ? 1 : 2;

            SpriteImage = Direction switch
            {
                Direction.Up => GetImage($"{Name}Up{_current}"),
                Direction.Right => GetImage($"{Name}Right{_current}"),
                Direction.Down => GetImage($"{Name}Down{_current}"),
                Direction.Left => GetImage($"{Name}Left{_current}"),
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}