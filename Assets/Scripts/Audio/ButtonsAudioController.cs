using System;
using UnityEngine;

namespace GameBoxProject
{
    public class ButtonsAudioController : MonoBehaviour
    {
        [SerializeField] private AudioSource _audio;
        [SerializeField] private AudioClip _onMouseEnter;
        [SerializeField] private AudioClip _onClick;

        private void Start()
        {
            MouseObserver.OnButtonClicked += MouseObserver_OnButtonClicked;
            MouseObserver.OnMouseEnter += MouseObserver_OnMouseEnter;
        }

        private void MouseObserver_OnMouseEnter()
        {
            PlaySound(_onMouseEnter);
        }

        private void MouseObserver_OnButtonClicked()
        {
            PlaySound(_onClick);
        }

        private void PlaySound(AudioClip clip)
        {
            _audio.PlayOneShot(clip);
        }

        private void OnDestroy()
        {
            MouseObserver.OnButtonClicked -= MouseObserver_OnButtonClicked;
            MouseObserver.OnMouseEnter -= MouseObserver_OnMouseEnter;
        }
    }
}