using System.Drawing;
using BattleCity.Shared;

namespace BattleCity.Information
{
    public class TankInformation : GameInformation
    {
        public TankInformation(Point position)
            : base(new Rectangle(position, new Size(Constants.Size.WidthTile, Constants.Size.HeightTile)))
        {
            SpriteImage = GetImage("InformationTank");
        }
    }
}
