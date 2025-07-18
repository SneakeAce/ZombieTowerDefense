using UnityEngine;

public class PlayerControlledUnitSpawner : IPlayerUnitSpawner
{
    private IPoolManager _poolManager;
    private IPlayerControlledUnitsFactory _playerUnitFactory;
    private ICommandInvoker _commandInvoker;

    private ICoroutinePerformer _coroutinePerformer; // пока не используется
    private Coroutine _createUnitCoroutine; // пока не используется

    public PlayerControlledUnitSpawner(ICoroutinePerformer coroutinePerformer, IPoolManager poolManager,
        IPlayerControlledUnitsFactory playerUnitFactory, ICommandInvoker commandInvoker)
    {
        _coroutinePerformer = coroutinePerformer;
        _poolManager = poolManager;
        _playerUnitFactory = playerUnitFactory;
        _commandInvoker = commandInvoker;
    }

    public void CreateUnit(UnitType unitType, Vector3 positionToMove)
    {
        CreateUnitJob(unitType, positionToMove);
    }

    private void CreateUnitJob(UnitType unitType, Vector3 positionToMove)
    {
        IObjectPool currentObjectPool = _poolManager.GetPool<UnitType>(PoolType.PlayerUnitPool, unitType);

        UnitSpawnArguments factoryArguments = new UnitSpawnArguments(currentObjectPool, new Vector3(3.1f, 0f, 0f), Quaternion.identity);

        Unit unit = _playerUnitFactory.CreateObject<Unit, UnitSpawnArguments>(factoryArguments);

        if (unit == null)
            return;

        unit.Initialize();

        _commandInvoker.AddCommand(new MoveCommand(unit, positionToMove));

        _commandInvoker.ExecuteCommand();
    }
}
