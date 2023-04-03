using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/SimpleBuff")]
public class BuffData : ScriptableObject
{
    public string Name;
    public string Description;

    public Sprite Icon;

    public List<BuffDataByLevel> BuffDataByLevels;
}

[System.Serializable]
public class BuffDataByLevel
{
    public int Level;
    public float BuffGain;
}
