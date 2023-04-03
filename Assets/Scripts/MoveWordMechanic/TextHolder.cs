using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace GameBoxProject
{
    class TextHolder : MonoBehaviour//, IDropHandler
    {
        //public static event Action<TextPlank> OnWordDropped;
        //public static event Action<TextPlank> OnWordDropError;

        //[SerializeField] private SelectedWordsHolder _wordHolder;

        //public void OnDrop(PointerEventData eventData)
        //{
        //    if (eventData.pointerDrag.TryGetComponent(out TextPlank textPlank))
        //    {
        //        if (_wordHolder.TryAddPlank(textPlank))
        //            OnWordDropped?.Invoke(textPlank);
        //        else
        //            OnWordDropError?.Invoke(textPlank);
        //    }
        //}
    }
}
