using UnityEngine;
using Zenject;

public class FirstLevelInstaller : MonoInstaller
{
    [SerializeField] private PlayerControlledUnitConfigs _unitConfig;

    public override void InstallBindings()
    {
        BindServices();
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

}
