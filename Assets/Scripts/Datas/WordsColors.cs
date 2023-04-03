using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Words Colors")]
public class WordsColors : ScriptableObject
{
    public List<WordColor> WordColors;
}

[System.Serializable]
public class WordColor
{
    public WordType WordType;
    public Color Color;
}
