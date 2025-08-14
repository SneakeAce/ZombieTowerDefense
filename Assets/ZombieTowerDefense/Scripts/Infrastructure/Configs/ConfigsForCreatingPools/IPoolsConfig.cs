using System;
using System.Collections.Generic;
using UnityEngine;

public interface IPoolsConfig<TPoolStats>
    where TPoolStats : class
{
    List<TPoolStats> PoolConfigs { get; }
    GameObject ContainerForPoolsPrefab { get; }
    PoolType PoolType { get; }

    TPoolStats GetPool<T>(T type) where T : Enum;
}