using System;
using System.Drawing;
using System.Linq;
using BattleCity.Algorithms;
using BattleCity.Enums;
using BattleCity.Game;
using BattleCity.Interfaces;
using BattleCity.MapItems.ShellModule;
using BattleCity.Shared;

namespace BattleCity.MapItems.TankModule.Enemy.Base
{
    public abstract class EnemyTank : ActiveTank, IAddTank, IResponse
    {
        private int _rndDirection = 0, _fireDirection = 0;
        private readonly int _points;
        protected EnemyTankType TankType;

        protected EnemyTank(Rectangle rect, int speed, Direction direction, int shellSpeed, int points)
            : base(rect, speed, direction, shellSpeed)
        {
            var random = new Random();
            _rndDirection = random.Next(0, 50);
            _fireDirection = random.Next(0, 100);
            IsParked = false;
            _points = points;

            Cooldown = Constants.EnemyCoolDown;
        }

        public void AddTank()
        {
            CurrentLevel.DictionaryObjGame[MapItemKey.TankEnemy].Add(this);
            TankType = CurrentLevel.TankType;
            CurrentLevel.ChangeTankType();
        }

        public override void Update()
        {
            Moving();
            if (!CollisionLevel()) CheckGoingBeyondBoundaries();
            Animation();
        }

        protected void Moving()
        {
            if (TankType == EnemyTankType.MovingByAlgorithm) MovingByAlgorithm();
            else MovingRandomly();
        }

        private void MovingByAlgorithm()
        {
            var myPoint = Rect.Location;
            var playerPoint = CurrentLevel.Player.Rect.Location;
            OldDirection = Direction;
            var point = playerPoint;
            var path = BfsSearcher.GetRoute(myPoint, playerPoint);
            if (path != null) LastPath = path.ToList();

            if (LastPath != null && LastPath.Any())
            {
                point = LastPath.First();
                LastPath.RemoveAt(0);
                while (point.Equals(myPoint) && LastPath.Any())
                {
                    point = LastPath.First();
                    LastPath.RemoveAt(0);
                }

                if (point.Equals(myPoint)) point = playerPoint;
            }

            if (point.X > myPoint.X && CurrentLevel.IsPointEmpty(new Point(point.X + Constants.DifPoint, point.Y))) NewDirection = Direction.Right;
            else if (point.X < myPoint.X && CurrentLevel.IsPointEmpty(new Point(point.X - Constants.DifPoint, point.Y))) NewDirection = Direction.Left;
            else if (point.Y > myPoint.Y && CurrentLevel.IsPointEmpty(new Point(point.X, point.Y + Constants.DifPoint))) NewDirection = Direction.Down;
            else if (point.Y < myPoint.Y && CurrentLevel.IsPointEmpty(new Point(point.X, point.Y - Constants.DifPoint))) NewDirection = Direction.Up;
            Move();

            if (playerPoint.X != point.X && playerPoint.Y != point.Y) return;
            if (Cooldown != 0)
            {
                Cooldown--;
                return;
            }

            Cooldown = Constants.EnemyCoolDown;
            Fire(MapItemKey.TankEnemy);
        }

        private void MovingRandomly()
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
                Fire(MapItemKey.TankEnemy);
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