using System;
using BattleCity.Shared;

namespace BattleCity.GameResult
{
    public class GameResultModel
    {
        public bool IsWin { get; set; }
        public double Time { get; set; }
        public int Score { get; set; }
        public string Today { get; set; }

        public GameResultModel(bool isWin, TimeSpan time, int score)
        {
            IsWin = isWin;
            Time = time.TotalSeconds;
            Score = score;
            Today = DateTime.Now.ToShortDateString().Replace("/", ".");
        }

        public override string ToString()
        {
            var win = IsWin ? "1" : "0";
            return $"{win},{Time},{Score},{Constants.HitCount},{Today}";
        }
    }
}
