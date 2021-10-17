using System.Collections.Generic;
using System.IO;
using System.Text;
using BattleCity.Shared;

namespace BattleCity.GameResult
{
    public class CsvFileWriter
    {
        public static void AppendGameInfo(GameResultModel result)
        {
            var csv = new StringBuilder();

            csv.Append(result);

            var list = new List<string>
            {
                csv.ToString(),
            };
            File.AppendAllLines(Constants.Path.ResultFilePath, list);
        }
    }
}
