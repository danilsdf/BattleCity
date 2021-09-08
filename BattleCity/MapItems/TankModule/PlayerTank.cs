using System.Drawing;
using BattleCity.Enums;
using BattleCity.Game;
using BattleCity.Interfaces;
using BattleCity.MapItems.ShellModule;
using BattleCity.Shared;
using BattleCity.SoundPart;

namespace BattleCity.MapItems.TankModule
{
    public class PlayerTank : ActiveTank, IResponse, IAddTank
    {
        public PlayerTank(Rectangle rect, int speed, Direction direction, int shellSpeed)
            : base(rect, speed, direction, shellSpeed)
        {
            Name = "SmallTankPlayer";
            SpriteImage = GetImage($"{Name}Down1");
        }

        protected void PlaySound()
        {
            if (!IsParked)
            {
                SoundService.SoundMove();
            }
            else if (IsParked)
            {
                SoundService.SoundStop();
            }
        }

        public void AddTank()
        {
            CurrentLevel.DictionaryObjGame[MapItemKey.Player].Add(this);
        }

        public void Response(Shell shell)
        {
            if (shell.TankOwner != MapItemKey.TankEnemy) return;

            CurrentLevel.DictionaryObjGame[MapItemKey.Player].Remove(this);
            shell.Detonation = true;

            CurrentLevel.LevelState = LevelState.GameOver;//todo
            new DetonationShellBig(shell.Rect.Location, shell.Direction, 0);
            SoundService.Stop();
        }

        public override void Update()
        {
            Moving();
            if (!CollisionLevel()) CheckGoingBeyondBoundaries();

            PlaySound();
            if (!IsParked) Animation();
        }

        protected void Moving()
        {
            OldDirection = Direction;

            if (Keyboard.Left)
            {
                NewDirection = Direction.Left;
                IsParked = false;
                Move();
            }
            else if (Keyboard.Right)
            {
                NewDirection = Direction.Right;
                IsParked = false;
                Move();
            }
            else if (Keyboard.Up)
            {
                NewDirection = Direction.Up;
                IsParked = false;
                Move();
            }
            else if (Keyboard.Down)
            {
                NewDirection = Direction.Down;
                IsParked = false;
                Move();
            }
            else
            {
                IsParked = true;
                Move();
            }

            if (Keyboard.Space)
            {
                Fire(MapItemKey.Player);
            }
        }
    }
}
