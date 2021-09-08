using System.Drawing;
using BattleCity.Enums;
using BattleCity.Game;
using BattleCity.MapItems.StaticItems;
using BattleCity.SoundPart;

namespace BattleCity.MapItems.ShellModule
{
    public class DetonationShellBig : DetonationShell
    {
        private readonly int _countPoints;

        public DetonationShellBig(Point position, Direction direction, int countPoints)
            : base(position, direction)
        {
            _countPoints = countPoints;
        }

        public override void Update()
        {
            switch (IntervalBetweenMove)
            {
                case 1:
                    SoundService.SoundBigDetonation();
                    CurrentUpdate("Detonation2");
                    break;
                case 2:
                    CurrentUpdate("Detonation3");
                    break;
                case 6:
                    CurrentUpdate("DetonationBig");
                    break;
                case 8:
                    CurrentUpdate("DetonationBig2");
                    break;
                case 10:
                {
                    CurrentLevel.DictionaryObjGame[MapItemKey.Other].Remove(this);
                    if (_countPoints != 0)
                        new MapPoint(new Point(SpriteRectangle.X + 21, SpriteRectangle.Y + 29), _countPoints);
                    break;
                }
                default:
                    IntervalBetweenMove++;
                    break;
            }
        }

        public void CurrentUpdate(string name)
        {
            IntervalBetweenMove++;
            SpriteImage = GetImage($"{name}");
            Position();
        }
    }
}
