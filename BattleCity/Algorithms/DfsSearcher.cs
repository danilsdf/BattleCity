﻿using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace BattleCity.Algorithms
{
    public class DfsSearcher : Searcher
    {
        public static IEnumerable<Point> GetRoute(Point start, Point finish)
        {
            if (start.Equals(finish)) return new[] { finish };

            var queue = new Stack<Point>();
            Visited = new List<Point>();
            queue.Push(start);
            Visited.Add(start);
            Track = new Dictionary<Point, Point> { { start, default } };

            while (queue.Count != 0)
            {
                var currentCell = queue.Pop();
                foreach (var neighbour in GetAccessNeighbours(currentCell, 40))
                {
                    if (Visited.Contains(neighbour)) continue;
                    if (!queue.Contains(neighbour))
                    {
                        Visited.Add(neighbour);
                        queue.Push(neighbour);

                        if (!Track.ContainsKey(neighbour))
                            Track.Add(neighbour, currentCell);
                    }
                    if (neighbour.Equals(finish))
                    {
                        return RouteRestore(finish).Reverse();
                    }
                }
            }

            return null;

        }
    }
}