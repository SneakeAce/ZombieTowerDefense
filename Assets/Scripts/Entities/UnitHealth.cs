using System;
using UnityEngine;

public class UnitHealth : IUnitHealth
{
    public event Action<float> CurrentValueChanged;
    public event Action<float> MaxValueChanged;
    public event Action<IUnit> UnitDied;

    public void TakeDamage()
    {
        throw new NotImplementedException();
    }
}
