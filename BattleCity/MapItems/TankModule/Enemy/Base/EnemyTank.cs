using System;
using System.Drawing;
using BattleCity.Enums;
using BattleCity.Game;
using BattleCity.Interfaces;
using BattleCity.MapItems.ShellModule;

namespace BattleCity.MapItems.TankModule.Enemy.Base
{
    public abstract class EnemyTank : ActiveTank, IAddTank, IResponse
    {
        private int _rndDirection = 0, _fireDirection = 0;
        private readonly int _points;

        protected EnemyTank(Rectangle rect, int speed, Direction direction, int shellSpeed, int points)
            : base(rect, speed, direction, shellSpeed)
        {
            var random = new Random();
            _rndDirection = random.Next(0, 50);
            _fireDirection = random.Next(0, 100);
            IsParked = false;
            _points = points;

        }

        public void AddTank()
        {
            CurrentLevel.DictionaryObjGame[MapItemKey.TankEnemy].Add(this);
        }

        public override void Update()
        {
            Moving();
            if (!CollisionLevel()) CheckGoingBeyondBoundaries();
            Animation();
        }

        protected void Moving()
        {
            var random = new Random();
            OldDirection = Direction;
            if (_rndDirection == 0)
            {
                _rndDirection = random.Next(6, 50);
                NewDirection = (Direction)random.Next(0, 4);
            }
            else _rndDirection--;

            Move();

            if (_fireDirection == 0)
            {
                _fireDirection = random.Next(0, 100);
                //Fire(MapItemKey.TankEnemy);
            }
            else _fireDirection--;
        }

        public virtual void Response(Shell shell)
        {
            if (shell.TankOwner == MapItemKey.TankEnemy) return;

            CurrentLevel.DictionaryObjGame[MapItemKey.TankEnemy].Remove(this);
            shell.Detonation = true;
            new DetonationShellBig(shell.Rect.Location, shell.Direction, _points);
        }
    }
}