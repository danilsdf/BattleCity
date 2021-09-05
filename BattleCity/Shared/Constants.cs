using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

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

            private const int ConstWindowClientHeight = 28 * HeightTile;

            public static int WindowClientHeight { get; private set; }

            public const int WindowClientWidth = 31 * WidthTile;
        }


        public static class Timer
        {

            public const int DetonationTimeout = 6;

            public const int TimerInterval = 20;

            public const int AppearanceOfTankTimeout = 48;

            public const int BigDetonationTimeout = 10;

            public const int GameOverTimeout = 4000;

            public const int ScreenGameOverTimeout = 2000;

            public const int CloseTimeout = 300;

            public static TimeSpan DelayScreenLoadLevel = TimeSpan.FromSeconds(3);

            public static TimeSpan DelayScreenRecord = TimeSpan.FromSeconds(8);

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

            public const char CharPlainTank = 'P';

            public const char CharArmoredPersonnelCarrierTank = 'A';

            public const char CharQuickFireTank = 'R';

            public const char CharArmoredTank = 'B';
        }

        public static class Path
        {

            public const string TexturePath = @"Content\Textures\";

            public const string SoundPath = @"Content\Media\";

            public const string MaxPointsPath = @"Content\MaxPoints";

        }

        public static class Volume
        {
            public const float VolumeDefault = 0.5f;

            public const float VolumeIncrement = 0.1f;
        }


        public const int CountLevel = 35;

        public static readonly Color BackColor = Color.DimGray;

        public const int DelayChangeColorBonusTank = 6;

        public const int ZIndexTank = 5;
}
}
