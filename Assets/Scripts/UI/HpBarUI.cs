using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace GameBoxProject
{
    public class HpBarUI : MonoBehaviour
    {
        [SerializeField] private MainCharacterController _mainCharacterController;

        [SerializeField] private Image _mainImageGreen;
        [SerializeField] private Image _backImageYellow;

        private Health _health;
        private float _prevHealth;
        private Sequence seq;
        private bool _isReady;


        private void Awake() => 
            StartCoroutine(WaitRoutine());

        private IEnumerator WaitRoutine()
        {
            while (_isReady is false)
            {
                if (_mainCharacterController == null)
                {
                    Debug.Log($"{name} waiting for Character");
                    yield return new WaitForFixedUpdate();
                }
                else if (_mainCharacterController.Health == null)
                {
                    Debug.Log($"{name} waiting for Health in Character");
                    yield return new WaitForFixedUpdate();
                }

                _isReady = true;
            }

            Construct();
            yield break;
        }

        private void Construct()
        {
            _health = _mainCharacterController.Health;
            _prevHealth = _health.Hp;

            _health.OnHealthChanged += OnHealthChanged;

            _mainImageGreen.fillAmount = 1;
            _backImageYellow.fillAmount = 1;
        }

        private void OnHealthChanged(object sender, float currentHp, float maxHp)
        {
            float newValue = _health.Hp / _health.MaxHP;
            _mainImageGreen.fillAmount = newValue;

            seq = DOTween.Sequence();
            seq.AppendInterval(1f)
                .Append(_backImageYellow.DOFillAmount(newValue, 1f))
                .AppendCallback(() => _prevHealth = _health.Hp)
                .SetLink(gameObject, LinkBehaviour.KillOnDestroy);
        }

        private void OnDestroy()
        {
            _health.OnHealthChanged -= OnHealthChanged;
        }
    }
}