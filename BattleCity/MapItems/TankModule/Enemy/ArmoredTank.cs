using System.Drawing;
using BattleCity.Enums;
using BattleCity.MapItems.ShellModule;
using BattleCity.MapItems.TankModule.Enemy.Base;
using BattleCity.Shared;
using BattleCity.SoundPart;

namespace BattleCity.MapItems.TankModule.Enemy
{
    internal class ArmoredTank : EnemyTank
    {
        private int _numberOfHits;

        public ArmoredTank(Point position, Direction direction)
            : base(new Rectangle(position.X, position.Y, Constants.Size.WidthTank, Constants.Size.HeightTank), Constants.EnemySpeed, direction, 8, 400)
        {
            _numberOfHits = 0;

            Name = "Armored";
            SpriteImage = GetImage($"{Name}Down1");
        }

        public override void Response(Shell shell)
        {
            if (shell.TankOwner == MapItemKey.TankEnemy) return;

            switch (_numberOfHits)
            {
                case 0:
                    SoundService.SoundDetonation();
                    Name = "ArmoredG";
                    shell.Detonation = true;
                    _numberOfHits++;
                    break;
                case 1:
                    SoundService.SoundDetonation();
                    Name = "ArmoredY";
                    shell.Detonation = true;
                    _numberOfHits++;
                    break;
                case 2:
                    SoundService.SoundDetonation();
                    Name = "Armored";
                    shell.Detonation = true;
                    _numberOfHits++;
                    break;
                case 3:
                    base.Response(shell);
                    break;
            }
        }
    }
}
