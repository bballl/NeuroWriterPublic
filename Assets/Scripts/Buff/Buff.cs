using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Buff : MonoBehaviour
{
    [field: SerializeField] public BuffData Data { get; protected set; }

    public int Level { get; private set; }

    public int RealLevel => Level + 1;

    private void Awake()
    {
        Level = -1;
    }

    [ContextMenu("Activate")]
    public virtual void Activate()
    {
        if (Level < 0)
            Level = 0;

        Debug.Log($"Активация бафа [{Data.Name}. Уровень - {Level}. Value = {Data.BuffDataByLevels[Level].BuffGain}]");
        ConcreteBuffActivate();
    }

    public bool CanUpgrade()
    {
        if ((Level + 1) < Data.BuffDataByLevels.Count)
        {
            return true;
        }
        Debug.Log("You have max Level");
        return false;
    }

    public void Upgrade()
    {
        Level++;
        Activate();
    }

    public abstract string GetDescription();
    protected abstract void ConcreteBuffActivate();
}
