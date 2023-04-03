using UnityEngine;
using DG.Tweening;
using System;
using System.Collections.Generic;
using Random = UnityEngine.Random;

namespace GameBoxProject
{
    class BuffSystem : MonoBehaviour
    {
        public static Action<Buff> OnBuffSelected;

        [SerializeField] private List<Buff> _buffs;
        [SerializeField] private BuffPanel _buffPanel;
        [SerializeField] private ChooseBuffPanel _chooseBuffPanel;
        [SerializeField] private int _buffCapacity = 4;

        private List<Buff> _buffsInGame = new();

        private void Start()
        {
            LevelController.OnGameLevelUp += ShowNewBuffs;

            DOTween.Sequence()
                .AppendCallback(() => _chooseBuffPanel.Hide())
                .AppendInterval(1f)
                .AppendCallback(() => ShowNewBuffs());
        }

        private void ShowNewBuffs()
        {
            var availableBuffs = GetAvailableBuffs();

            if (availableBuffs.Count == 0)
                return;

            int buffsCount = Mathf.Clamp(availableBuffs.Count, 1, 3);
            List<Buff> buffsToShow = new();

            for (int i = 0; i < buffsCount; i++)
            {
                var randBuff = availableBuffs[Random.Range(0, availableBuffs.Count)];
                buffsToShow.Add(randBuff);
                availableBuffs.Remove(randBuff);
            }

            availableBuffs.Clear();

            _chooseBuffPanel.Activate(buffsToShow, ChooseBuff);
            _chooseBuffPanel.Show();

            Time.timeScale = 0;
        }

        private void ChooseBuff(Buff buff)
        {
            if (!_buffsInGame.Contains(buff))
            {
                _buffsInGame.Add(buff);
                buff.Activate();
            }
            else
                buff.Upgrade();

            _buffPanel.AddBuff(buff);

            _chooseBuffPanel.Hide();
            Time.timeScale = 1;

            OnBuffSelected?.Invoke(buff);
        }

        private List<Buff> GetAvailableBuffs()
        {
            var buffsCanAdd = _buffs.FindAll(x => CanAddBuff(x));
            return buffsCanAdd;
        }

        private bool CanAddBuff(Buff buff)
        {
            if (_buffsInGame.Contains(buff))
            {
                if (buff.CanUpgrade())
                    return true;
                else
                    return false;
            }
            else if (_buffsInGame.Count < _buffCapacity)
                return true;
            else
                return false;
        }

        private void OnDestroy()
        {
            LevelController.OnGameLevelUp -= ShowNewBuffs;
        }
    }
}
