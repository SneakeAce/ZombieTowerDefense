using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class PlayerUnitPoolsFactory : IAsyncPoolFactory
{
    private readonly IConfigsProvider _configsProvider;
    private readonly IContainersCreator _containersCreator;
    private readonly ILibraryConfigs<PlayerUnitConfig> _unitConfigsLibrary;
    private readonly ILibraryConfigs<PoolsConfig> _poolConfigsLibrary;

    private PoolType _poolType;

    private Transform _playerUnitPoolContainer;

    public PlayerUnitPoolsFactory(IConfigsProvider configsProvider, IContainersCreator containersCreator)
    {
        _configsProvider = configsProvider;
        _containersCreator = containersCreator;

        _unitConfigsLibrary = _configsProvider.GetConfigsLibrary<PlayerUnitConfig>();
        _poolConfigsLibrary = _configsProvider.GetConfigsLibrary<PoolsConfig>();
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

                _playerUnitPoolContainer = _containersCreator.CreateContainer(
                    playerUnitsPoolsConfig.ContainerForPoolsPrefab, "PlayerUnitPoolContainer");

                _playerUnitPoolContainer.parent = _containersCreator.ContainerRoot.transform;

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

            Transform poolContainer = _containersCreator.CreateContainer(
                _playerUnitPoolContainer.gameObject, unitType.ToString() + "UnitPool");

            poolContainer.parent = _playerUnitPoolContainer;

            PoolCreatingArguments poolArgs = new PoolCreatingArguments(
                poolStats.PoolSize,
                poolStats.CanExpand,
                poolContainer);

            ObjectPool<PlayerUnit> pool = new ObjectPool<PlayerUnit>(unitConfig.UnitMainStats.Prefab, poolArgs);

            if (pool == null)
                throw new NullReferenceException("Pool is null after creation! In PlayerUnitPoolsFactory");

            await pool.CreatePool();

            unitPools.Add((int)unitConfig.UnitMainStats.UnitType, pool);
        }
        
        return unitPools;
    }
}
