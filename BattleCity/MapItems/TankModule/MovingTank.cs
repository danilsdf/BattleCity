using System;
using System.Drawing;
using BattleCity.Enums;
using BattleCity.Game;
using BattleCity.Shared;

namespace BattleCity.MapItems.TankModule
{
    public abstract class MovingTank : CollisionGame
    {
        protected bool IsParked;
        protected Direction OldDirection;
        protected Direction NewDirection;

        protected MovingTank(Rectangle rect, int speed, Direction direction)
            : base(rect, speed, direction)
        {
            IsParked = true;
            NewDirection = OldDirection = direction;
        }

        protected override void Move()
        {
            if (!IsParked)
            {
                if (NewDirection != OldDirection)
                {
                    if (IsGoingToBorderTile()) Direction = NewDirection;
                }
                else base.Move();
            }
            else IsGoingToBorderTile();
        }

        private bool IsGoingToBorderTile()
        {
            var tmp = (OldDirection - NewDirection);
            if (tmp == 2 || tmp == -2)
            {
                Direction = NewDirection;
                return false;
            }

            var offsetX = SpriteRectangle.X % Constants.Size.WidthTile;
            var offsetY = SpriteRectangle.Y % Constants.Size.HeightTile;


            switch (Direction)
            {
                case Direction.Left when offsetX == 0:
                    return true;
                case Direction.Left when offsetX >= Speed:
                    SpriteRectangle.X -= Speed;
                    break;
                case Direction.Left:
                    SpriteRectangle.X -= offsetX;
                    break;
                case Direction.Up when offsetY == 0:
                    return true;
                case Direction.Up when offsetY >= Speed:
                    SpriteRectangle.Y -= Speed;
                    break;
                case Direction.Up:
                    SpriteRectangle.Y -= offsetY;
                    break;
                case Direction.Right:
                {
                    var rightOffset = Constants.Size.WidthTile - offsetX;
                    if (rightOffset == Constants.Size.WidthTile)
                        return true;

                    if (rightOffset >= Speed)
                        SpriteRectangle.X += Speed;
                    else
                        SpriteRectangle.X += rightOffset;
                    break;
                }
                case Direction.Down:
                {
                    var downOffset = Constants.Size.HeightTile - offsetY;
                    if (downOffset == Constants.Size.HeightTile)
                        return true;

                    if (downOffset >= Speed)
                        SpriteRectangle.Y += Speed;
                    else
                        SpriteRectangle.Y += downOffset;
                    break;
                }
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return false;
        }
    }
}
