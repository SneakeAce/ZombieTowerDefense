using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;

public class PoolManager : IPoolManager
{
    private readonly Dictionary<PoolType, Dictionary<int, IObjectPool>> _pools = new Dictionary<PoolType, Dictionary<int, IObjectPool>>();

    public PoolManager(List<IAsyncPoolFactory> poolFactories)
    {
        CreatePoolsAsync(poolFactories).Forget();
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

    private async UniTask CreatePoolsAsync(List<IAsyncPoolFactory> poolFactories)
    {
        for (int i = 0; i < poolFactories.Count; i++)
        {
            Dictionary<int, IObjectPool> pools = await poolFactories[i].CreateAsync();

            _pools.Add(poolFactories[i].PoolType, pools);
        }
    }
}
