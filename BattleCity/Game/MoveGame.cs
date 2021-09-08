using System;
using System.Drawing;
using BattleCity.Enums;
using BattleCity.MapItems.Base;

namespace BattleCity.Game
{
    public abstract class MoveGame : BaseItem
    {
        public Direction Direction;
        protected int Speed;

        protected MoveGame(Rectangle rect, int speed, Direction direction)
            : base(rect)
        {
            Speed = speed;
            Direction = direction;
        }

        protected virtual void Move()
        {
            switch (Direction)
            {
                case Direction.Up:
                    SpriteRectangle.Y -= Speed;
                    break;
                case Direction.Right:
                    SpriteRectangle.X += Speed;
                    break;
                case Direction.Down:
                    SpriteRectangle.Y += Speed;
                    break;
                case Direction.Left:
                    SpriteRectangle.X -= Speed;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
