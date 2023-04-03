using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogData
{
    public string Name;
    public Sprite SpeakerIcon;
    public string Text;
    [Tooltip("Скорость печати символов в минуту")]
    public float TextSpeed;
}
