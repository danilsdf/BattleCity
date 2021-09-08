using System;
using System.Drawing;

namespace BattleCity.Shared
{
    public class Constants
    {
        public static class Size
        {
            public const int HeightTile = 20;

            public const int WidthTile = 20;

            public const int HeightTank = 2 * HeightTile;

            public const int WidthTank = 2 * WidthTile;

            public const int HeightShell = 7;

            public const int WidthShell = 5;

            public const int HeightBoard = 26 * HeightTile;

            public const int WidthBoard = 26 * WidthTile;

            public const int WindowClientHeight = 28 * HeightTile;

            public const int WindowClientWidth = 31 * WidthTile;
        }


        public static class Timer
        {

            public const int RemovePointsTimeout = 10;

            public const int GameOverTimeout = 200;

            public static TimeSpan GetDelayScreenPoints(int countTank)
            {
                return TimeSpan.FromMilliseconds(150 * (countTank + 9) + 1000);
            }

        }

        public static class CharValue
        {

            public const char CharBrickWall = '#';

            public const char CharConcreteWall = '@';

            public const char CharWater = '~';

            public const char CharForest = '%';

            public const char CharIce = '-';

            public const char CharSimpleTank = 'P';

            public const char CharFastTank = 'A';

            public const char CharPowerTank = 'R';

            public const char CharArmoredTank = 'B';
        }

        public static class Path
        {
            public static string PathToProject = Environment.CurrentDirectory;

            public const string SoundPath = @"..\..\..\Content\Media\";

            public static string Content = @"..\..\..\Content\Images\";

            public static string LevelMaps = @"..\..\..\Content\LevelMaps\";

        }

        public const int CountLevel = 35;
}
}
