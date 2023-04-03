using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Datas/Weapon")]
public class WeaponData : ScriptableObject
{
    public string WeaponName;
    public float Damage;
    public float AttackDistance;
    public float AttackCoolDown;
    public int EnemyOnAttackCount;
}
