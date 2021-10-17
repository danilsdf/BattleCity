using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using BattleCity.Enums;
using BattleCity.GameResult;
using BattleCity.Information;
using BattleCity.Interfaces;
using BattleCity.MapItems.Base;
using BattleCity.MapItems.StaticItems;
using BattleCity.MapItems.TankModule;
using BattleCity.Shared;
using BattleCity.SoundPart;

namespace BattleCity.Game
{
    public class CurrentLevel
    {
        private int _currentLevel;
        private GameOverInformation _gameOverInformation;

        public static LevelState LevelState;
        public static Stopwatch Stopwatch = new Stopwatch();
        public static int Score;

        public static int PlayerHealth;

        private CurrentLevelInformation _currentLevelInformation;
        public static Dictionary<MapItemKey, List<BaseItem>> DictionaryObjGame;
        private readonly List<BaseItem> _listTankEnemy ;
        private readonly List<BaseItem> _listPlayer;
        private readonly List<BaseItem> _listWall;
        private readonly List<BaseItem> _listRandomWall;
        private readonly List<BaseItem> _listWater;
        private readonly List<BaseItem> _listShell;
        private readonly List<BaseItem> _listIce;
        private readonly List<BaseItem> _listOther;

        public static List<IDraw> ListInformation;
        public static AlgorithmInformation AlgorithmInformation;

        public static PlayerTank Player;
        private Eagle _eagle;

        private readonly Point _spawnEagle;
        private readonly Point _spawnPlayer;

        private int _timerWin;
        private int _timerSpawnWall = Constants.Timer.CreateRandomWall;

        public static EnemyTankType TankType = EnemyTankType.MovingByAlgorithm;
        private static AlgorithmType _currentAlgorithm = AlgorithmType.Bfs;

        private SpawnTanks _enemyTanks;
        private string _tanksFromMap;

        public CurrentLevel()
        {
            ListInformation = new List<IDraw>();
            DictionaryObjGame = new Dictionary<MapItemKey, List<BaseItem>>();

            _listTankEnemy = new List<BaseItem>();
            _listPlayer = new List<BaseItem>();
            _listWall = new List<BaseItem>();
            _listRandomWall = new List<BaseItem>();
            _listShell = new List<BaseItem>();
            _listWater = new List<BaseItem>();
            _listIce = new List<BaseItem>();
            _listOther = new List<BaseItem>();

            _spawnEagle = new Point(12 * Constants.Size.WidthTile, 24 * Constants.Size.HeightTile);
            _spawnPlayer = new Point(8 * Constants.Size.WidthTile, 24 * Constants.Size.HeightTile);
            AlgorithmInformation = new AlgorithmInformation();

            DictionaryObjGame.Add(MapItemKey.Ice, _listIce);
            DictionaryObjGame.Add(MapItemKey.TankEnemy, _listTankEnemy);
            DictionaryObjGame.Add(MapItemKey.Player, _listPlayer);
            DictionaryObjGame.Add(MapItemKey.Wall, _listWall);
            DictionaryObjGame.Add(MapItemKey.RandomWall, _listRandomWall);
            DictionaryObjGame.Add(MapItemKey.Water, _listWater);
            DictionaryObjGame.Add(MapItemKey.Shell, _listShell);
            DictionaryObjGame.Add(MapItemKey.Other, _listOther);

            PlayerHealth = 3;
        }

        private void TimerWin()
        {
            _currentLevel = 1;

            DownloadLevel(_currentLevel);
        }

        public static void ChangeTankType()
        {
            TankType = TankType == EnemyTankType.MovingByAlgorithm
                ? EnemyTankType.MovingRandomly
                : EnemyTankType.MovingByAlgorithm;
        }

