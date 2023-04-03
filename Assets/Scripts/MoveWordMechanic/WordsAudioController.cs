using UnityEngine;

namespace GameBoxProject
{
    class WordsAudioController : MonoBehaviour
    {
        [SerializeField] private AudioSource _audio;

        [SerializeField] private AudioClip _wordDrag;
        [SerializeField] private AudioClip _shotWordDrop;
        [SerializeField] private AudioClip _longWordDrop;
        [SerializeField] private AudioClip _errorOnDrop;
        [SerializeField] private AudioClip _wordsChanged;

        private void Start()
        {
            WordMover.OnWordsChanged += PlayWordChanged;
            WordMover.OnWordBeginDrag += PlayWordBeginDrag;
            SecretWordHolder.OnWordDroppedToSecret += TextHolder_OnWordDropped;
        }

        private void TextHolder_OnWordDropped(TextPlank text)
        {
            int wordLength = text.ToString().Length;

            if (wordLength >= 6)
                _audio.PlayOneShot(_longWordDrop);
            else
                _audio.PlayOneShot(_shotWordDrop);
        }

        private void PlayWordChanged() =>
            _audio.PlayOneShot(_wordsChanged);

        private void PlayWordBeginDrag() =>
            _audio.PlayOneShot(_wordDrag);

        private void OnDestroy()
        {
            WordMover.OnWordsChanged -= PlayWordChanged;
            WordMover.OnWordBeginDrag -= PlayWordBeginDrag;
            SecretWordHolder.OnWordDroppedToSecret -= TextHolder_OnWordDropped;
        }
    }
}
