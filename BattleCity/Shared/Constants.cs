namespace BattleCity.Shared
{
    public class Constants
    {
        public static class Size
        {
            public static int HeightTile = 20;

            public static int WidthTile = 20;

            public static int HeightTank = 2 * HeightTile;

            public static int WidthTank = 2 * WidthTile;

            public static int HeightShell = 7;

            public static int WidthShell = 5;

            public static int SquareCount = 26;

            public static int HeightBoard = SquareCount * HeightTile;

            public static int WidthBoard = SquareCount * WidthTile;

            public static int WindowClientHeight = 28 * HeightTile;

            public static int WindowClientWidth = 31 * WidthTile;
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
            public const string ResultFilePath = @"D:\University\(5)Fiveth_Semestr\AI_Basic\BattleCity\Lisp\GameResults.csv";

            public const string SoundPath = @"..\..\..\Content\Media\";

            public static string Content = @"..\..\..\Content\Images\";

            public static string LevelMaps = @"..\..\..\Content\LevelMaps\";

        }
        public static int[] XPoints = new[] { 0, 80, 160, 320, 380 };

        public const int CountLevel = 35;

        public static int PlayerCoolDown = 30;

        public static int EnemyCoolDown = 50;

        public static int HitCount { get; set; }

        public static int Speed = 20;

        public const int DifPoint = 20;

        public static int CountEnemy = 10;

        public static int EnemySpeed = 4;

        public static int PlayerSpeed = 4;
    }
}
