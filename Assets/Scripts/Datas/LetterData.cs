using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Letters")]
public class LetterData : ScriptableObject
{
    public List<Letter> Letters;
}

[System.Serializable]
public class Letter
{
    public char Name;
    public bool IsSimetric = true;
    public Sprite LeftIcon;
    public Sprite RightIcon;
}
