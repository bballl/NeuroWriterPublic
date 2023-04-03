using UnityEngine;

namespace Assets.Scripts.Sounds
{
    internal class PenSoundController
    {
        private AudioSource _hit;
        private AudioSource _shot;

        internal PenSoundController(Transform penTransform)
        {
            _hit = Resources.Load<AudioSource>("PenHitSound");
            _shot = Resources.Load<AudioSource>("PenShotSound");

            _hit = UnityEngine.Object.Instantiate(_hit, penTransform);
            _shot = UnityEngine.Object.Instantiate(_shot, penTransform);
        }

        public void Hit()
        {
            _hit.Play();
        }

        public void Shot()
        {
            _shot.Play();
        }
    }
}
