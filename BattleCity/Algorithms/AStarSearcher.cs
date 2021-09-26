using System;
using System.Collections.Generic;
using System.Drawing;
using BattleCity.Algorithms.Model;
using BattleCity.Shared;

namespace BattleCity.Algorithms
{
    public class AStarSearcher : Searcher
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
                var (_, value) = fringe.Dequeue();
                foreach (var neighbour in GetAStarCostNeighbours(value, Constants.Speed))
                {
                    if (Visited.Contains(neighbour.Value)) continue;
                    if (!fringe.Contains(neighbour.Value))
                    {
                        Visited.Add(neighbour.Value);
                        fringe.Enqueue(neighbour);

                        if (!Track.ContainsKey(neighbour.Value))
                            Track.Add(neighbour.Value, value);
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