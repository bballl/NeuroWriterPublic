using Assets.Scripts.Units.Character;
using System;
using System.Collections;
using UnityEngine;

namespace GameBoxProject
{
    public class LevelController : MonoBehaviour
    {
        [SerializeField] private ExperienceHolder _experienceHolder;

        [SerializeField] private bool _haveMaxLevel;

        public static event Action OnGameLevelUp;
        public event Action OnMaxLevelCompleted;

        public int CurrentGameLevel { get; private set; }
        public int NeedToNextLevel => _neededExpToUpgrade;

        private LevelData _levelData;
        private Experience _exp;
        private int _neededExpToUpgrade;

        private bool _isReady = false;
        private bool _levelIsMax = false;

        private void Start() => 
            StartCoroutine(WaitCoroutine());


        private IEnumerator WaitCoroutine()
        {
            while(_isReady is false)
            {
                if (_experienceHolder == null)
                {
                    Debug.Log("Waiting for ExperienceHolder");
                    yield return new WaitForFixedUpdate();
                }
                else if (_experienceHolder.Experience == null)
                {
                    Debug.Log("Waiting for Experience in Holder");
                    yield return new WaitForFixedUpdate();
                }

                _isReady = true;
            }

            Construct();
        }



        private void Construct()
        {
            _levelData = SceneContent.Instance._levelData;
            _exp = _experienceHolder.Experience;

            CurrentGameLevel = 1;
            _neededExpToUpgrade = _levelData.LevelProgress[0].NeededExp;

            _exp.OnExperienceCountChanged += OnExperienceCountChanged;
        }

        private void OnDestroy()
        {
            _exp.OnExperienceCountChanged -= OnExperienceCountChanged;
        }

        private void UpgradeLevel()
        {
            if (CurrentGameLevel >= _levelData.LevelProgress.Count)
            {
                if (!_haveMaxLevel)
                    _neededExpToUpgrade *= 2;
                else
                {
                    _levelIsMax = true;
                    OnMaxLevelCompleted?.Invoke();
                }
            }
            else
                _neededExpToUpgrade = _levelData.LevelProgress[CurrentGameLevel].NeededExp;

            CurrentGameLevel++;
            CharacterAttributes.WeaponUpdate();
            OnGameLevelUp?.Invoke();
        }

        private void OnExperienceCountChanged(int currentExp)
        {
            if (_levelIsMax)
                return;

            if (currentExp >= _neededExpToUpgrade)
                UpgradeLevel();
        }
    }
}