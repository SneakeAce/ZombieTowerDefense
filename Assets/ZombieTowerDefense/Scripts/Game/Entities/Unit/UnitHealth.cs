using System;
using UnityEngine;

public class UnitHealth : IUnitHealth
{


    public event Action<float> CurrentValueChanged;
    public event Action<float> MaxValueChanged;
    public event Action<IPlayerUnit> UnitDied;

    public void TakeDamage(DamageData damageData)
    {
        Debug.Log($"Taking damage = {damageData.Damage}");
    }
}
