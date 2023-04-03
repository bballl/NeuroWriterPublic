using UnityEngine;

namespace Assets.Scripts.Sounds
{
    internal sealed class CharacterSoundController
    {
        private AudioSource _takingDamage;

        public CharacterSoundController(Transform characterTransform)
        {
            _takingDamage = Resources.Load<AudioSource>("CharacterTakingDamageSound");
            _takingDamage = Object.Instantiate(_takingDamage, characterTransform);
        }

        public void TakingDamage()
        {
            _takingDamage.Play();
        }
    }
}
