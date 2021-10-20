using System;
using System.Drawing;
using BattleCity.Enums;
using BattleCity.Shared;

namespace BattleCity.Information
{
    public class AlgorithmInformation : GameInformation
    {
        public AlgorithmInformation()
            : base(new Rectangle(27 * Constants.Size.WidthTile, 10 * Constants.Size.HeightTile, 
                Constants.Size.WidthTank + 10, Constants.Size.HeightTank + 10))
        {
            SpriteImage = GetImage("Bfs");
        }

        public void Change(AlgorithmType type)
        {
            SpriteImage = type switch
            {
                AlgorithmType.Dfs => GetImage("Dfs"),
                AlgorithmType.Bfs => GetImage("Bfs"),
                AlgorithmType.UniformCostSearch => GetImage("Ucs"),
                AlgorithmType.AStar => GetImage("Ucs"),
                _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
            };
        }

        public void ChangeTime(long time)
        {
            SpriteText = $"{time} ms";
        }
    }
}