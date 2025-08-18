using System;
using Zenject;
using Object = UnityEngine.Object;

public class UnitFactory : IUnitsFactory
{
    private DiContainer _container;
    private IConfigsProvider _configsProvider;

    public UnitFactory(DiContainer container, IConfigsProvider configsProvider)
    {
        _container = container;
        _configsProvider = configsProvider;
    }

    public T CreateObject<T, TArgs>(TArgs args) 
        where T : Object
        where TArgs : IFactoryArguments
    {
        if (args is not UnitSpawnArguments spawnArgs)
            throw new ArgumentException("Invalid arguments provided for UnitsFactory.");

        T unitT = (T)spawnArgs.Pool.GetObjectFromPool();

        if (unitT == null)
            throw new ArgumentNullException("Unit is Null!");

        switch (spawnArgs.UnitPoolType)
        {
            case PoolType.PlayerUnitPool:

                ILibraryConfigs<PlayerUnitConfig> playerUnitLibraryConfigs = _configsProvider.GetConfigsLibrary<PlayerUnitConfig>();

                if (unitT is PlayerUnit playerUnit)
                {
                    foreach (var config in playerUnitLibraryConfigs.Configs)
                    {
                        playerUnit.SetConfig(config);
                        playerUnit.transform.position = spawnArgs.SpawnPosition;
                        playerUnit.transform.rotation = spawnArgs.SpawnRotation;

                        _container.Inject(playerUnit);
                    }
                }
                break;

            case PoolType.EnemyUnitPool:

                ILibraryConfigs<EnemyUnitConfig> enemyUnitLibraryConfigs = _configsProvider.GetConfigsLibrary<EnemyUnitConfig>();

                if (unitT is EnemyUnit enemyUnit)
                {
                    foreach (var config in enemyUnitLibraryConfigs.Configs)
                    {
                        enemyUnit.SetConfig(config);
                        enemyUnit.transform.position = spawnArgs.SpawnPosition;
                        enemyUnit.transform.rotation = spawnArgs.SpawnRotation;

                        _container.Inject(enemyUnit);
                    }
                }
                break;
        }

        return unitT;
    }
}
