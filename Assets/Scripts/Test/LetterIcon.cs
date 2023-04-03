using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GameBoxProject
{
    public class LetterIcon : MonoBehaviour
    {
        [SerializeField] private Image _sprite;
        [SerializeField] private TMP_Text _text;

        public bool IsHiden { get; private set; }

        private void Start()
        {
            IsHiden = true;
            _sprite.enabled = true;
            _text.enabled = false;  
        }

        public void Show(char letter)
        {
            _sprite.enabled = false;
            _text.text = letter.ToString().ToUpper();
            _text.enabled = true;
            IsHiden = false;
        }
    }
}