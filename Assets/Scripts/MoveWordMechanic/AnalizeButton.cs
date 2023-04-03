using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GameBoxProject
{
    public class AnalizeButton : MonoBehaviour
    {
        public Button _button;
        public TestAddWordToPanel TestAddWordToPanel;
        public MemoryProgressPanel MemoryProgressPanel;
        

        private void Start()
        {
            _button.onClick.AddListener(ShowResult);
            MemoryProgressPanel.gameObject.SetActive(false);
        }

        public void ShowResult()
        {
            MemoryProgressPanel.Show(TestAddWordToPanel.GetResult());
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveListener(ShowResult);
        }
    }
}