using Assets.Scripts.Units.Character;
using Assets.Scripts.Units.Character.Ability;
using TMPro;
using UnityEngine;
using DG.Tweening;

namespace GameBoxProject
{
    public class BuffTimerUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text _timerText;
        [SerializeField] private AbilityData _abilityData;
        [SerializeField] private AbilityType _type;

        private RectTransform _rectTransform;
        private float _timer = 0f;
        private bool _activated;

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
        }

        private void Start()
        {
            Observer.ActivationAbilityEvent += Activate;
            Observer.DeactivationAbilityEvent += Deactivate;
        }

        private void Deactivate(AbilityType type)
        {
            if (type != _type)
                return;

            Hide();
            _activated = false;
        }

        public void Activate(AbilityType type)
        {
            if (type != _type)
                return;

            _activated = true;
            _timer = AbilityDuration(type);
            _timerText.text = $"{_timer}";
            Show();
        }

        private void Show()
        {
            _rectTransform.DOAnchorPosY(110, 1f)
                .SetEase(Ease.OutElastic)
                .SetLink(gameObject, LinkBehaviour.KillOnDestroy);
        }

        private void Hide()
        {
            _rectTransform.DOAnchorPosY(55, 1f)
                .SetEase(Ease.OutBounce)
                .SetLink(gameObject, LinkBehaviour.KillOnDestroy);
        }

        private float AbilityDuration(AbilityType type)
        {
            switch (type)
            {
                case AbilityType.DroneAbility:
                    return _abilityData.DroneAbilityDuration;
                case AbilityType.ExtraDamageAbility:
                    return _abilityData.ExtraDamageAbilityDuration;
                case AbilityType.InvulnerAbility:
                    return _abilityData.InvulnerAbilityDuration;
            }
            return 0f;
        }

        private void FixedUpdate()
        {
            if (_activated)
            {
                _timer -= Time.fixedDeltaTime;
                _timerText.text = $"{(int)_timer}";

                if (_timer <= 0)
                    Deactivate(_type);
            }
        }

        private void OnDestroy()
        {
            Observer.ActivationAbilityEvent -= Activate;
            Observer.DeactivationAbilityEvent -= Deactivate;
        }
    }
}