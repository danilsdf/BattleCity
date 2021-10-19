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

            public const int SquareCount = 26;

            public const int HeightBoard = SquareCount * HeightTile;

            public const int WidthBoard = SquareCount * WidthTile;

            public const int WindowClientHeight = 28 * HeightTile;

            public const int WindowClientWidth = 31 * WidthTile;
        }


        public static class Timer
        {

            public const int RemovePointsTimeout = 10;

            public const int RemovePathTimeout = 2;

            public const int RemoveRandomWall = 100;

            public const int CreateRandomWall = 50;

            public const int GameOverTimeout = 150;

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
            public const string ResultFilePath = @"D:\Университет\(5)Fiveth_Semestr\AI_Basic\BattleCity\GameResults.csv";

            public const string SoundPath = @"..\..\..\Content\Media\";

            public static string Content = @"..\..\..\Content\Images\";

            public static string LevelMaps = @"..\..\..\Content\LevelMaps\";

        }
        public static int[] XPoints = new[] { 0, 80, 160, 320, 380 };

        public const int CountLevel = 35;

        public const int PlayerCoolDown = 30;

        public const int EnemyCoolDown = 50;

        public const int HitCount = 1000;

        public const int Speed = 20;

        public const int DifPoint = 20;
    }
}
