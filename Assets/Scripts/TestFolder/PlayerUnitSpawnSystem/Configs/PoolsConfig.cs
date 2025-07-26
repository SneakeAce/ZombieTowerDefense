using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class PoolsConfig : ScriptableObject
{
    [field: SerializeField] public List<PoolStats> PoolConfigs { get; private set; }
    [field: SerializeField] public GameObject ContainerForPoolsPrefab { get; private set; }
    [field: SerializeField] public PoolType PoolType { get; private set; }

    public abstract PoolStats GetPool<T>(T type) where T : Enum;
}
