using System.Collections;
using UnityEngine;

public class PlayerUnitSpawner : IPlayerUnitSpawner
{
    private IPoolManager _poolManager;
    private IPlayerUnitsFactory _playerUnitFactory;
    private ICommandInvoker _commandInvoker;

    private ICoroutinePerformer _coroutinePerformer; // пока не используется
    private Coroutine _createUnitCoroutine; // Сделать массив корутин, чтобы можно было нанимать несколько юнитов одновременно.

    public PlayerUnitSpawner(ICoroutinePerformer coroutinePerformer, IPoolManager poolManager,
        IPlayerUnitsFactory playerUnitFactory, ICommandInvoker commandInvoker)
    {
        _coroutinePerformer = coroutinePerformer;
        _poolManager = poolManager;
        _playerUnitFactory = playerUnitFactory;
        _commandInvoker = commandInvoker;
    }

    public void CreateUnit(UnitType unitType, Vector3 positionToMove)
    {
        _createUnitCoroutine = _coroutinePerformer.StartRoutine(CreateUnitJob(unitType, positionToMove));
    }

    private IEnumerator CreateUnitJob(UnitType unitType, Vector3 positionToMove)
    {
        IObjectPool currentObjectPool = _poolManager.GetPool<UnitType>(PoolType.PlayerUnitPool, unitType);

        UnitSpawnArguments factoryArguments = new UnitSpawnArguments(currentObjectPool, new Vector3(3.1f, 0f, 0f), Quaternion.identity);

        yield return new WaitForSeconds(2f);

        Unit unit = _playerUnitFactory.CreateObject<Unit, UnitSpawnArguments>(factoryArguments);

        if (unit == null)
            yield break;

        unit.Initialize();

        _commandInvoker.AddCommand(new MoveCommand(unit, positionToMove));

        _commandInvoker.ExecuteCommand();
    }
}
