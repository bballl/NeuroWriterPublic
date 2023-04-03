using Assets.Scripts.Units.Character;
using Assets.Scripts.Units.Character.Ability;
using System;
using UnityEngine;

namespace GameBoxProject
{
    public class BuffSounds : MonoBehaviour
    {
        [SerializeField] private AudioSource _audioSource;

        [Header("Sounds")]
        [SerializeField] private AudioClip _buffActivation;
        [SerializeField] private AudioClip _buffChoose;
        [SerializeField] private AudioClip _levelUp;
        [SerializeField] private AudioClip _buffShowed;

        private void Start()
        {
            Observer.ActivationAbilityEvent += PlayBuffActivationSound;
            BuffSystem.OnBuffSelected += PlayChooseBuffSound;
            LevelController.OnGameLevelUp += PlayLevelUpSound;
            ChooseBuffPanel.OnBuffIconShowed += PlayBuffShowed;
        }

        private void PlayBuffShowed() => 
            _audioSource.PlayOneShot(_buffShowed);

        private void PlayLevelUpSound() => 
            _audioSource.PlayOneShot(_levelUp);

        private void PlayChooseBuffSound(Buff obj) => 
            _audioSource.PlayOneShot(_buffChoose);

        private void PlayBuffActivationSound(AbilityType obj) => 
            _audioSource.PlayOneShot(_buffActivation);

        private void OnDestroy()
        {
            Observer.ActivationAbilityEvent -= PlayBuffActivationSound;
            BuffSystem.OnBuffSelected -= PlayChooseBuffSound;
            LevelController.OnGameLevelUp -= PlayLevelUpSound;
            ChooseBuffPanel.OnBuffIconShowed -= PlayBuffShowed;
        }
    }
}