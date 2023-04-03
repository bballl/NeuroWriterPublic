using System;
using UnityEngine;
using DG.Tweening;

namespace GameBoxProject
{
    internal abstract class DroppedItem : MonoBehaviour, IPoolObject
    {
        public event Action<IPoolObject> OnObjectNeededToDeactivate;

        [SerializeField] protected float _delayBeforeShow;
        [SerializeField] protected AudioClip _soundOnDrop;

        protected int _droppedValue;

        public void Activate(int value, Vector2 position)
        {
            transform.localScale = Vector3.zero;

            transform.position = position;
            _droppedValue = value;

            transform.DOScale(1f, _delayBeforeShow).SetLink(gameObject, LinkBehaviour.KillOnDestroy);
        }

        public void ResetBeforeBackToPool()
        {
            gameObject.SetActive(false);
        }

        protected void BackToPool() => 
            OnObjectNeededToDeactivate?.Invoke(this);
    }
}
