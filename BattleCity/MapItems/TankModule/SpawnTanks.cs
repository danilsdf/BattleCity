using System.Drawing;
using System.Linq;
using BattleCity.Enums;
using BattleCity.Game;
using BattleCity.MapItems.TankModule.Enemy;
using BattleCity.Shared;

namespace BattleCity.MapItems.TankModule
{
    internal class SpawnTanks
    {
        private int _currentlyUsed;
        private readonly Point _spawnFirstEnemy;
        private readonly Point _spawnSecondEnemy;
        private readonly Point _spawnThirdEnemy;
        private int _countEnemy;
        private Point _spawnPoint;

        private readonly string _tanksEnemy;
        private int _index;

        public SpawnTanks(string tanksEnemy)
        {
            this._tanksEnemy = tanksEnemy;
            _index = 0;

            _currentlyUsed = 1;
            _spawnFirstEnemy = new Point();
            _spawnSecondEnemy = new Point(12 * Constants.Size.WidthTile, 0);
            _spawnThirdEnemy = new Point(24 * Constants.Size.HeightTile, 0);
            _countEnemy = 20;
        }

        public void AddEnemy()
        {
            if (CurrentLevel.DictionaryObjGame[MapItemKey.TankEnemy].Count >= 3 || _countEnemy <= 0) return;

            if (_currentlyUsed == 3) _currentlyUsed = 1;
            else _currentlyUsed++;

            _spawnPoint = _currentlyUsed switch
            {
                1 => _spawnFirstEnemy,
                2 => _spawnSecondEnemy,
                _ => _spawnThirdEnemy
            };

            var rect = new Rectangle(_spawnPoint, new Size(Constants.Size.WidthTank, Constants.Size.HeightTank));
            foreach (var (key, value) in CurrentLevel.DictionaryObjGame)
            {
                if (key == MapItemKey.Wall) continue;
                if (value.Any(sprite => rect.IntersectsWith(sprite.Rect))) return;
            }

            switch (_tanksEnemy[_index])
            {
                case Constants.CharValue.CharSimpleTank:
                    new TankAppearance(_spawnPoint, new SimpleTank(_spawnPoint, Direction.Down));
                    break;
                case Constants.CharValue.CharFastTank:
                    new TankAppearance(_spawnPoint, new FastTank(_spawnPoint, Direction.Down));
                    break;
                case Constants.CharValue.CharPowerTank:
                    new TankAppearance(_spawnPoint, new PowerTank(_spawnPoint, Direction.Down));
                    break;
                case Constants.CharValue.CharArmoredTank:
                    new TankAppearance(_spawnPoint, new ArmoredTank(_spawnPoint, Direction.Down));
                    break;
            }
            _index++;

            CurrentLevel.ListInformation.RemoveAt(--_countEnemy);
        }

        public bool IsTanks() => _countEnemy > 0;
    }
}
