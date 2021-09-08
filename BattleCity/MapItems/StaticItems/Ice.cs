using System.Drawing;
using BattleCity.MapItems.Base;

namespace BattleCity.MapItems.StaticItems
{
    public class Ice : BaseItem
    {
        public Ice(Point position): base(position)
        {
            SpriteImage = GetImage(GetType().Name);
        }
    }
}
