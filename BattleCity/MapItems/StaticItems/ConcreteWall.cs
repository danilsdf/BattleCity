using System.Drawing;
using BattleCity.Enums;
using BattleCity.Interfaces;
using BattleCity.MapItems.Base;
using BattleCity.MapItems.ShellModule;
using BattleCity.SoundPart;

namespace BattleCity.MapItems.StaticItems
{
    public class ConcreteWall : BaseItem, IResponse
    {
        public ConcreteWall(Point position) : base(position)
        {
            SpriteImage = GetImage(GetType().Name);
        }

        public void Response(Shell shell)
        {
            shell.Detonation = true;
            if (shell.TankOwner == MapItemKey.Player) SoundService.SoundDetonation();
        }
    }
}
