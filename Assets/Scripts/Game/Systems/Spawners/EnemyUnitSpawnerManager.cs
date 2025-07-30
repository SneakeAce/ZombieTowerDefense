using UnityEngine;

public class EnemyUnitSpawnerManager : IEnemyUnitSpawnerManager
{
    private IEnemyUnitSpawner _enemyUnitSpawner;

    public EnemyUnitSpawnerManager(IEnemyUnitSpawner enemyUnitSpawner)
    {
        _enemyUnitSpawner = enemyUnitSpawner;
    }

    public void OnTrySpawn(CreateUnitData createUnitData)
    {
        _enemyUnitSpawner.CreateUnit(createUnitData);
    }
}
