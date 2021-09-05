using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace BattleCity.Interfaces
{
    interface ICollision
    {
        /// <summary>
        /// Location on the map, width + height
        /// </summary>
        Rectangle Rect { get; }
    }
}
