﻿using System;
using System.Drawing;
using System.Linq;
using BattleCity.Algorithms;
using BattleCity.Enums;
using BattleCity.Game;
using BattleCity.Interfaces;
using BattleCity.MapItems.ShellModule;
using BattleCity.MapItems.StaticItems;
using BattleCity.Shared;
using BattleCity.SoundPart;

namespace BattleCity.MapItems.TankModule
{
    public class PlayerTank : ActiveTank, IResponse, IAddTank
    {
        public Point PointToMove = new Point(0,0);
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
            if (CurrentLevel.PlayerHealth != 0)
            {
                CurrentLevel.PlayerHealth--;
                return;
            }
            //CurrentLevel.DictionaryObjGame[MapItemKey.Player].Remove(this);
            shell.Detonation = true;

            //CurrentLevel.LevelState = LevelState.GameOver;//todo
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
            var myPoint = Rect.Location;
            if (myPoint.Equals(PointToMove))
            {
                var rnd = new Random();
                var x = 40;
                var y = 40;
                while (!CurrentLevel.IsPointEmpty(new Point(x, y)))
                {
                    x = Constants.XPoints[rnd.Next(0, Constants.XPoints.Length - 1)];

                    y = rnd.Next(0, 20) * 20;
                    if (x == 40 && y == 40) x = y = 0;
                }

                PointToMove = new Point(x, y);
            }
            new ColorPoint(PointToMove, "RedPoint");
            IsParked = false;
            OldDirection = Direction;
            var point = PointToMove;
            var path = AStarSearcher.GetRoute(myPoint, PointToMove);
            if (path != null) LastPath = path.ToList();

            if (LastPath != null && LastPath.Any())
            {
                point = LastPath.Last();
                LastPath.RemoveAt(0);
                while (point.Equals(myPoint) && LastPath.Any())
                {
                    point = LastPath.Last();
                    LastPath.RemoveAt(0);
                }
                if (point.Equals(myPoint)) point = PointToMove;
            }

            //todo check shell in direction to an eagle


            if (point.Y > myPoint.Y && CurrentLevel.IsPointEmpty(new Point(myPoint.X, myPoint.Y + 20))) NewDirection = Direction.Down;
             else if (point.Y < myPoint.Y && CurrentLevel.IsPointEmpty(new Point(myPoint.X, myPoint.Y - 20))) NewDirection = Direction.Up;
             else if(point.X > myPoint.X && CurrentLevel.IsPointEmpty(new Point(myPoint.X + 20, myPoint.Y))) NewDirection = Direction.Right;
             else if (point.X < myPoint.X && CurrentLevel.IsPointEmpty(new Point(myPoint.X - 20, myPoint.Y))) NewDirection = Direction.Left;

            if (CurrentLevel.IsFire(point)) Fire(MapItemKey.Player);

            if (Keyboard.Left)
            {
                NewDirection = Direction.Left;
                Move();
            }
            else if (Keyboard.Right)
            {
                NewDirection = Direction.Right;
                Move();
            }
            else if (Keyboard.Up)
            {
                NewDirection = Direction.Up;
                Move();
            }
            else if (Keyboard.Down)
            {
                NewDirection = Direction.Down;
                Move();
            }
            else
            {
                Move();
            }

            if (Keyboard.Space)
            {
                Fire(MapItemKey.Player);
            }
        }
    }
}
