using System;
using System.Collections.Generic;
using System.Diagnostics;
using Cysharp.Threading.Tasks;

public class PlayerUnitPoolsFactory : IAsyncPoolFactory
{
    private readonly PlayerControlledUnitConfigs _config;
    private readonly PoolType _poolType = PoolType.PlayerUnitPool;

    private Dictionary<UnitType, ObjectPool<Unit>> _unitPools = new Dictionary<UnitType, ObjectPool<Unit>>();

    public PlayerUnitPoolsFactory(PlayerControlledUnitConfigs config)
    {
        UnityEngine.Debug.Log($"PlayerUnitPoolsFactory contruct called. config = {config.name}");

        _config = config;
    }

    public Dictionary<UnitType, ObjectPool<Unit>> UnitPools => _unitPools;
    public PoolType PoolType => _poolType;

    public async UniTask<Dictionary<Enum, IObjectPool>> CreateAsync() 
    {
        foreach (UnitConfig unitConfig in _config.Configs)
        {
            PoolCreatingArguments poolArgs = new PoolCreatingArguments(_config.InitialPoolSize, _config.CanExpand, _config.Container);

            ObjectPool<Unit> pool = new ObjectPool<Unit>(unitConfig.Prefab, poolArgs);

            if (pool == null)
                throw new NullReferenceException("Pool is null after creation! In PlayerUnitPoolsFactory");

            await pool.CreatePool();

            _unitPools.Add(unitConfig.UnitType, pool);
        }

        Dictionary<Enum, IObjectPool> result = new Dictionary<Enum, IObjectPool>();
        foreach (var kvp in _unitPools)
        {
            result.Add(kvp.Key, kvp.Value);
        }

        UnityEngine.Debug.Log($"CreateAsync in Factory. result = {result.Count}");

        return result;
    }
}
