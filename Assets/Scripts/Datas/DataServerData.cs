using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/DataServerData")]
public class DataServerData : ScriptableObject
{
    [Header("Spawn")]
    [Tooltip("Время в секундах")]
    public float TimeBetweenSpawn;

    [Header("Drop")]
    [Range(0, 1)]
    public float ChanceToDropHealth;
    [Range(0, 1)]
    public float ChanceToDropGold;
    public int HealthDropCount;
    public int GoldDropCount;

    [Header("DataServer")]
    public float Health;
}