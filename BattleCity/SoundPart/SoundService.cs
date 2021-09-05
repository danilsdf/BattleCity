using System;
using System.Collections.Generic;
using System.Media;
using System.Text;

namespace BattleCity.SoundPart
{
    public class SoundService
    {
        public static SoundPlayer MoveSound = new SoundPlayer(SoundPath.MovePath);
        private static bool _move;
        public static void SoundMove()
        {
            if (_move) return;

            StopSound.Stop();
            MoveSound.Play();
            _move = true;
            _stop = false;
        }

        public static SoundPlayer GameOverSound = new SoundPlayer(SoundPath.OverPath);
        public static void GameOver()
        {
            GameOverSound.Stop();
            GameOverSound.Play();
        }

        public static SoundPlayer GameStartSound = new SoundPlayer(SoundPath.StartPath);
        public static void GameStart()
        {
            GameStartSound.Stop();
            GameStartSound.Play();
        }

        public static SoundPlayer FireSound = new SoundPlayer(SoundPath.FirePath);
        public static void SoundFire()
        {
            FireSound.Stop();
            FireSound.Play();
        }

        public static SoundPlayer BigDetonationSound = new SoundPlayer(SoundPath.BigDetonationPath);
        public static void SoundBigDetonation()
        {
            BigDetonationSound.Stop();
            BigDetonationSound.Play();
        }

        public static SoundPlayer DetonationSound = new SoundPlayer(SoundPath.DetonationPath);
        public static void SoundDetonation()
        {
            DetonationSound.Stop();
            DetonationSound.Play();
        }

        public static SoundPlayer StopSound = new SoundPlayer(SoundPath.StopPath);
        private static bool _stop;


        public static void SoundStop()
        {
            if (_stop) return;

            MoveSound.Stop();
            StopSound.Play();
            _stop = true;
            _move = false;
        }

        public static void Stop()
        {
            MoveSound.Stop();
            StopSound.Stop();
            _move = false;
            _stop = false;
        }
    }
}
