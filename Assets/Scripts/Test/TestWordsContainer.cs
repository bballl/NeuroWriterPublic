using System.Collections.Generic;

namespace GameBoxProject
{
    public class TestWordsContainer : WordsContainer
    {
        private List<AllWords> _words = new();

        private void Start()
        {
            if (SceneContent.Instance.NeedTutorial is false)
            {
                Destroy(gameObject);
                return;
            }

            _words.Add(SceneContent.Instance._obligatoryWords);
            _words.Add(SceneContent.Instance._otherWords);


            if (_container.Count > 0)
                Destroy(this.gameObject);

            for (int i = 0; i < _words.Count; i++)
            {
                foreach (var wordlist in _words[i].Words)
                {
                    foreach (var word in wordlist.Words)
                    {
                        AddWord(word, wordlist.Type);
                    }
                }
            }
        }
    }
}