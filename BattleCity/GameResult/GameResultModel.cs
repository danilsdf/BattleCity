using System;

namespace BattleCity.GameResult
{
    public class GameResultModel
    {
        public bool IsWin { get; set; }
        public TimeSpan Time { get; set; }
        public int Score { get; set; }
        public string Algorithm { get; set; }
        public string Today { get; set; }

        public GameResultModel(bool isWin, TimeSpan time, int score, string algorithm)
        {
            IsWin = isWin;
            Time = time;
            Score = score;
            Algorithm = algorithm;
            Today = DateTime.Now.ToShortDateString().Replace("/", ".");
        }

        public override string ToString()
        {
            var win = IsWin ? "Win" : "Lose";
            return $"{win},{Time},{Score},{Algorithm},{Today}";
        }
    }
}
