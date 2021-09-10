using System.Drawing;
using BattleCity.Enums;
using BattleCity.MapItems.ShellModule;
using BattleCity.Shared;
using BattleCity.SoundPart;

namespace BattleCity.MapItems.TankModule
{
    public abstract class FireTank : CollisionTank
    {
        protected int ShellSpeed;
        protected Shell Shell;

        protected FireTank(Rectangle rect, int velocity, Direction direction, int shellSpeed)
            : base(rect, velocity, direction)
        {
            ShellSpeed = shellSpeed;
        }

        protected void Fire(MapItemKey tankOwner)
        {
            if (Shell != null && Shell.IsAlive) return;

            if (tankOwner == MapItemKey.Player)
            {
                SoundService.SoundFire();
            }

            Shell = CreateShellByDirection(tankOwner);
        }

        private Shell CreateShellByDirection(MapItemKey tankOwner)
        {
            return Direction switch
            {
                Direction.Up => new CollisionShell(
                    new Rectangle(SpriteRectangle.Left + (SpriteRectangle.Width / 2 - Constants.Size.WidthShell / 2),
                            SpriteRectangle.Top - Constants.Size.HeightShell,
                            Constants.Size.WidthShell,
                            Constants.Size.HeightShell),
                    ShellSpeed,
                    Direction,
                    tankOwner),
                Direction.Right => new CollisionShell(
                    new Rectangle(SpriteRectangle.Right + 1,
                            SpriteRectangle.Top + (SpriteRectangle.Height / 2 - Constants.Size.HeightShell / 2),
                            Constants.Size.HeightShell,
                            Constants.Size.WidthShell),
                    ShellSpeed,
                    Direction,
                    tankOwner),
                Direction.Down => new CollisionShell(
                    new Rectangle(SpriteRectangle.Left + (SpriteRectangle.Width / 2 - Constants.Size.WidthShell / 2),
                        SpriteRectangle.Bottom,
                        Constants.Size.WidthShell,
                        Constants.Size.HeightShell),
                    ShellSpeed,
                    Direction,
                    tankOwner),
                Direction.Left => new CollisionShell(
                    new Rectangle(SpriteRectangle.Left - Constants.Size.WidthShell,
                        SpriteRectangle.Top + (SpriteRectangle.Height / 2 - Constants.Size.HeightShell / 2),
                        Constants.Size.HeightShell, Constants.Size.WidthShell),
                    ShellSpeed,
                    Direction,
                    tankOwner),
                _ => Shell
            };
        }
    }
}