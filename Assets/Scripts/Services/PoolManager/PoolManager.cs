using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;

public class PoolManager : IPoolManager
{
    private readonly Dictionary<PoolType, Dictionary<int, IObjectPool>> _pools = new Dictionary<PoolType, Dictionary<int, IObjectPool>>();

    public PoolManager(List<IAsyncPoolFactory> poolFactories)
    {
        foreach (IAsyncPoolFactory asyncPool in poolFactories)
        {
            UnityEngine.Debug.Log($"PoolManager contrusct called. poolFactory = {asyncPool}");
        }

        CreatePoolsAsync(poolFactories).Forget();
    }

    public Dictionary<PoolType, Dictionary<int, IObjectPool>> AvailablePools => _pools;

    public IObjectPool GetPool<TEnum>(PoolType poolType, TEnum type) 
        where TEnum : Enum
    {
        if (_pools.TryGetValue(poolType, out var dictionaryPools))
        {
            UnityEngine.Debug.Log("first if in GetPool");

            foreach (var kvp in dictionaryPools)
            {
                UnityEngine.Debug.Log($"kvp.Key = {kvp.Key}, kvp.Value = {kvp.Value}");
            }

            int key = Convert.ToInt32(type);

            if (dictionaryPools.TryGetValue(key, out var pool))
            {
                UnityEngine.Debug.Log("second if in GetPool");
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
            Dictionary<int, IObjectPool> pools = await poolFactories[i].CreateAsync();

            _pools.Add(poolFactories[i].PoolType, pools);

            foreach (var kvp in _pools)
            {
                UnityEngine.Debug.Log($"CreatePoolsAsync pools.key = {kvp.Key.GetType()}. pools.Value = {kvp.Value.GetType()}");
            }
        }
    }
}
