using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Object = UnityEngine.Object;

public class PoolsFactory<TObject, TObjectConfig, TPoolStats, TPoolsConfig, TEnum> : IPoolsFactoryAsync
    where TObject : Component
    where TObjectConfig : ScriptableObject, IUnitConfig<TEnum>
    where TPoolStats : class, IPoolStats
    where TPoolsConfig : ScriptableObject, IPoolsConfig<TPoolStats>
    where TEnum : Enum
{
    private readonly IConfigsProvider _configsProvider;
    private readonly IContainersCreator _containersCreator;
    private readonly ILibraryConfigs<TObjectConfig> _unitConfigsLibrary;
    private readonly ILibraryConfigs<TPoolsConfig> _poolConfigsLibrary;

    private PoolType _poolType;

    private Transform _playerUnitPoolContainer;

    public PoolsFactory(IConfigsProvider configsProvider, IContainersCreator containersCreator)
    {
        _configsProvider = configsProvider;
        _containersCreator = containersCreator;

        _unitConfigsLibrary = _configsProvider.GetConfigsLibrary<TObjectConfig>();
        _poolConfigsLibrary = _configsProvider.GetConfigsLibrary<TPoolsConfig>();
    }

    public PoolType PoolType => _poolType;

    public async UniTask<Dictionary<int, IObjectPool>> CreateAsync()
    {
        Dictionary<int, IObjectPool> unitPools = new Dictionary<int, IObjectPool>();

        var poolsConfig = GetPoolsConfig();

        foreach (var unitConfig in _unitConfigsLibrary.Configs)
        {
            if (unitConfig.UnitMainStats.Prefab == null)
            {
                Debug.LogError($"This UnitPrefab {unitConfig.UnitMainStats.Prefab} is null. Check this code line.");
                continue;
            }

            Debug.Log($"TObject is {typeof(TObject).Name}");

            ObjectPool<TObject> objectPool = GetObjectPool(poolsConfig, unitConfig);

            await objectPool.CreatePool();

            unitPools.Add(Convert.ToInt32(unitConfig.UnitType), objectPool);
        }

        return unitPools;
    }

    private TPoolsConfig GetPoolsConfig()
    {
        var poolsConfig = _poolConfigsLibrary.Configs
            .FirstOrDefault(c => c.PoolType == GetExpectedPoolType());

        if (poolsConfig == null)
            throw new NullReferenceException($"Pools config not found for PoolType {PoolType} in {typeof(TPoolsConfig).Name}");

        _poolType = poolsConfig.PoolType;

        _playerUnitPoolContainer = _containersCreator.CreateContainer(
            poolsConfig.ContainerForPoolsPrefab, typeof(TObject).Name + "PoolsContainer");

        _playerUnitPoolContainer.parent = _containersCreator.ContainerRoot.transform;

        return poolsConfig;
    }

    private ObjectPool<TObject> GetObjectPool(TPoolsConfig poolsConfig, TObjectConfig unitConfig)
    {
        var enumValue = (TEnum)unitConfig.UnitType;

        var poolStats = poolsConfig.GetPool<TEnum>(enumValue);

        if (poolStats == null)
        {
            Debug.LogWarning($"No PoolStats found for UnitType: {enumValue}");
            return null;
        }

        Transform poolContainer = _containersCreator.CreateContainer(
            _playerUnitPoolContainer.gameObject, enumValue.ToString() + "Pool");

        poolContainer.parent = _playerUnitPoolContainer;

        PoolCreatingArguments poolArgs = new PoolCreatingArguments(
            poolStats.PoolSize,
            poolStats.CanExpand,
            poolContainer);

        ObjectPool<TObject> pool = new ObjectPool<TObject>(unitConfig.UnitMainStats.Prefab, poolArgs);

        if (pool == null)
            throw new NullReferenceException("Pool is null after creation! In PlayerUnitPoolsFactory");

        return pool;
    }

    private PoolType GetExpectedPoolType()
    {
        if (typeof(TObject) == typeof(PlayerUnit)) return PoolType.PlayerUnitPool;
        if (typeof(TObject) == typeof(EnemyUnit)) return PoolType.EnemyUnitPool;
        throw new InvalidOperationException($"Unknown PoolType for {typeof(TObject).Name}");
    }
}