        public void DownloadLevel(int levelNumber)
        {
            _timerWin = 50;

            Clear();
            _currentLevel = levelNumber;

            LevelState = LevelState.Download;
            var levelName = _currentLevel < 10 ? $"0{_currentLevel}" : _currentLevel.ToString();
            var path = Constants.Path.LevelMaps + levelName;
            var linesTileMap = File.ReadAllLines(path);
            _tanksFromMap = linesTileMap[26];

            for (var i = 1; i <= 10; i++)
            {
                ListInformation.Add(new TankInformation(new Point(27 * Constants.Size.WidthTile, i * Constants.Size.HeightTile)));
                ListInformation.Add(new TankInformation(new Point(28 * Constants.Size.WidthTile, i * Constants.Size.HeightTile)));
            }

            ListInformation.Add(new LevelInformation(_currentLevel, new Point(28 * Constants.Size.WidthTile, 23 * Constants.Size.HeightTile)));
            ListInformation.Add(new LivesInformation(3));
            ListInformation.Add(AlgorithmInformation);

            int x = 0, y = 0;
            foreach (var line in linesTileMap)
            {
                foreach (var c in line)
                {
                    switch (c)
                    {
                        case Constants.CharValue.CharBrickWall:
                            _listWall.Add(new BrickWall(new Point(x, y)));
                            break;
                        case Constants.CharValue.CharConcreteWall:
                            _listWall.Add(new ConcreteWall(new Point(x, y)));
                            break;
                        case Constants.CharValue.CharWater:
                            _listWater.Add(new Water(new Point(x, y)));
                            break;
                        case Constants.CharValue.CharForest:
                            _listOther.Add(new Forest(new Point(x, y)));
                            break;
                        case Constants.CharValue.CharIce:
                            _listIce.Add(new Ice(new Point(x, y)));
                            break;
                    }
                    x += Constants.Size.WidthTile;
                }
                x = 0;
                y += Constants.Size.HeightTile;
            }

            _currentLevelInformation = new CurrentLevelInformation(_currentLevel);

            _eagle = new Eagle(_spawnEagle);
            _listPlayer.Add(_eagle);
        }

        public void Draw()
        {
            StartGame.GameWindow.Invalidate(StartGame.GameWindow.ClientRectangle);
        }

        public static void StopTimer()
        {
            Stopwatch.Stop();
        }

        public static void StartTimer()
        {
            Stopwatch.Restart();
        }


