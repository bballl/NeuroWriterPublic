using UnityEngine;

namespace Assets.Scripts.Sounds
{
    internal sealed class PencilSoundController
    {
        private AudioSource _hit;
        private AudioSource _whoosh;

        internal PencilSoundController(Transform pencilTransform)
        {
            //_hitClip = Resources.Load<AudioClip>("PencilHitAudioClip");
            //_whooshClip = Resources.Load<AudioClip>("PencilWhooshAudioClip");
            //_hit = pencil.AddComponent<AudioSource>();
            //_hit.clip = _hitClip;
            //_whoosh = pencil.AddComponent<AudioSource>();
            //_whoosh.clip = _whooshClip;

            _hit = Resources.Load<AudioSource>("PencilHitSound");
            _whoosh = Resources.Load<AudioSource>("PencilWhooshSound");

            _hit = Object.Instantiate(_hit, pencilTransform);
            _whoosh = Object.Instantiate(_whoosh, pencilTransform);
        }

        public void Hit()
        {
            _hit.Play();
        }

        public void Whoosh()
        {
            _whoosh.Play();
        }
    }
}
