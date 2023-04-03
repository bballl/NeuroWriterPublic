using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Enemy")]
public class EnemyData : ScriptableObject
{
    [Header("COMMON")]
    public float MoveSpeed;

    [Header("NUMBER ENEMY")]
    public int NullNumberExperience;
    public int OneNumberExperience;

    [Header("LETTER ENEMY")]
    public float LetterDamage;

    [Header("WORD ENEMY")]
    public float HpMultiplier;
    public float DamageMultiplier;
}