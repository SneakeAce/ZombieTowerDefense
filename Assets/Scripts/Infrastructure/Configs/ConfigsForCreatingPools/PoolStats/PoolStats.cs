using System;
using UnityEngine;

[Serializable]
public class PoolStats
{
    [field: SerializeField] public int PoolSize { get; private set; }
    [field: SerializeField] public bool CanExpand { get; private set; }
    [field: SerializeField] public UnitType UnitTypeInPool { get; private set; }
}