        public void Update()
        {
            if (LevelState == LevelState.Download)
            {
                if (!_currentLevelInformation.IsShow())
                {
                    ListInformation.Remove(_currentLevelInformation);

                    var rect = new Rectangle(_spawnPlayer.X, _spawnPlayer.Y, Constants.Size.HeightTank, Constants.Size.WidthTank);

                    Player = new PlayerTank(rect, 5, Direction.Up, 8);
                    new TankAppearance(_spawnPlayer, Player);

                    _enemyTanks = new SpawnTanks(_tanksFromMap);
                    _enemyTanks.AddEnemy();

                    LevelState = LevelState.Game;
                }
                else _currentLevelInformation.Update();
            }
            if (LevelState != LevelState.GameOver)
            {
                foreach (var player in _listPlayer)
                {
                    player.Update();
                }
            }
            for (var i = 0; i < _listOther.Count; i++)
            {
                _listOther[i].Update();
            }
            for (var i = 0; i < _listRandomWall.Count; i++)
            {
                _listRandomWall[i].Update();
            }
            foreach (var item in _listWater)
            {
                item.Update();
            }
            for (var i = 0; i < _listTankEnemy.Count; i++)
            {
                _listTankEnemy[i].Update();
            }
            for (var i = 0; i < _listShell.Count; i++)
            {
                _listShell[i].Update();
            }

            switch (LevelState)
            {
                case LevelState.GameOver when _gameOverInformation == null:
                    var loseResult = new GameResultModel(false, CurrentLevel.Stopwatch.Elapsed, CurrentLevel.Score, "BFS");
                    CsvFileWriter.AppendGameInfo(loseResult);
                    SoundService.Stop();
                    _gameOverInformation = new GameOverInformation();
                    ListInformation.Add(_gameOverInformation);
                    break;
                case LevelState.GameOver:
                    _gameOverInformation.Update();
                    break;
                case LevelState.Game when _listTankEnemy.Count == 0 && !_enemyTanks.IsTanks():
                {
                    if (_timerWin == 0)
                    {
                        SoundService.Stop();
                        var winResult = new GameResultModel(true, Stopwatch.Elapsed, Score, "BFS");
                        CsvFileWriter.AppendGameInfo(winResult);
                        TimerWin();
                    }
                    else _timerWin--;

                    break;
                }
                case LevelState.Game:
                {
                    if (_timerSpawnWall == 0)
                    {
                        _timerSpawnWall = Constants.Timer.CreateRandomWall;
                        var rnd = new Random();
                        var x = 40;
                        var y = 40;

                        while (!IsPointIsFreeToBuild(new Point(x, y)))
                        {
                            x = Constants.XPoints[rnd.Next(0, Constants.XPoints.Length - 1)];

                            y = rnd.Next(0, 10) * 40;
                        }

                        new RandomBrickWall(new Point(x, y));
                        new RandomBrickWall(new Point(x + 20, y));
                        new RandomBrickWall(new Point(x, y + 20));
                        new RandomBrickWall(new Point(x + 20, y + 20));
                    }
                    else _timerSpawnWall--;

                    #region Algorithm for Laba-1

                    //switch (_currentAlgorithm)
                    //    {
                    //        case AlgorithmType.Bfs:
                    //            {
                    //                Stopwatch.Restart();
                    //                foreach (var item in _listTankEnemy.Select(enemy => BfsSearcher.GetRoute(
                    //                    Player.Rect.Location,
                    //                    enemy.Rect.Location)).Where(path => path != null).SelectMany(path => path))
                    //                {
                    //                    new ColorPoint(item, "RedPoint");
                    //                }

                    //                Stopwatch.Stop();
                    //                AlgorithmInformation.ChangeTime(Stopwatch.ElapsedMilliseconds);
                    //                break;
                    //            }
                    //        case AlgorithmType.Dfs:
                    //            {
                    //                Stopwatch.Restart();
                    //                foreach (var item in _listTankEnemy.Select(enemy => DfsSearcher.GetRoute(
                    //                    Player.Rect.Location,
                    //                    enemy.Rect.Location)).Where(path => path != null).SelectMany(path => path))
                    //                {
                    //                    new ColorPoint(item, "GreenPoint");
                    //                }

                    //                Stopwatch.Stop();
                    //                AlgorithmInformation.ChangeTime(Stopwatch.ElapsedMilliseconds);
                    //                break;
                    //            }
                    //        case AlgorithmType.UniformCostSearch:
                    //            {
                    //                Stopwatch.Restart();
                    //                foreach (var item in _listTankEnemy.Select(enemy => UniformCostSearcher.GetRoute(
                    //                    Player.Rect.Location,
                    //                    enemy.Rect.Location)).Where(path => path != null).SelectMany(path => path))
                    //                {
                    //                    new ColorPoint(item, "YellowPoint");
                    //                }

                    //                Stopwatch.Stop();
                    //                AlgorithmInformation.ChangeTime(Stopwatch.ElapsedMilliseconds);
                    //                break;
                    //            }
                    //        default:
                    //            throw new ArgumentOutOfRangeException();
                    //    }

                    #endregion

                    if (_listTankEnemy.Count < 3)
                    {
                        _enemyTanks.AddEnemy();
                    }

                    break;
                }
                case LevelState.Download:
                    break;
                case LevelState.Win:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public static bool IsPointEmpty(Point point)
        {
            var enums = Enum.GetValues(typeof(MapItemKey)).Cast<MapItemKey>()
                .Where(type => type != MapItemKey.Other
                               && type != MapItemKey.Player
                               && type != MapItemKey.Ice);

            if (point.X > Constants.Size.WidthBoard - Constants.Size.WidthTank
                || point.Y > Constants.Size.HeightBoard - Constants.Size.HeightTank
                || point.X < 0
                || point.Y < 0)
            {
                return false;
            }

            foreach (var mapItemKey in enums)
            {
                foreach (var baseItem in DictionaryObjGame[mapItemKey])
                {
                    if ((point.X == baseItem.Rect.X || point.X + 20 == baseItem.Rect.X)
                        && (point.Y == baseItem.Rect.Y || point.Y + 20 == baseItem.Rect.Y)) return false;
                }
            }

            return true;
        }

        public static bool IsPointIsFreeToBuild(Point point)
        {
            var enums = Enum.GetValues(typeof(MapItemKey)).Cast<MapItemKey>();

            if (point.X > Constants.Size.WidthBoard - Constants.Size.WidthTank
                || point.Y > Constants.Size.HeightBoard - Constants.Size.HeightTank
                || point.X < 0
                || point.Y < 0)
            {
                return false;
            }

            return enums.All(mapItemKey => !DictionaryObjGame[mapItemKey]
                .Any(baseItem => point.X == baseItem.Rect.X && point.Y == baseItem.Rect.Y));
        }

        public static bool IsFire(Point point)
        {
            return DictionaryObjGame[MapItemKey.TankEnemy]
                .Any(enemy => enemy.Rect.X > point.X - 20 && enemy.Rect.X < point.X + 60 
                              || enemy.Rect.Y > point.Y - 20 && enemy.Rect.Y < point.Y + 60);
        }

        public static void ChangeAlgorithm()
        {
            _currentAlgorithm = _currentAlgorithm switch
            {
                AlgorithmType.Bfs => AlgorithmType.Dfs,
                AlgorithmType.Dfs => AlgorithmType.UniformCostSearch,
                AlgorithmType.UniformCostSearch => AlgorithmType.Bfs,
                _ => _currentAlgorithm
            };
            AlgorithmInformation.Change(_currentAlgorithm);
        }

        public void Clear()
        {
            _gameOverInformation = null;
            ListInformation.Clear();
            foreach (var list in DictionaryObjGame.Values)
            {
                list.Clear();
            }
        }
    }
}
