using UnityEngine;
using UnityEngine.UI;

public class BuffUiIcon : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private GameObject[] _levelIcons;

    public void Activate(Buff buff)
    {
        _icon.sprite = buff.Data.Icon;

        for (int i = 0; i <= buff.Level; i++)
        {
            _levelIcons[i].SetActive(true);
        }
    }
}