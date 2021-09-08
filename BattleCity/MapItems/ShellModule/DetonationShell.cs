using System;
using System.Drawing;
using BattleCity.Enums;
using BattleCity.Game;
using BattleCity.MapItems.Base;

namespace BattleCity.MapItems.ShellModule
{
    public class DetonationShell : BaseItem
    {
        protected int IntervalBetweenMove = 0;
        private readonly Direction _direction;

        public DetonationShell(Point position, Direction direction)
            : base(new Rectangle(position.X, position.Y, 0, 0))
        {
            _direction = direction;

            SpriteImage = GetImage("Detonation");
            Position();

            CurrentLevel.DictionaryObjGame[MapItemKey.Other].Add(this);
        }

        public override void Update()
        {
            switch (IntervalBetweenMove)
            {
                case 2:
                    IntervalBetweenMove++;
                    SpriteImage = GetImage("Detonation2");
                    Position();
                    break;
                case 4:
                    IntervalBetweenMove++;
                    SpriteImage = GetImage("Detonation3");
                    Position();
                    break;
                case 6:
                    CurrentLevel.DictionaryObjGame[MapItemKey.Other].Remove(this);
                    break;
                default:
                    IntervalBetweenMove++;
                    break;
            }
        }

        protected void Position()
        {
            switch (_direction)
            {
                case Direction.Up:
                    SpriteRectangle.Location = new Point(SpriteRectangle.X - ((SpriteImage.Width - SpriteRectangle.Width) / 2), SpriteRectangle.Top - (SpriteImage.Height - SpriteRectangle.Width) / 2);
                    break;
                case Direction.Right:
                    SpriteRectangle.Location = new Point(SpriteRectangle.Left - (SpriteImage.Width - SpriteRectangle.Width) / 2, SpriteRectangle.Y - (SpriteImage.Height / 2 - SpriteRectangle.Height / 2));
                    break;
                case Direction.Down:
                    SpriteRectangle.Location = new Point(SpriteRectangle.X - (SpriteImage.Width / 2 - SpriteRectangle.Width / 2), SpriteRectangle.Top - (SpriteImage.Height - SpriteRectangle.Width) / 2);
                    break;
                case Direction.Left:
                    SpriteRectangle.Location = new Point(SpriteRectangle.Left - (SpriteImage.Width - SpriteRectangle.Width) / 2, SpriteRectangle.Y - (SpriteImage.Height / 2 - SpriteRectangle.Height / 2));
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            SpriteRectangle.Size = SpriteImage.Size;
        }
    }
}
