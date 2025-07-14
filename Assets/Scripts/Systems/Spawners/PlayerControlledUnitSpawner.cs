using UnityEngine;

public class PlayerControlledUnitSpawner : IPlayerUnitSpawner
{
    private IPoolManager _poolManager;
    private IPlayerControlledUnitsFactory _playerUnitFactory;
    private ICoroutinePerformer _coroutinePerformer; // пока не используется

    private Coroutine _createUnitCoroutine; // пока не используется

    public PlayerControlledUnitSpawner(ICoroutinePerformer coroutinePerformer, IPoolManager poolManager,
        IPlayerControlledUnitsFactory playerUnitFactory)
    {
        _coroutinePerformer = coroutinePerformer;
        _poolManager = poolManager;
        _playerUnitFactory = playerUnitFactory;
    }

    public void CreateUnit(UnitType unitType)
    {
        CreateUnitJob(unitType);
    }

    private void CreateUnitJob(UnitType unitType)
    {
        IObjectPool currentObjectPool = _poolManager.GetPool<UnitType>(PoolType.PlayerUnitPool, unitType);

        UnitSpawnArguments factoryArguments = new UnitSpawnArguments(currentObjectPool, new Vector3(0, 0, 0), Quaternion.identity);

        Unit unit = _playerUnitFactory.CreateObject<Unit, UnitSpawnArguments>(factoryArguments);
    }
}
