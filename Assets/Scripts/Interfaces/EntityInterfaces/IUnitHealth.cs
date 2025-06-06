using System;

public interface IUnitHealth
{
    void TakeDamage();

    event Action<float> CurrentValueChanged;
    event Action<float> MaxValueChanged;
    event Action<IUnit> UnitDied;
}
