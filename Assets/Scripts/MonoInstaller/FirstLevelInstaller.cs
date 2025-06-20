using UnityEngine;
using UnityEngine.TextCore.Text;
using Zenject;

public class FirstLevelInstaller : MonoInstaller
{
    [SerializeField] private PlayerControlledUnitConfigs _unitConfig;

    public override void InstallBindings()
    {

        BindServices();

        BindPlayerUnitSpawner();
    }


    private void BindServices()
    {
        Container.Bind<IUnitHealth>()
            .To<UnitHealth>()
            .AsTransient();

        Container.Bind<ICommandInvoker>()
            .To<CommandInvoker>()
            .AsSingle();

        Container.Bind<UnitMoveTargetSelector>().AsSingle();

        Container.Bind<SelectUnit>()
            .AsSingle()
            .NonLazy();

        Container.Bind<IPoolConfig<PlayerUnitConfig>>()
            .To<PlayerControlledUnitConfigs>()
            .FromInstance(_unitConfig)
            .AsSingle();

        Container.Bind<IAsyncPoolFactory>()
            .To<PlayerUnitPoolsFactory>()
            .AsSingle();

        Container.Bind<IPoolManager>().To<PoolManager>().AsSingle();
    }

    private void BindPlayerUnitSpawner()
    {
        Container.Bind<IPlayerControlledUnitsFactory>().To<PlayerControlledUnitsFactory>().AsSingle();

        Container.Bind<IPlayerUnitSpawner>().To<PlayerControlledUnitSpawner>().AsSingle();

        Container.Bind<SpawnerManager>().AsSingle().NonLazy();
    }
}
