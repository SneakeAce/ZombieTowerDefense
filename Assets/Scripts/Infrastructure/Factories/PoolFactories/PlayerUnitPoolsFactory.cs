using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class PlayerUnitPoolsFactory : IAsyncPoolFactory
{
    private readonly IConfigsProvider _configsProvider;
    private readonly ILibraryConfigs<PlayerUnitConfig> _unitConfigsLibrary;
    private readonly ILibraryConfigs<PoolsConfig> _poolConfigsLibrary;

    private PoolType _poolType;

    public PlayerUnitPoolsFactory(IConfigsProvider configsProvider)
    {
        _configsProvider = configsProvider;

        _unitConfigsLibrary = _configsProvider.GetLibrary<PlayerUnitConfig>();
        _poolConfigsLibrary = _configsProvider.GetLibrary<PoolsConfig>();
    }

    public PoolType PoolType => _poolType;

    public async UniTask<Dictionary<int, IObjectPool>> CreateAsync() 
    {
        Dictionary<int, IObjectPool> unitPools = new Dictionary<int, IObjectPool>();

        PlayerUnitsPoolsConfig playerUnitsPoolsConfig = null;

        foreach (PoolsConfig poolsConfig in _poolConfigsLibrary.Configs)
        {
            if (poolsConfig.PoolType == PoolType.PlayerUnitPool)
            {
                playerUnitsPoolsConfig = poolsConfig as PlayerUnitsPoolsConfig;

                _poolType = playerUnitsPoolsConfig.PoolType;

                break;
            }
        }

        if (playerUnitsPoolsConfig == null)
            throw new NullReferenceException("PlayerUnitsPoolsConfig not found in PoolConfigsLibrary");

        foreach (PlayerUnitConfig unitConfig in _unitConfigsLibrary.Configs)
        {
            UnitType unitType = unitConfig.UnitMainStats.UnitType;

            PoolStats poolStats = playerUnitsPoolsConfig.GetPool<UnitType>(unitType);

            if (poolStats == null)
            {
                Debug.LogWarning($"No PoolStats found for UnitType: {unitType}");
                continue;
            }

            PoolCreatingArguments poolArgs = new PoolCreatingArguments(
                poolStats.PoolSize,
                poolStats.CanExpand,
                poolStats.PoolContainer);

            ObjectPool<Unit> pool = new ObjectPool<Unit>(unitConfig.UnitMainStats.Prefab, poolArgs);

            if (pool == null)
                throw new NullReferenceException("Pool is null after creation! In PlayerUnitPoolsFactory");

            await pool.CreatePool();

            unitPools.Add((int)unitConfig.UnitMainStats.UnitType, pool);
        }
        
        return unitPools;
    }
}
