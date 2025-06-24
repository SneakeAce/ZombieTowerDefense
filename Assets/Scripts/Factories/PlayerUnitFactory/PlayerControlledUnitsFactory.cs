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

        T unit = (T)spawnArgs.Pool.GetObjectFromPool();

        if (unit == null)
            throw new NullReferenceException("Unit is Null!");

        _container.Inject(unit);
        
        if (unit is Unit un)
        {
            foreach (var config in _unitConfigs.Configs)
            {
                un.SetConfig(config);
            }
        }

        return unit;
    }
}
