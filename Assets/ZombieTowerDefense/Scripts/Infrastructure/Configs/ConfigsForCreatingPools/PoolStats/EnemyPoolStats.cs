using System;
using UnityEngine;

[Serializable]
public class EnemyPoolStats : IPoolStats
{
    [field: SerializeField] public int SizePool { get; private set; }
    [field: SerializeField] public bool Expand { get; private set; }
    [field: SerializeField] public EnemyUnitType UnitType { get; private set; }

    public int PoolSize => SizePool;
    public bool CanExpand => Expand;
}
