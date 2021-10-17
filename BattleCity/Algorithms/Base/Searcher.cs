using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using BattleCity.Game;
using BattleCity.MapItems.StaticItems;

namespace BattleCity.Algorithms.Base
{
    public class Searcher
    {
        protected static List<Point> Visited = new List<Point>();
        protected static Dictionary<Point, Point> Track = new Dictionary<Point, Point>();


        public static IEnumerable<Point> GetAccessNeighbours(Point cell, int speed)
        {
            var topPoint = new Point(cell.X, cell.Y + speed);
            var rightPoint = new Point(cell.X + speed, cell.Y);
            var bottomPoint = new Point(cell.X, cell.Y - speed);
            var leftPoint = new Point(cell.X - speed, cell.Y);
            var points = new[] { topPoint, bottomPoint, leftPoint, rightPoint };

            return points.Where(CurrentLevel.IsPointEmpty);
        }

        public static IEnumerable<Tuple<int, Point>> GetCostNeighbours(Point cell, int speed)
        {
            return GetAccessNeighbours(cell, speed)
                .Select(point =>
                    new Tuple<int, Point>((cell.X + point.Y) / 2, point))
                .OrderBy(keyValue => keyValue.Item1);
        }

        public static IEnumerable<KeyValuePair<int, Point>> GetAStarCostNeighbours(Point cell, int speed)
        {
            return GetAccessNeighbours(cell, speed)
                .Select(point =>
                    new KeyValuePair<int, Point>(((cell.X + point.Y) / 2) + GetHeuristicPathLength(cell, point), point))
                .OrderBy(keyValue => keyValue.Key);
        }

        private static int GetHeuristicPathLength(Point from, Point to)
        {
            return Math.Abs(from.X - to.X) + Math.Abs(from.Y - to.Y);
        }

        public static IEnumerable<Point> RouteRestore(Point finish)
        {
            var currentPoint = finish;
            yield return currentPoint;
            while (Track[currentPoint] != default)
            {
                new ColorPoint(Track[currentPoint], "YellowPoint");
                yield return Track[currentPoint];
                currentPoint = Track[currentPoint];
            }
        }

        public static IEnumerable<Point> RouteRestore(Point start, Point finish)
        {
            var currentPoint = finish;
            yield return currentPoint;
            while (Track[currentPoint] != default)
            {
                if (!Track[currentPoint].Equals(start))
                {
                    new ColorPoint(Track[currentPoint], "RedPoint");
                    yield return Track[currentPoint];
                }

                currentPoint = Track[currentPoint];
            }
        }
    }
}
