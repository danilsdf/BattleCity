using System;
using System.Collections.Generic;
using System.Linq;

namespace BattleCity.DQN
{
    public class QLearning
    {
        private Random _random = new Random();
        private double _gamma;
        public double Gamma { get => _gamma; }

        private double[][] _qTable;
        public double[][] QTable { get => _qTable; }

        private IQLearningProblem _qLearningProblem;
        
        public QLearning(double gamma, IQLearningProblem qLearningProblem)
        {
            _qLearningProblem = qLearningProblem;
            _gamma = gamma;
            _qTable = new double[qLearningProblem.NumberOfStates][];
            for(int i = 0; i < qLearningProblem.NumberOfStates; i++)
                _qTable[i] = new double[qLearningProblem.NumberOfActions];
        }

        public void TrainAgent(int numberOfIterations)
        {
            for(int i = 0; i < numberOfIterations; i++)
            {
                int initialState = SetInitialState(_qLearningProblem.NumberOfStates);
                InitializeEpisode(initialState);
            }
        }

        public Models.QLearningStats Run(int initialState)
        {
            if (initialState < 0 || initialState > _qLearningProblem.NumberOfStates) throw new ArgumentException($"The initial state can be between [0-{_qLearningProblem.NumberOfStates}", nameof(initialState));
            
            var result = new Models.QLearningStats();
            result.InitialState = initialState;
            int state = initialState;
            List<int> actions = new List<int>();
            while (true)
            {
                result.Steps += 1;
                int action = _qTable[state].ToList().IndexOf(_qTable[state].Max());
                state = action;
                actions.Add(action);
                if (_qLearningProblem.GoalStateIsReached(action))
                {
                    result.EndState = action;
                    break;
                }
            }
            result.Actions = actions.ToArray();
            return result;
        }

        private void InitializeEpisode(int initialState)
        {
            int currentState = initialState;
            while (true)
            {
                currentState = TakeAction(currentState);
                if (_qLearningProblem.GoalStateIsReached(currentState))
                    break;
            }
        }

        private int TakeAction(int currentState)
        {
            var validActions = _qLearningProblem.GetValidActions(currentState);
            int randomIndexAction = _random.Next(0, validActions.Length);
            int action = validActions[randomIndexAction];

            double saReward = _qLearningProblem.GetReward(currentState, action);
            double nsReward = _qTable[action].Max();          
            double qCurrentState = saReward + (_gamma * nsReward);
            _qTable[currentState][action] = qCurrentState;
            int newState = action;
            return newState;
        }

        private int SetInitialState(int numberOfStates)
        {
            return _random.Next(0, numberOfStates);
        }
    }
}
