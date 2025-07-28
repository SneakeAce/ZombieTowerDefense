using Zenject;

public class FirstLevelInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        BindServices();

        BindPoolManagerAndAsyncPoolFactories();

        BindBootstrapper();
    }

    private void BindServices()
    {
        Container.Bind<ICommandInvoker>()
            .To<CommandInvoker>()
            .AsSingle();

        Container.Bind<UnitMoveTargetSelector>().AsSingle();

        Container.Bind<SelectUnit>()
            .AsSingle()
            .NonLazy();
    }

    private void BindPoolManagerAndAsyncPoolFactories()
    {
        Container.Bind<IAsyncPoolFactory>()
            .To<PlayerUnitPoolsFactory>()
            .AsSingle();

        Container.Bind<IPoolManager>().To<PoolManager>().AsSingle();
    }

    private void BindBootstrapper()
    {
        Container.BindInterfacesAndSelfTo<FirstLevelSceneBootstrapper>()
            .AsSingle()
            .NonLazy();
    }
}
