using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;

public class PoolManager
{
    private readonly Dictionary<PoolType, Dictionary<Enum, IObjectPool>> _pools = new Dictionary<PoolType, Dictionary<Enum, IObjectPool>>();

    private IAsyncPoolFactory _currentPoolFactory;

    public PoolManager(List<IAsyncPoolFactory> poolFactories)
    {
        UnityEngine.Debug.Log($"PoolManager contrusct called. poolFactories = {poolFactories}");

        CreatePoolsAsync(poolFactories).Forget();
    }

    public IObjectPool GetPool(PoolType poolType, Enum type)
    {
        if (_pools.TryGetValue(poolType, out var dictionaryPools))
        {
            if (dictionaryPools.TryGetValue(type, out var pool))
            {
                return pool;
            }
        }

        throw new KeyNotFoundException($"Pool not found: {poolType} -> {type}");
    }

    private async UniTask CreatePoolsAsync(List<IAsyncPoolFactory> poolFactories)
    {
        UnityEngine.Debug.Log($"CreatePoolsAsync in PoolManager called");

        for (int i = 0; i < poolFactories.Count; i++)
        {
            Dictionary<Enum, IObjectPool> pools = await poolFactories[i].CreateAsync();

            UnityEngine.Debug.Log($"CreatePoolsAsync cycle For. pools.Keys = {pools.Keys.Count}, pools.Values = {pools.Values.Count}");

            _pools.Add(poolFactories[i].PoolType, pools);
        }
    }
}
