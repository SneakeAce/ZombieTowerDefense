using System;

public interface IEntityHealth
{
    void TakeDamage();

    event Action<float> CurrentValueChanged;
    event Action<float> MaxValueChanged;
    event Action<IEntity> EntityDied;
}
