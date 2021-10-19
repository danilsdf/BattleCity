using System;
using System.Drawing;
using System.Linq;
using BattleCity.Algorithms.Base;
using BattleCity.Algorithms.Strategy.Model;
using BattleCity.Shared;
using static System.Int32;

namespace BattleCity.Algorithms.Strategy
{
    public class MiniMax : Searcher
    {
        public static double Minimax(Node start, Point finish, double alpha, double beta, bool isMax)
        {
            if (start.Point.Equals(finish)) return start.Value;
            double bestValue;

            if (isMax)
            {
                bestValue = MinValue;
                foreach (var value in GetNeighboursNodes(start.Point, Constants.Speed).Select(neighbour => Minimax(neighbour, finish, alpha, beta, false)))
                {
                    bestValue = Math.Max(bestValue, value);
                    alpha = Math.Max(alpha, value);
                    if (beta <= alpha) break;
                }

                start.Value = bestValue;
                return bestValue;
            }

            bestValue = MaxValue;
            foreach (var value in GetNeighboursNodes(start.Point, Constants.Speed).Select(neighbour => Minimax(neighbour, finish, alpha, beta, true)))
            {
                bestValue = Math.Min(bestValue, value);
                alpha = Math.Min(beta, value);
                if (beta <= alpha) break;
            }
            start.Value = bestValue;
            return bestValue;
        }
    }
}
