using System;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

namespace GameBoxProject
{
    class WordMover : MonoBehaviour, 
        IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
    {
        public static event Action OnWordBeginDrag;
        public static event Action OnWordsChanged;
        public static event Action OnWordEndDrag;


        [SerializeField] private CanvasGroup _canvasGroup;

        private Vector2 _offset;
        private Vector2 _startPosition;
        private Vector2 _startScale;
        
        public Transform ParentAfterDrag { get; private set; }
        public int ChildIndex { get; private set; }

        public void OnBeginDrag(PointerEventData eventData)
        {
            OnWordBeginDrag?.Invoke();

            _startPosition = transform.position;
            _startScale = transform.localScale;

            ParentAfterDrag = transform.parent;
            ChildIndex = transform.GetSiblingIndex();

            transform.SetParent(transform.root);
            transform.SetAsLastSibling();

            _offset = (Vector2)transform.position - eventData.position;
            _canvasGroup.blocksRaycasts = false;

            transform.localScale = 1.2f * _startScale;
            _canvasGroup.alpha = 0.7f;
            
            //_canvasGroup.DOFade(0.7f, 0.5f)
            //    .SetLink(gameObject, LinkBehaviour.KillOnDisable);
            //transform.DOScale(1.2f * _startScale, 0.3f)
            //    .SetEase(Ease.OutBack)
            //    .SetLink(gameObject, LinkBehaviour.KillOnDisable);
        }

        public void OnDrag(PointerEventData eventData)
        {
            transform.position = eventData.position + _offset;
        }

        public void MoveToParent(Transform parent, int index)
        {
            ParentAfterDrag = parent;
            ChildIndex = index;
        }

        public void OnDrop(PointerEventData eventData)
        {
            //Debug.Log($"OnDROP on {name} gameobject");

            //if (eventData.pointerDrag.TryGetComponent(out TextPlank dragTextPlank))
            //{
            //    if (gameObject.TryGetComponent(out TextPlank thisTextPlank))
            //    {
            //        if (dragTextPlank.WordType != thisTextPlank.WordType)
            //            return;

            //        var dragWordMover = dragTextPlank.gameObject.GetComponent<WordMover>();

            //        var tempParent = dragWordMover.ParentAfterDrag;
            //        int tempIndex = dragWordMover.ChildIndex;

            //        dragWordMover.MoveToParent(ParentAfterDrag, ChildIndex);

            //        transform.SetParent(tempParent);
            //        transform.SetSiblingIndex(tempIndex);

            //        dragTextPlank.SetSecretPlank(thisTextPlank.SecretWordPlank);
            //        thisTextPlank.ResetSecretPlank();

            //        OnWordsChanged?.Invoke();
            //    }
            //}
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            Debug.Log($"OnEndDrag on {name} gameobject");

            //transform.position = _startPosition;
            _canvasGroup.blocksRaycasts = true;

            transform.SetParent(ParentAfterDrag);
            transform.SetSiblingIndex(ChildIndex);

            //_canvasGroup.DOFade(1f, 0.3f)
            //    .SetLink(gameObject, LinkBehaviour.KillOnDisable);

            _canvasGroup.alpha = 1f;
            transform.localScale = _startScale;

            OnWordEndDrag?.Invoke();
        }
    }
}
