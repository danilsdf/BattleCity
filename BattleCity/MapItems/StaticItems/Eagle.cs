using System.Drawing;
using BattleCity.Enums;
using BattleCity.Game;
using BattleCity.Interfaces;
using BattleCity.MapItems.Base;
using BattleCity.MapItems.ShellModule;
using BattleCity.Shared;

namespace BattleCity.MapItems.StaticItems
{
    public class Eagle : BaseItem, IResponse
    {
        private bool _isAlive;

        public Eagle(Point spawnPoint)
            : base(new Rectangle(spawnPoint.X, spawnPoint.Y, Constants.Size.WidthTank, Constants.Size.HeightTank))
        {
            SpriteImage = GetImage(GetType().Name);
            _isAlive = true;
        }

        public void Response(Shell shell)
        {
            if (!_isAlive) return;

            _isAlive = false;
            SpriteImage = GetImage($"{GetType().Name}2");
            shell.Detonation = true;
            var detonationShellBig = new DetonationShellBig(shell.Rect.Location, shell.Direction, 0);
            CurrentLevel.LevelState = LevelState.GameOver;
        }
    }
}
