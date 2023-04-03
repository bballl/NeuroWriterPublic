using Assets.Scripts.MenuAndUI;
using System;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace GameBoxProject
{
    public class MusicController : MonoBehaviour
    {
        public static MusicController Instance { get; private set; }

        [SerializeField] private Scenes _thisScene;
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private List<SceneMusic> _sceneMusics;

        private Scenes _currentScene = Scenes.ShopScene;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);

                var obj = FindObjectOfType<VolumeController>();
                obj?.LoadVolumes();

                SceneController.OnSceneStartLoadWithFade += ChangeMusicSmooth;

                _audioSource.clip = _sceneMusics.Find(x => x.SceneType.Equals(_thisScene)).Music;
                _audioSource.Play();
            }
            else
                Destroy(gameObject);
        }

        private void ChangeMusicSmooth(Scenes sceneName)
        {
            var sceneMusic = _sceneMusics.Find(x => x.SceneType.Equals(sceneName));

            if (sceneMusic == null)
            {
                Debug.Log($"There is no music for scene {sceneName}");
                return;
            }

            if (sceneMusic.SceneType.Equals(_currentScene))
            {
                Debug.Log($"Music in scene {sceneName} is already play");
                return;
            }

            float startVolume = _audioSource.volume;

            DOTween.Sequence()
                .Append(_audioSource.DOFade(0f, 1.5f))
                .AppendCallback(() => _audioSource.Stop())
                .AppendCallback(() => _audioSource.clip = sceneMusic.Music)
                .AppendCallback(() => _audioSource.Play())
                .AppendCallback(() => _currentScene = sceneName)
                .Append(_audioSource.DOFade(startVolume, 0.5f))
                .SetLink(gameObject, LinkBehaviour.KillOnDestroy)
                .SetUpdate(true)
                .Play();
        }
    }

    [Serializable]
    public class SceneMusic 
    {
        [field: SerializeField] public Scenes SceneType { get; private set; }
        [field: SerializeField] public AudioClip Music { get; private set; }
    }

}