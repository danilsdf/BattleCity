using System;
using System.Drawing;
using BattleCity.Enums;
using BattleCity.Game;

namespace BattleCity.MapItems.ShellModule
{
    public abstract class Shell : CollisionGame
    {
        public bool Detonation { get; set; }
        public bool IsAlive { get; set; }

        protected MapItemKey tankOwner;
        public MapItemKey TankOwner => tankOwner;

        protected Shell(Rectangle rect, int speed, Direction direction, MapItemKey tankOwner)
            : base(rect, speed, direction)
        {
            IsAlive = true;
            this.tankOwner = tankOwner;
            Speed = speed;
            Direction = direction;

            SpriteImage = direction switch
            {
                Direction.Up => GetImage("BulletUp"),
                Direction.Right => GetImage("BulletRight"),
                Direction.Down => GetImage("BulletDown"),
                Direction.Left => GetImage("BulletLeft"),
                _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
            };

            CurrentLevel.DictionaryObjGame[MapItemKey.Shell].Add(this);
        }
    }
}
