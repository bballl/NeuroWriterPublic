using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;

namespace GameBoxProject
{
    public class ChooseBuffPanel : MonoBehaviour
    {
        public static event Action OnBuffIconShowed;

        [SerializeField] private BuffChooseIcon[] _buffIcons;

        [SerializeField] private TMP_Text _description;

        private Action<Buff> _chooseBuff;

        public void Show()
        {
            foreach (var icon in _buffIcons)
            {
                icon.transform.localScale = Vector3.zero;
            }

            gameObject.SetActive(true);

            var seq = DOTween.Sequence();

            seq.AppendInterval(0.7f)
                .AppendCallback(() => OnBuffIconShowed?.Invoke())
                .Append(_buffIcons[0].transform.DOScale(Vector3.one, 0.3f))
                .AppendCallback(() => OnBuffIconShowed?.Invoke())
                .Append(_buffIcons[1].transform.DOScale(Vector3.one, 0.3f))
                .AppendCallback(() => OnBuffIconShowed?.Invoke())
                .Append(_buffIcons[2].transform.DOScale(Vector3.one, 0.3f))
                .SetLink(gameObject, LinkBehaviour.KillOnDisable);

            seq.SetUpdate(true);
            seq.Play();    
        }

        public void Hide() =>
            gameObject.SetActive(false);

        private void Start()
        {
            foreach (var icon in _buffIcons)
            {
                icon.OnBuffChoosen += Icon_OnBuffChoosen;
            }
        }

        private void Icon_OnBuffChoosen(Buff buff)
        {
            _chooseBuff?.Invoke(buff);
            _chooseBuff = null;
        }

        public void Activate(List<Buff> buffs, Action<Buff> callback)
        {
            _description.text = string.Empty;
            _chooseBuff += callback;

            for (int i = 0; i < _buffIcons.Length; i++)
            {
                if (i < buffs.Count)
                {
                    _buffIcons[i].Activate(buffs[i], _description);
                }
                else
                {
                    _buffIcons[i].gameObject.SetActive(false);
                }
            }
        }

        private void OnDestroy()
        {
            foreach (var icon in _buffIcons)
            {
                icon.OnBuffChoosen -= Icon_OnBuffChoosen;
            }
        }
    }
}