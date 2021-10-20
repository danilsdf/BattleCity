using System;
using System.Drawing;
using System.Linq;
using BattleCity.Algorithms.Base;
using BattleCity.Algorithms.Strategy.Model;
using BattleCity.Shared;
using static System.Int32;

namespace BattleCity.Algorithms.Strategy
{
    public class ExpectimaxAlgorithm : Searcher
    {
        public static double Expectimax(Node start, Point finish, bool isMax)
        {
            if (start.Point.Equals(finish)) return start.Value;
            double bestValue;

            if (isMax)
            {
                bestValue = GetNeighboursNodes(start.Point, Constants.Speed)
                    .Select(neighbour => Expectimax(neighbour, finish, false))
                    .Concat(new double[] {MinValue}).Max();
            }
            else
            {
                bestValue = GetNeighboursNodes(start.Point, Constants.Speed)
                    .Select(neighbour => Expectimax(neighbour, finish, true))
                    .Concat(new double[] {MaxValue}).Min();
            }

            start.Value = bestValue;
            return bestValue;
        }
    }
}
