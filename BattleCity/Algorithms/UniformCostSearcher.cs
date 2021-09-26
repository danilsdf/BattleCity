using System.Collections.Generic;
using System.Drawing;
using BattleCity.Algorithms.Model;
using BattleCity.Shared;

namespace BattleCity.Algorithms
{
    internal class UniformCostSearcher : Searcher
    {
        public static IEnumerable<Point> GetRoute(Point start, Point finish)
        {
            var fringe = new PriorityQueue();
            Visited = new List<Point>();
            Track = new Dictionary<Point, Point> { { start, default } };

            fringe.Enqueue(0, start);
            Visited.Add(start);
            while (!fringe.IsEmpty)
            {
                var currentCell = fringe.Dequeue();
                foreach (var neighbour in GetCostNeighbours(currentCell.Value, Constants.Speed))
                {
                    if (Visited.Contains(neighbour.Value)) continue;
                    if (!fringe.Contains(neighbour.Value))
                    {
                        Visited.Add(neighbour.Value);
                        fringe.Enqueue(neighbour);

                        if (!Track.ContainsKey(neighbour.Value))
                            Track.Add(neighbour.Value, currentCell.Value);
                    }

                    if (neighbour.Value.Equals(finish))
                    {
                        return RouteRestore(finish);
                    }
                }
            }

            return null;
        }
    }
}