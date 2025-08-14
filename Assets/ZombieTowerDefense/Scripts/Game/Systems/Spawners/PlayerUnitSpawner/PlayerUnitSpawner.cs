using System.Collections;
using UnityEngine;

public class PlayerUnitSpawner : UnitSpawner, IPlayerUnitSpawner
{
    public PlayerUnitSpawner(ICoroutinePerformer coroutinePerformer, IPoolManager poolManager, 
        IUnitsFactory playerUnitFactory, ICommandInvoker commandInvoker) 
        : base(coroutinePerformer, poolManager, playerUnitFactory, commandInvoker)
    {
    }

    protected override IEnumerator CreateUnitJob(ICreateUnitData createUnitData)
    {
        _poolType = PoolType.PlayerUnitPool;

        IObjectPool currentObjectPool = _poolManager.GetPool<PlayerUnitType>(_poolType, (PlayerUnitType)createUnitData.UnitTypeEnum);

        UnitSpawnArguments factoryArguments = new UnitSpawnArguments(
            currentObjectPool, 
            createUnitData.SpawnPosition, 
            createUnitData.SpawnRotation,
            _poolType);

        yield return new WaitForSeconds(createUnitData.HiringTime);

        PlayerUnit unit = _unitFactory.CreateObject<PlayerUnit, UnitSpawnArguments>(factoryArguments);

        if (unit == null)
            yield break;

        unit.Initialize();

        _commandInvoker.AddCommand(new MoveCommand(unit, createUnitData.PositionToMove));

        _commandInvoker.ExecuteCommand();
    }
}
