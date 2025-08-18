using System;

public interface IUnitHealth : IDamageable
{
    event Action<float> CurrentValueChanged;
    event Action<float> MaxValueChanged;
    event Action<IPlayerUnit> UnitDied;
}
