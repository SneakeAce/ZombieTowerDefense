using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;

public class PoolManager : IPoolManager
{
    private readonly Dictionary<PoolType, Dictionary<int, IObjectPool>> _pools = new Dictionary<PoolType, Dictionary<int, IObjectPool>>();

    private readonly List<IPoolsFactoryAsync> _poolFactories;

    public PoolManager(List<IPoolsFactoryAsync> poolFactories)
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
        // Тут будет универсальная фабрика пулов. Она будет принимать в себя префаб объекта, библиотеку конфигов, конфиг пулов
        // и тип объекта, например, PlayerUnitType и т.д.
        //Dictionary<int, IObjectPool> pools = await _poolFactories[i].CreateAsync();
        //Dictionary<int, IObjectPool> pools = await _poolFactories[i].CreateAsync();
        //Dictionary<int, IObjectPool> pools = await _poolFactories[i].CreateAsync();
        //Dictionary<int, IObjectPool> pools = await _poolFactories[i].CreateAsync();

        for (int i = 0; i < _poolFactories.Count; i++)
        {
            Dictionary<int, IObjectPool> pools = await _poolFactories[i].CreateAsync();

            _pools.Add(_poolFactories[i].PoolType, pools);
        }
    }
}
