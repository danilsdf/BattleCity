using System;
using System.Collections.Generic;
using System.Drawing;
using BattleCity.Algorithms.Search;
using BattleCity.Enums;
using BattleCity.Game;

namespace BattleCity.MapItems.TankModule
{
    public abstract class ActiveTank : FireTank
    {
        protected List<Point> LastPath;
        protected string Name;
        protected ActiveTank(Rectangle rect, int speed, Direction direction, int shellSpeed)
            : base(rect, speed, direction, shellSpeed)
        { }

        private int _current = 1;

        protected void Animation()
        {
            _current = _current == 2 ? 1 : 2;

            SpriteImage = Direction switch
            {
                Direction.Up => GetImage($"{Name}Up{_current}"),
                Direction.Right => GetImage($"{Name}Right{_current}"),
                Direction.Down => GetImage($"{Name}Down{_current}"),
                Direction.Left => GetImage($"{Name}Left{_current}"),
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        protected IEnumerable<Point> GetPath(Point start, Point finish)
        {
           return CurrentLevel.CurrentAlgorithm switch
            {
                AlgorithmType.Dfs => DfsSearcher.GetRoute(start, finish),
                AlgorithmType.Bfs => BfsSearcher.GetRoute(start, finish),
                AlgorithmType.UniformCostSearch => UniformCostSearcher.GetRoute(start, finish),
                AlgorithmType.AStar => AStarSearcher.GetRoute(start, finish),
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}