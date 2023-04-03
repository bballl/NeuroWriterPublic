using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelProgress", menuName = "Data/LevelProgress")]
public class LevelData : ScriptableObject
{
    public List<Level> LevelProgress;
}

[System.Serializable]
public class Level
{
    public int Lvl;
    public int NeededExp;
}