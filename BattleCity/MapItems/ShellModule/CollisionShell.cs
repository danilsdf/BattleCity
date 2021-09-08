using System.Drawing;
using BattleCity.Enums;
using BattleCity.Game;
using BattleCity.Interfaces;
using BattleCity.SoundPart;

namespace BattleCity.MapItems.ShellModule
{
    public class CollisionShell : Shell, IResponse
    {
        private int _delay;
        public CollisionShell(Rectangle rect, int speed, Direction direction, MapItemKey tankOwner)
            : base(rect, speed, direction, tankOwner)
        {
            _delay = 0;
        }

        public override void Update()
        {
            if (!Detonation)
            {
                Move();
                Detonation = CheckGoingBeyondBoundaries();
                if (!Detonation)
                    CheckCollisionWithMapItem();
                else if (TankOwner == MapItemKey.Player)
                    SoundService.SoundDetonation();

            }
            else if (Detonation && _delay == 0)
            {
                var detonationShell = new DetonationShell(SpriteRectangle.Location, Direction);
                _delay++;
            }
            else if (_delay == 7)
            {
                IsAlive = false;

                CurrentLevel.DictionaryObjGame[MapItemKey.Shell].Remove(this);
            }
            else if (_delay > 0)
                _delay++;
        }

        protected virtual void CheckCollisionWithMapItem()
        {
            foreach (var keyValue in CurrentLevel.DictionaryObjGame)
            {
                for (var i = 0; i < keyValue.Value.Count; i++)
                {
                    if (keyValue.Value[i] is Shell)
                    {
                        var shell = (Shell)keyValue.Value[i];
                        if (shell == this) continue;

                        if (shell.TankOwner == TankOwner) continue;
                    }

                    if (keyValue.Value[i] is IResponse iResponse)
                    {
                        if (!SpriteRectangle.IntersectsWith(keyValue.Value[i].Rect)) continue;

                        iResponse.Response(this);
                        return;
                    }
                }
            }
        }

        public void Response(Shell shell)
        {
            if (!IsAlive) return;

            IsAlive = false;
            CurrentLevel.DictionaryObjGame[MapItemKey.Shell].Remove(this);

            shell.IsAlive = false;
            CurrentLevel.DictionaryObjGame[MapItemKey.Shell].Remove(shell);
        }
    }
}
