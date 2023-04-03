using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Random = UnityEngine.Random;

public class DialogPanel : MonoBehaviour
{
    public static event Action<DialogData> OnReplicaShowed;

    [SerializeField] private Image _speakerIcon;
    [SerializeField] private TMP_Text _speakerName;
    [SerializeField] private TMP_Text _replic;
    [SerializeField] private Button _nextButton;
    [SerializeField] private CanvasGroup _canvasGroup;

    [SerializeField] private float _timeToShowPanel;
    [SerializeField] private float _timeToHidePanel;

    [SerializeField] private float _timeDelayBeforeHideReplica = 3f;

    private DialogData _dialogData;
    private bool _textIsShowed = false;

    private void Start()
    {
        _canvasGroup.alpha = 0;
        _nextButton.onClick.AddListener(OnButtonClicked);
    }

    public void Activate(DialogData dialogData)
    {
        _dialogData = dialogData;

        if (_dialogData.Name.Equals(string.Empty))
        {
            _speakerName.text = " ";
        }
        else
        {
            _speakerName.text = $"{_dialogData.Name}: ";
        }

        _speakerIcon.sprite = _dialogData.SpeakerIcon;
        _replic.text = string.Empty;

        _nextButton.interactable = true;

        _textIsShowed = false;

        Show();
    }

    private void OnButtonClicked()
    {
        if (_textIsShowed)
        {
            _nextButton.interactable = false;
            Hide();
        }
        else
        {
            ShowTextFast();
        }
    }

    private void Show()
    {
        DOTween.Sequence()
            .Append(_canvasGroup.DOFade(1f, _timeToShowPanel))
            .AppendCallback(() => StartCoroutine(ShowTextSlow()))
            .SetLink(gameObject, LinkBehaviour.KillOnDestroy);
    }

    private void Hide()
    {
        DOTween.Sequence()
            .Append(_canvasGroup.DOFade(0, _timeToHidePanel))
            .AppendCallback(() => OnReplicaShowed?.Invoke(_dialogData))
            .SetLink(gameObject, LinkBehaviour.KillOnDestroy);
    }

    private IEnumerator ShowTextSlow()
    {
        yield return new WaitForSeconds(0.5f);

        for (int i = 0; i < _dialogData.Text.Length; i++)
        {
            float timeDelay = Random.Range(0.2f, 1.5f) * 60f / _dialogData.TextSpeed;
            _replic.text = _dialogData.Text.Substring(0, i+1);
            yield return new WaitForSeconds(timeDelay);
        }
        _textIsShowed = true;
        yield break;
    }

    private void ShowTextFast()
    {
        StopAllCoroutines();
        _replic.text = _dialogData.Text;
        _textIsShowed = true;
    }
}
