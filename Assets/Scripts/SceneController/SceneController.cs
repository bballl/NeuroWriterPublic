using Assets.Scripts.MenuAndUI;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameBoxProject
{
    public class SceneController : MonoBehaviour
    {
        public static event Action<Scenes> OnSceneStartLoad;
        public static event Action<Scenes> OnSceneStartLoadWithFade;

        public static void LoadScene(Scenes scene)
        {
            OnSceneStartLoad?.Invoke(scene);

            if (Time.timeScale == 0)
                Time.timeScale = 1f;

            SceneManager.LoadScene((int)scene);
        }

        public static void LoadSceneWithFade(Scenes scene)
        {
            Debug.Log($"Start fade to load new scene ({scene})");
            OnSceneStartLoadWithFade?.Invoke(scene);
            ScreenFader.Instance.Activate(() => LoadScene(scene));
        }

        public void LoadMainMenu() //”ƒ¿À»“‹
        {
            LoadScene(Scenes.MainMenu);
        }
    }
}