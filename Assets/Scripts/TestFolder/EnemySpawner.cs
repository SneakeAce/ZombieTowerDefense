using UnityEngine;

public class EnemySpawner : IEnemySpawner
{
    private IPoolManager _poolManager;
    private ICoroutinePerformer _coroutinePerformer;

    public EnemySpawner(IPoolManager poolManager, ICoroutinePerformer coroutinePerformer)
    {
        _poolManager = poolManager;
        _coroutinePerformer = coroutinePerformer;
    }

    public void CreateUnit(UnitType unitType, Vector3 positionToMove)
    {
        throw new System.NotImplementedException();
    }
}
