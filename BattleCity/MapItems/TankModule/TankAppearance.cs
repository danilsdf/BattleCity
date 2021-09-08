using System.Drawing;
using BattleCity.Enums;
using BattleCity.Game;
using BattleCity.Interfaces;
using BattleCity.MapItems.Base;
using BattleCity.Shared;

namespace BattleCity.MapItems.TankModule
{
    class TankAppearance : BaseItem
    {
        private int _size;
        private int _cycles;
        private readonly IAddTank _tank;
        public TankAppearance(Point position, IAddTank tank)
            : base(new Rectangle(position.X, position.Y, Constants.Size.WidthTank, Constants.Size.HeightTank))
        {
            _tank = tank;
            _size = 1;
            _cycles = 0;

            SpriteImage = GetImage($"Star{_size}");

            CurrentLevel.DictionaryObjGame[MapItemKey.TankEnemy].Add(this);
        }

        public override void Update()
        {
            if (_cycles % 4 == 0)
            {
                if (_size == 3) _size = 1;
                else _size++;
            }

            SpriteImage = GetImage($"Star{_size}");
            _cycles++;

            if (_cycles != 36) return;
            
            CurrentLevel.DictionaryObjGame[MapItemKey.TankEnemy].Remove(this);
            _tank.AddTank();
        }
    }
}
