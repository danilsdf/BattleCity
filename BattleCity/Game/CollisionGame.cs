using System;
using System.Drawing;
using BattleCity.Enums;
using BattleCity.Shared;

namespace BattleCity.Game
{
    public abstract class CollisionGame : MoveGame
    {
        protected CollisionGame(Rectangle rect, int speed, Direction direction)
            : base(rect, speed, direction)
        { }

        protected bool CheckGoingBeyondBoundaries()
        {
            switch (Direction)
            {
                case Direction.Up:
                    if (SpriteRectangle.Y <= 0)
                    {
                        SpriteRectangle.Y = 0;
                        return true;
                    }
                    break;
                case Direction.Right:
                    if (SpriteRectangle.X >= Constants.Size.WidthBoard - SpriteRectangle.Width)
                    {
                        SpriteRectangle.X = Constants.Size.WidthBoard - SpriteRectangle.Width;
                        return true;
                    }
                    break;
                case Direction.Down:
                    if (SpriteRectangle.Y >= Constants.Size.HeightBoard - SpriteRectangle.Height)
                    {
                        SpriteRectangle.Y = Constants.Size.HeightBoard - SpriteRectangle.Height;
                        return true;
                    }
                    break;
                case Direction.Left:
                    if (SpriteRectangle.X <= 0)
                    {
                        SpriteRectangle.X = 0;
                        return true;
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return false;
        }
    }
}
