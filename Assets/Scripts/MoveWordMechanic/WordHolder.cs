using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

namespace GameBoxProject
{
    public class WordHolder : MonoBehaviour, IDropHandler
    {
        public event Action<List<string>> OnWordDropped;
        [field: SerializeField] public WordType Type { get; private set; }

        private List<string> _wordsInHolder = new();

        public void Refresh()
        {
            _wordsInHolder.Clear();
            for (int i = 0; i < transform.childCount; i++)
            {
                if (transform.GetChild(i).TryGetComponent(out TextPlank textPlank))
                    _wordsInHolder.Add(textPlank.ToString());
            }
            OnWordDropped?.Invoke(_wordsInHolder);
        }

        internal void AddPlank(TextPlank textPlank)
        {
            if (Type.Equals(WordType.any) || textPlank.WordType.Equals(Type))
            {
                textPlank.transform.SetParent(transform);
                textPlank.ShowSecretPlank();
                textPlank.gameObject.GetComponent<WordMover>().MoveToParent(transform, 0);
            }

            Refresh();
        }

        public void BackWords()
        {
            var holders = FindObjectsOfType<WordHolder>().ToList();

            while (transform.childCount > 0)
            {
                var plank = transform.GetChild(0).GetComponent<TextPlank>();
                var type = plank.WordType;

                var holder = holders.Find(x => x.Type == type);
                plank.transform.SetParent(holder.transform);
            }

            Refresh();
        }

        public void OnDrop(PointerEventData eventData)
        {
            if (eventData.pointerDrag.TryGetComponent(out TextPlank textPlank))
            {
                AddPlank(textPlank);
            }
        }
    }
}