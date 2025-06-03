using System;
using UnityEngine;

public class EntityHealth : IEntityHealth
{
    public event Action<float> CurrentValueChanged;
    public event Action<float> MaxValueChanged;
    public event Action<IEntity> EntityDied;

    public void TakeDamage()
    {
        throw new NotImplementedException();
    }
}
