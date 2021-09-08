using System.Drawing;
using BattleCity.Shared;

namespace BattleCity.Information
{
    public class LivesInformation : GameInformation
    {
        public LivesInformation(int lives)
            : base(new Rectangle(28 * Constants.Size.WidthTile, 16 * Constants.Size.HeightTile, Constants.Size.WidthTile, Constants.Size.HeightTile))
        {
            SpriteImage = GetImage($"0{lives}");
        }
    }
}
