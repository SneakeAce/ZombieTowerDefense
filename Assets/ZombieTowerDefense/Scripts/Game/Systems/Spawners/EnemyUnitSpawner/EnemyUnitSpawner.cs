using System.Collections;
using UnityEngine;

public class EnemyUnitSpawner : UnitSpawner, IEnemyUnitSpawner
{
    public EnemyUnitSpawner(ICoroutinePerformer coroutinePerformer, IPoolManager poolManager, 
        IUnitsFactory enemyUnitFactory, ICommandInvoker commandInvoker) 
        : base(coroutinePerformer, poolManager, enemyUnitFactory, commandInvoker)
    {
    }

    protected override IEnumerator CreateUnitJob(ICreateUnitData createUnitData)
    {
        Debug.Log($"EnemyUnitSpawner CreateUnitJob");

        _poolType = PoolType.EnemyUnitPool;

        IObjectPool currentObjectPool = _poolManager.GetPool<EnemyUnitType>(_poolType, (EnemyUnitType)createUnitData.UnitTypeEnum);

        UnitSpawnArguments factoryArguments = new UnitSpawnArguments(
            currentObjectPool,
            createUnitData.SpawnPosition,
            createUnitData.SpawnRotation,
            _poolType);

        EnemyUnit unit = _unitFactory.CreateObject<EnemyUnit, UnitSpawnArguments>(factoryArguments);

        if (unit == null)
            yield break;

        unit.Initialize();

        _commandInvoker.AddCommand(new MoveCommand(unit, createUnitData.PositionToMove));

        _commandInvoker.ExecuteCommand();

        yield return null;
    }
}
