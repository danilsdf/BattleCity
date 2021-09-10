using System.Media;

namespace BattleCity.SoundPart
{
    public class SoundService
    {
        public static SoundPlayer MoveSound = new SoundPlayer(SoundPath.MovePath);
        private static bool _isMuted;
        public static bool IsMuted => _isMuted;
        private static bool _move;

        public static void SoundMove()
        {
            if (_move) return;
            if (_isMuted) return;

            StopSound.Stop();
            MoveSound.Play();
            _move = true;
            _stop = false;
        }

        public static SoundPlayer GameOverSound = new SoundPlayer(SoundPath.OverPath);
        public static void GameOver()
        {
            if (_isMuted) return;
            GameOverSound.Stop();
            GameOverSound.Play();
        }

        public static SoundPlayer GameStartSound = new SoundPlayer(SoundPath.StartPath);
        public static void GameStart()
        {
            if (_isMuted) return;
            GameStartSound.Stop();
            GameStartSound.Play();
        }

        public static SoundPlayer FireSound = new SoundPlayer(SoundPath.FirePath);
        public static void SoundFire()
        {
            if (_isMuted) return;
            FireSound.Stop();
            FireSound.Play();
        }

        public static SoundPlayer BigDetonationSound = new SoundPlayer(SoundPath.BigDetonationPath);
        public static void SoundBigDetonation()
        {
            if (_isMuted) return;
            BigDetonationSound.Stop();
            BigDetonationSound.Play();
        }

        public static SoundPlayer DetonationSound = new SoundPlayer(SoundPath.DetonationPath);
        public static void SoundDetonation()
        {
            if (_isMuted) return;
            DetonationSound.Stop();
            DetonationSound.Play();
        }

        public static SoundPlayer StopSound = new SoundPlayer(SoundPath.StopPath);
        private static bool _stop;


        public static void SoundStop()
        {
            if (_stop) return;

            if (_isMuted) return;
            MoveSound.Stop();
            StopSound.Play();
            _stop = true;
            _move = false;
        }

        public static void Mute()
        {
            _isMuted = !_isMuted;
            GameStartSound.Stop();
            FireSound.Stop();
            DetonationSound.Stop();
            BigDetonationSound.Stop();
            GameOverSound.Stop();
            MoveSound.Stop();
            StopSound.Stop();
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
