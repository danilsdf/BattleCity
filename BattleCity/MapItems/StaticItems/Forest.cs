using System.Drawing;
using BattleCity.MapItems.Base;

namespace BattleCity.MapItems.StaticItems
{
    public class Forest : BaseItem
    {
        public Forest(Point position) : base(position)
        {
            SpriteImage = GetImage(GetType().Name);
        }
    }
}
