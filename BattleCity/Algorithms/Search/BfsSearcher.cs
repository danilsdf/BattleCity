using System.Collections.Generic;
using System.Drawing;
using BattleCity.Algorithms.Base;
using BattleCity.Shared;

namespace BattleCity.Algorithms.Search
{
    public class BfsSearcher : Searcher
    {
        public static IEnumerable<Point> GetRoute(Point start, Point finish)
        {
            if (start.Equals(finish)) return new[] {finish};

            var queue = new Queue<Point>();
            Visited = new List<Point>();
            queue.Enqueue(start);
            Visited.Add(start);
            Track = new Dictionary<Point, Point> { { start, default } };

            while (queue.Count != 0)
            {
                var currentCell = queue.Dequeue();
                foreach (var neighbour in GetAccessNeighbours(currentCell, Constants.Speed))
                {
                    if (Visited.Contains(neighbour)) continue;
                    if (!queue.Contains(neighbour))
                    {
                        Visited.Add(neighbour);
                        queue.Enqueue(neighbour);

                        if (!Track.ContainsKey(neighbour))
                            Track.Add(neighbour, currentCell);
                    }
                    
                    if (neighbour.Equals(finish))
                    {
                        return RouteRestore(start, finish);
                    }
                }
            }

            return null;

        }
    }
}
