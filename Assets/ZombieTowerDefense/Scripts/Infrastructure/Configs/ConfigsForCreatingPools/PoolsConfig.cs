using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class PoolsConfig<TPoolStats> : ScriptableObject, IPoolsConfig<TPoolStats>
    where TPoolStats : class
{
    [field: SerializeField] public GameObject ContainerForPoolsPrefab { get; private set; }
    [field: SerializeField] public PoolType PoolType { get; private set; }

    public abstract List<TPoolStats> PoolConfigs { get; }

    public abstract TPoolStats GetPool<T>(T type) where T : Enum;
}
