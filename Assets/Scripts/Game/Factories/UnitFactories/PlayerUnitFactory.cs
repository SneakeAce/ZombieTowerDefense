using System;
using Zenject;
using Object = UnityEngine.Object;

public class PlayerUnitFactory : IPlayerUnitsFactory
{
    private DiContainer _container;
    private IConfigsProvider _configsProvider;

    public PlayerUnitFactory(DiContainer container, IConfigsProvider configsProvider)
    {
        _container = container;
        _configsProvider = configsProvider;
    }

    public T CreateObject<T, TArgs>(TArgs args) 
        where T : Object
        where TArgs : IFactoryArguments
    {
        if (args is not UnitSpawnArguments spawnArgs)
            throw new ArgumentException("Invalid arguments provided for SceneObjectFactory.");

        T unitT = (T)spawnArgs.Pool.GetObjectFromPool();

        if (unitT == null)
            throw new NullReferenceException("Unit is Null!");

        _container.Inject(unitT);

        ILibraryConfigs<PlayerUnitConfig> unitLibraryConfigs = _configsProvider.GetConfigsLibrary<PlayerUnitConfig>();
        
        if (unitT is Unit unit)
        {
            foreach (var config in unitLibraryConfigs.Configs)
            {
                unit.SetConfig(config);
                unit.transform.position = spawnArgs.SpawnPosition;
                unit.transform.rotation = spawnArgs.SpawnRotation;
            }
        }

        return unitT;
    }
}
