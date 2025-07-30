using System.Collections;
using UnityEngine;

public class EnemyUnitSpawner : UnitSpawner, IEnemyUnitSpawner
{
    public EnemyUnitSpawner(ICoroutinePerformer coroutinePerformer, IPoolManager poolManager, 
        IUnitsFactory playerUnitFactory, ICommandInvoker commandInvoker) 
        : base(coroutinePerformer, poolManager, playerUnitFactory, commandInvoker)
    {
    }

    protected override IEnumerator CreateUnitJob(CreateUnitData createUnitData)
    {
        _poolType = PoolType.EnemyUnitPool;

        IObjectPool currentObjectPool = _poolManager.GetPool<UnitType>(_poolType, createUnitData.UnitType);

        UnitSpawnArguments factoryArguments = new UnitSpawnArguments(
            currentObjectPool,
            createUnitData.SpawnPosition,
            createUnitData.SpawnRotation,
            _poolType);

        yield return new WaitForSeconds(createUnitData.HiringTime);

        EnemyUnit unit = _unitFactory.CreateObject<EnemyUnit, UnitSpawnArguments>(factoryArguments);

        if (unit == null)
            yield break;

        unit.Initialize();

        _commandInvoker.AddCommand(new MoveCommand(unit, createUnitData.PositionToMove));

        _commandInvoker.ExecuteCommand();
    }
}
