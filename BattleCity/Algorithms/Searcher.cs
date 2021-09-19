using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using BattleCity.Game;

namespace BattleCity.Algorithms
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
            var t = points.Where(CurrentLevel.IsPointEmpty).ToArray();
            return points.Where(CurrentLevel.IsPointEmpty);
        }

        public static IEnumerable<Point> RouteRestore(Point finish)
        {
            var currentPoint = finish;
            yield return currentPoint;
            while (Track[currentPoint] != default)
            {
                yield return Track[currentPoint];
                currentPoint = Track[currentPoint];
            }
        }
    }
}
