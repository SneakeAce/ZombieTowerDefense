using System;
using UnityEngine;

[Serializable]
public class PlayerPoolStats : IPoolStats
{
    [field: SerializeField] public int SizePool { get; private set; }
    [field: SerializeField] public bool Expand { get; private set; }
    [field: SerializeField] public PlayerUnitType UnitType { get; private set; }

    public int PoolSize => SizePool;
    public bool CanExpand => Expand;
}
