using System;
using Zenject;
using Object = UnityEngine.Object;

public class PlayerControlledUnitsFactory : IPlayerControlledUnitsFactory
{
    private DiContainer _container;
    private IPoolConfig<PlayerUnitConfig> _unitConfigs;

    public PlayerControlledUnitsFactory(DiContainer container, IPoolConfig<PlayerUnitConfig> unitConfigs)
    {
        _container = container;
        _unitConfigs = unitConfigs;
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
        
        if (unitT is Unit unit)
        {
            foreach (var config in _unitConfigs.Configs)
            {
                unit.SetConfig(config);
                unit.transform.position = spawnArgs.SpawnPosition;
                unit.transform.rotation = spawnArgs.SpawnRotation;
            }
        }

        return unitT;
    }
}
