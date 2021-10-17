using System;
using System.Drawing;
using BattleCity.Algorithms.Base;
using BattleCity.Shared;
using static System.Int32;

namespace BattleCity.Algorithms.Strategy
{
    public class ExpectimaxAlgorithm : Searcher
    {
        public static int Expectimax(Tuple<int, Point> start, Point finish, bool isMax)
        {
            if (start.Item2.Equals(finish)) return start.Item1;
            int bestValue;

            if (isMax)
            {
                bestValue = MinValue;
                foreach (var neighbour in GetCostNeighbours(start.Item2, Constants.Speed))
                {
                    var value = Expectimax(neighbour, finish, false);
                    bestValue = Math.Max(bestValue, value);
                }
                start = new Tuple<int, Point>(bestValue, start.Item2);
                return bestValue;
            }

            bestValue = MaxValue;
            foreach (var neighbour in GetCostNeighbours(start.Item2, Constants.Speed))
            {
                var value = Expectimax(neighbour, finish, true);
                bestValue = Math.Min(bestValue, value);
            }
            start = new Tuple<int, Point>(bestValue, start.Item2);
            return bestValue;
        }
    }
}
