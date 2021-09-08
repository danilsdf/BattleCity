using System.Drawing;
using BattleCity.MapItems.Base;

namespace BattleCity.MapItems.StaticItems
{
    public class Water : BaseItem
    {
        public Water(Point position) : base(position)
        {
            SpriteImage = GetImage($"{GetType().Name}_1");
        }
    }
}
