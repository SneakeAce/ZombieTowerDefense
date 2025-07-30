using System.Collections;
using UnityEngine;

public class PlayerUnitSpawner : IPlayerUnitSpawner
{
    private IPoolManager _poolManager;
    private IPlayerUnitsFactory _playerUnitFactory;
    private ICommandInvoker _commandInvoker;

    private ICoroutinePerformer _coroutinePerformer;
    private Coroutine _createUnitCoroutine; // Сделать массив корутин, чтобы можно было нанимать несколько юнитов одновременно.

    public PlayerUnitSpawner(ICoroutinePerformer coroutinePerformer, IPoolManager poolManager,
        IPlayerUnitsFactory playerUnitFactory, ICommandInvoker commandInvoker)
    {
        _coroutinePerformer = coroutinePerformer;
        _poolManager = poolManager;
        _playerUnitFactory = playerUnitFactory;
        _commandInvoker = commandInvoker;
    }

    public void CreateUnit(CreateUnitData createUnitData)
    {
        _createUnitCoroutine = _coroutinePerformer.StartRoutine(CreateUnitJob(createUnitData));
    }

    private IEnumerator CreateUnitJob(CreateUnitData createUnitData)
    {
        IObjectPool currentObjectPool = _poolManager.GetPool<UnitType>(PoolType.PlayerUnitPool, createUnitData.UnitType);

        UnitSpawnArguments factoryArguments = new UnitSpawnArguments(
            currentObjectPool, 
            createUnitData.SpawnPosition, 
            createUnitData.SpawnRotation);

        yield return new WaitForSeconds(2f);

        Unit unit = _playerUnitFactory.CreateObject<Unit, UnitSpawnArguments>(factoryArguments);

        if (unit == null)
            yield break;

        unit.Initialize();

        _commandInvoker.AddCommand(new MoveCommand(unit, createUnitData.PositionToMove));

        _commandInvoker.ExecuteCommand();
    }
}
