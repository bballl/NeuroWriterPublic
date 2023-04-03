using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GameBoxProject
{
    public class ExpBarUI : MonoBehaviour
    {
        [SerializeField] private Image _filler;
        [SerializeField] private TMP_Text _expText;

        [SerializeField] private LevelController _levelController;
        [SerializeField] private ExperienceHolder _experienceHolder;

        private Experience _exp;

        private int _expOnCurrentLevel = 0;
        private int _expToNextLevel = 0;

        private bool _isReady = false;
        private bool _expIsMax = false;

        private void Awake() => 
            StartCoroutine(WaitCoroutine());

        private IEnumerator WaitCoroutine()
        {
            while (_isReady is false)
            {
                if (_levelController == null)
                {
                    Debug.Log("Waiting for LevelController");
                    yield return new WaitForFixedUpdate();
                }
                else if (_experienceHolder == null)
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
            yield break;
        }

        private void Construct()
        {
            _exp = _experienceHolder.Experience;

            _exp.OnExperienceCountChanged += UpdateExp;
            LevelController.OnGameLevelUp += OnGameLevelUp;
            _levelController.OnMaxLevelCompleted += OnMaxLevelCompleted;

            _filler.fillAmount = 0;
            OnGameLevelUp();
        }

        private void OnMaxLevelCompleted()
        {
            _expIsMax = true;

            LevelController.OnGameLevelUp -= OnGameLevelUp;
            _exp.OnExperienceCountChanged -= UpdateExp;

            _exp.OnExperienceCountChanged += UpdateMaxExp;
        }

        private void UpdateMaxExp(int obj)
        {
            _filler.fillAmount = 1f;
            _expText.text = $"ÌÀÊÑ.ÓÐ.  {_exp.Current}";
        }

        private void OnGameLevelUp()
        {
            _expOnCurrentLevel = _expToNextLevel;
            _expToNextLevel = _levelController.NeedToNextLevel;
            UpdateExp(_exp.Current);
        }

        private void UpdateExp(int expValue)
        {
            _filler.fillAmount = (float)(_exp.Current - _expOnCurrentLevel) / (float)(_levelController.NeedToNextLevel - _expOnCurrentLevel);
            _expText.text = $"Óð.{_levelController.CurrentGameLevel}  {_exp.Current}/{_expToNextLevel}";
        }

        private void OnDestroy()
        {
            if (_expIsMax)
            {
                _levelController.OnMaxLevelCompleted -= OnMaxLevelCompleted;
                _exp.OnExperienceCountChanged -= UpdateMaxExp;
            }
            else
            {
                _exp.OnExperienceCountChanged -= UpdateExp;
                LevelController.OnGameLevelUp -= OnGameLevelUp;
            }
        }
    }
}