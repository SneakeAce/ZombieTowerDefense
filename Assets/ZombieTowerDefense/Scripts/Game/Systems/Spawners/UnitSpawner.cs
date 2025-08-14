using System.Collections;
using UnityEngine;

public abstract class UnitSpawner
{
    protected IPoolManager _poolManager;
    protected IUnitsFactory _unitFactory;
    protected ICommandInvoker _commandInvoker;

    protected ICoroutinePerformer _coroutinePerformer;
    protected Coroutine _createUnitCoroutine; // Сделать массив корутин, чтобы можно было нанимать несколько юнитов одновременно.

    protected PoolType _poolType;

    public UnitSpawner(ICoroutinePerformer coroutinePerformer, IPoolManager poolManager,
        IUnitsFactory playerUnitFactory, ICommandInvoker commandInvoker)
    {
        _coroutinePerformer = coroutinePerformer;
        _poolManager = poolManager;
        _unitFactory = playerUnitFactory;
        _commandInvoker = commandInvoker;
    }

    public void CreateUnit(ICreateUnitData createUnitData)
    {
        _createUnitCoroutine = _coroutinePerformer.StartRoutine(CreateUnitJob(createUnitData));
    }

    protected abstract IEnumerator CreateUnitJob(ICreateUnitData createUnitData);

}
