using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class PoolManager : IPoolManager
{
    private readonly Dictionary<PoolType, Dictionary<int, IObjectPool>> _pools = new Dictionary<PoolType, Dictionary<int, IObjectPool>>();

    private readonly List<IAsyncPoolFactory> _poolFactories;

    public PoolManager(List<IAsyncPoolFactory> poolFactories)
    {
        _poolFactories = poolFactories ?? throw new ArgumentNullException(nameof(poolFactories));
    }

    public Dictionary<PoolType, Dictionary<int, IObjectPool>> AvailablePools => _pools;

    public IObjectPool GetPool<TEnum>(PoolType poolType, TEnum type) 
        where TEnum : Enum
    {
        if (_pools.TryGetValue(poolType, out var dictionaryPools))
        {
            int key = Convert.ToInt32(type);

            if (dictionaryPools.TryGetValue(key, out var pool))
            {
                return pool;
            }
        }

        throw new KeyNotFoundException($"Pool not found: {poolType} -> {type}");
    }

    public async UniTask CreatePoolsAsync()
    {
        for (int i = 0; i < _poolFactories.Count; i++)
        {
            Dictionary<int, IObjectPool> pools = await _poolFactories[i].CreateAsync();

            _pools.Add(_poolFactories[i].PoolType, pools);
        }
    }
}
