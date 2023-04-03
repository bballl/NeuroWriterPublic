using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace GameBoxProject
{
    public class SkinChanger : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private float _timeBetweenColors;

        [SerializeField] private Color _colors;

        private void OnEnable()
        {
            _spriteRenderer.color = Color.white;

            DOTween.Sequence()
                .Append(_spriteRenderer.DOColor(_colors, _timeBetweenColors))
                .AppendInterval(_timeBetweenColors)
                .Append(_spriteRenderer.DOColor(Color.white, _timeBetweenColors))
                .AppendInterval(_timeBetweenColors)
                .SetLoops(-1, LoopType.Yoyo)
                .SetLink(gameObject, LinkBehaviour.KillOnDisable);
        }
    }
}