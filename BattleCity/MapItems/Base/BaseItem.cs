using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using BattleCity.Interfaces;

namespace BattleCity.MapItems.Base
{
    [Serializable]
    internal abstract class BaseItem : InformationGame, ICollision
    {
        protected BaseItem(Rectangle rect)
            : base(rect)
        { }

        public Rectangle Rect => SpriteRectangle;

        public virtual void Update()
        { }
    }
}
