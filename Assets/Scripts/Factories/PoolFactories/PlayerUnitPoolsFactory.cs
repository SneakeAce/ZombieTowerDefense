using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;

public class PlayerUnitPoolsFactory : IAsyncPoolFactory
{
    private readonly IPoolConfig<UnitConfig> _config;
    private readonly PoolType _poolType = PoolType.PlayerUnitPool;

    public PlayerUnitPoolsFactory(IPoolConfig<UnitConfig> config)
    {
        UnityEngine.Debug.Log($"PlayerUnitPoolsFactory contruct called. config = {config}");

        _config = config;
    }

    public PoolType PoolType => _poolType;

    public async UniTask<Dictionary<int, IObjectPool>> CreateAsync() 
    {
        Dictionary<int, IObjectPool> unitPools = new Dictionary<int, IObjectPool>();

        foreach (UnitConfig unitConfig in _config.Configs)
        {
            PoolCreatingArguments poolArgs = new PoolCreatingArguments(_config.PoolSize, _config.CanExpand, _config.PoolContainer);

            ObjectPool<Unit> pool = new ObjectPool<Unit>(unitConfig.Prefab, poolArgs);

            if (pool == null)
                throw new NullReferenceException("Pool is null after creation! In PlayerUnitPoolsFactory");

            await pool.CreatePool();

            unitPools.Add((int)unitConfig.UnitType, pool);
        }

        return unitPools;
    }
}
