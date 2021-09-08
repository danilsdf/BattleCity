using System;
using System.Drawing;
using BattleCity.Information;
using BattleCity.Interfaces;
using BattleCity.Shared;

namespace BattleCity.MapItems.Base
{
    [Serializable]
    public abstract class BaseItem : GameInformation, ICollision
    {
        protected BaseItem(Rectangle rect)
            : base(rect)
        { }

        protected BaseItem(Point position)
            : base(new Rectangle(position.X, position.Y, Constants.Size.WidthTile, Constants.Size.HeightTile))
        { }

        public Rectangle Rect => SpriteRectangle;

        public virtual void Update()
        { }
    }
}
