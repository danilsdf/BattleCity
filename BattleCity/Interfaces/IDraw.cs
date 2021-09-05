using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace BattleCity.Interfaces
{
    interface IDraw
    {
        /// <summary>
        /// Paint object on the map
        /// </summary>
        void Draw(Graphics g, Point offset);
    }
}
