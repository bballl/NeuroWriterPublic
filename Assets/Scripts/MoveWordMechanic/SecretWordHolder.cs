using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace GameBoxProject
{
    public class SecretWordHolder : MonoBehaviour, IDropHandler
    {
        public static event Action<TextPlank> OnWordDroppedToSecret;

        private string _secretWord;
        private WordType _type;
        private string _tempSecret;

        public void Init(string word, WordType type)
        {
            _secretWord = word;
            _type = type;
        }

        public void OnPlankDragged(TextPlank textPlank)
        {
            gameObject.SetActive(true);
            OnWordDroppedToSecret?.Invoke(textPlank);
        }

        public void OnDrop(PointerEventData eventData)
        {
            if (eventData.pointerDrag.TryGetComponent(out TextPlank textPlank))
            {
                textPlank.ShowSecretPlank();
                int index = transform.GetSiblingIndex();
                gameObject.SetActive(false);
                textPlank.gameObject.GetComponent<WordMover>().MoveToParent(transform.parent, index);
                //textPlank.transform.SetSiblingIndex(index);
                textPlank.SetSecretPlank(this);
                //OnWordDroppedToSecret?.Invoke(textPlank);
            }
        }

        public void SetTempWord(string text)
        {
            _tempSecret = text;
        }

        public bool GetResult()
        {
            return _secretWord.Equals(_tempSecret);
        }
    }
}