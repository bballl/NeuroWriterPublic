using Assets.Scripts.MenuAndUI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameBoxProject
{
    public class SceneChanger : MonoBehaviour
    {
        public void LoadScene(Scenes scenes)
        {
            SceneManager.LoadScene((int)scenes);
        }

        private void Start()
        {
            Timer.OnTimeEnded += Timer_OnTimeEnded;
        }

        public void Timer_OnTimeEnded()
        {
            LoadScene(Scenes.WordsScene);
        }

        private void OnDestroy()
        {
            Timer.OnTimeEnded -= Timer_OnTimeEnded;
        }
    }
}