using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/EnemyDataByMinutes")]
public class EnemyDataByMinutes : ScriptableObject
{
    public EnemyType EnemyType;
    public float HpMiltyplier;
    public float DamageMiltyplier;

    public float StartHP;
    public float StartDamage;

    public List<CommonEnemyData> DataByMinutes;

    private void OnValidate()
    {
        DataByMinutes[0].HP = StartHP;
        DataByMinutes[0].Damage = StartDamage;

        for (int i = 1; i < DataByMinutes.Count; i++)
        {
            DataByMinutes[i].HP = Mathf.Abs(DataByMinutes[i - 1].HP * HpMiltyplier);
            DataByMinutes[i].Damage = Mathf.Abs(DataByMinutes[i - 1].Damage * DamageMiltyplier);
        }
    }
}

[System.Serializable]
public class CommonEnemyData
{
    public int MinuteInGame;
    public float SpawnCountInSecond;
    public float HP;
    public float Damage;
}

public enum EnemyType
{
    Number = 0,
    Letter = 1,
    Word = 2,
    Boss = 3
}