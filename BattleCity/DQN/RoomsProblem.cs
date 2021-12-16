using System;
using System.Collections.Generic;
using BattleCity.Shared;

namespace BattleCity.DQN
{
    public class RoomsProblem: IQLearningProblem
        {
            private double[,] _rewards = new double[Constants.Size.SquareCount, Constants.Size.SquareCount];

            public int NumberOfStates => Constants.Size.SquareCount;

            public int NumberOfActions => Constants.Size.SquareCount;

            public void FulfillArray(double[,] rewards)
            {
                _rewards = rewards;
            }

            public double GetReward(int currentState, int action)
            {
                return _rewards[currentState,action];
            }

            public int[] GetValidActions(int currentState)
            {
                List<int> validActions = new List<int>();
                for (int i = 0; i < Constants.Size.SquareCount; i++)
                {
                    if (Math.Abs(_rewards[currentState, i] - (-1)) > 0.001)
                        validActions.Add(i);
                }
                return validActions.ToArray();
            }

            public bool GoalStateIsReached(int currentState)
            {
                return currentState == 5;
            }
        }
}