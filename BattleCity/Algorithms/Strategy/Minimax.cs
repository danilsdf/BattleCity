using System;
using System.Drawing;
using BattleCity.Algorithms.Base;
using BattleCity.Shared;
using static System.Int32;

namespace BattleCity.Algorithms.Strategy
{
    public class MiniMax : Searcher
    {
        public static int Minimax(Tuple<int, Point> start, Point finish, int alpha, int beta, bool isMax)
        {
            if (start.Item2.Equals(finish)) return start.Item1;
            int bestValue;

            if (isMax)
            {
                bestValue = MinValue;
                foreach (var neighbour in GetCostNeighbours(start.Item2, Constants.Speed))
                {
                    var value = Minimax(neighbour, finish, alpha, beta, false);
                    bestValue = Math.Max(bestValue, value);
                    alpha = Math.Max(alpha, value);
                    if (beta <= alpha) break;
                }
                start = new Tuple<int, Point>(bestValue, start.Item2);
                return bestValue;
            }

            bestValue = MaxValue;
            foreach (var neighbour in GetCostNeighbours(start.Item2, Constants.Speed))
            {
                var value = Minimax(neighbour, finish, alpha, beta, true);
                bestValue = Math.Min(bestValue, value);
                alpha = Math.Min(beta, value);
                if (beta <= alpha) break;
            }
            start = new Tuple<int, Point>(bestValue, start.Item2);
            return bestValue;
        }
    }
}
