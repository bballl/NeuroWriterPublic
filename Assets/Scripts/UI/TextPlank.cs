using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GameBoxProject
{
    public class TextPlank : MonoBehaviour
    {
        [SerializeField] private Image _backImage;
        [SerializeField] private TMP_Text _text;
        [SerializeField] private int _pxForSymbol;
        [SerializeField] private RectTransform _rectTransform;

        public SecretWordHolder SecretWordPlank { get; private set; }
        private string _word;

        public WordType WordType { get; private set; }

        internal void Init(Color color, string word, WordType wordType, bool show = true)
        {
            gameObject.name = word;

            _word = word;
            WordType = wordType;
            _text.color = color;
            //_backImage.color = color;

            float width = _pxForSymbol * word.Length;

            if (show)
            {
                _rectTransform.sizeDelta = new Vector2(width, _rectTransform.sizeDelta.y);
                _text.text = word.ToUpper();
            }
            else
            {
                _rectTransform.sizeDelta = new Vector2(300f, _rectTransform.sizeDelta.y);
                _text.text = "";
            }
        }

        public void SetSecretPlank(SecretWordHolder secretWordHolder)
        {
            if (secretWordHolder != null)
            {
                SecretWordPlank = secretWordHolder;
                SecretWordPlank.SetTempWord(_word);
            }
        }

        public void ResetSecretPlank()
        {
            SecretWordPlank = null;
        }

        public void ShowSecretPlank()
        {
            if (SecretWordPlank != null)
            {
                SecretWordPlank.OnPlankDragged(this);
                SecretWordPlank.SetTempWord("");
                SecretWordPlank = null;
            }
        }

        public override string ToString()
        {
            return _word;
        }
    }
}