using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using BattleCity.Shared;
using CsvHelper;
using CsvHelper.Configuration;

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
